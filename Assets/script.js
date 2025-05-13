window.onload = function() {
    const fileInput = document.getElementById('fileInput');
    const dropdownDate = document.getElementById('dropdownDate');
    const dropdownTime = document.getElementById('dropdownTime');
    const lineChartContainer = document.getElementById('lineChart').getContext('2d');
    const pieChartContainer = document.getElementById('pieChart').getContext('2d');
    const barChartContainer = document.getElementById('barChart').getContext('2d');
    const pieChartForBarContainer = document.getElementById('pieChartForBar').getContext('2d');
    let pieChartForBar = null;
    let lineChart = null;
    let pieChart = null;
    let barChart = null;

    let reportData = {};

    // Função para limpar os gráficos
    function clearCharts() {
        if (lineChart) lineChart.destroy();
        if (pieChart) pieChart.destroy();
        if (barChart) barChart.destroy();
        if (pieChartForBar) pieChartForBar.destroy();
    }

    function clearTimeDropdown() {
        dropdownTime.innerHTML = '<option value="">Selecione a hora</option>';
    }

    // Função para converter o tempo de string para segundos
    function converterTempo(tempoStr) {
        let [horas, minutos, segundos] = tempoStr.split(':').map(Number);
        return (horas * 3600) + (minutos * 60) + segundos;
    }

    // Função para carregar e processar os dados do relatório
    async function loadData() {
        try {
            const response = await fetch('Relatorio.txt');
            const text = await response.text();
            processReportData(text);
        } catch (error) {
            console.error('Erro ao carregar o arquivo:', error);
        }
    }

    // Função para processar os dados do relatório
    function processReportData(text) {
        const lines = text.split('\n');
        let currentSession = {};

        lines.forEach(line => {
            if (line.startsWith('[')) {
                const dateTimeMatch = line.match(/\[(\d{2}\/\d{2}\/\d{4}) (\d{2}:\d{2}:\d{2})\]/);
                if (dateTimeMatch) {
                    const [_, date, time] = dateTimeMatch;
                    currentSession = { date, time };
                    if (!reportData[date]) {
                        reportData[date] = [];
                    }
                    reportData[date].push(currentSession);
                }
            } else if (line.includes('Número de colisões:')) {
                currentSession.collisions = parseInt(line.split(': ')[1], 10);
            } else if (line.includes('Tempo para conclusão da atividade:')) {
                currentSession.completionTime = converterTempo(line.split(': ')[1]);
            } else if (line.includes('Maior sequência de acertos:')) {
                currentSession.maxStreak = parseInt(line.split(': ')[1], 10);
            } else if (line.includes('Número de saltos dados pelo paciente:')) {
                currentSession.jumps = parseInt(line.split(': ')[1], 10);
            } else if (line.includes('Velocidade configurada na fase:')) {
                currentSession.speed = parseFloat(line.split(': ')[1].replace(',', '.'));
            }
        });

        // Adicionar opção "Dia todo" e popular dropdown de datas
        const allDayOption = document.createElement('option');
        allDayOption.value = 'all';
        allDayOption.text = 'Dia todo';
        dropdownTime.appendChild(allDayOption);

        Object.keys(reportData).forEach(date => {
            const option = document.createElement('option');
            option.value = date;
            option.text = date;
            dropdownDate.appendChild(option);
        });
    }

    // Evento ao selecionar uma data
dropdownDate.addEventListener('change', function() {
    clearCharts();
    clearTimeDropdown();

    const selectedDate = dropdownDate.value;

    // Adiciona a opção "Dia todo" ao dropdown de horário
    const allDayOption = document.createElement('option');
    allDayOption.value = 'all';
    allDayOption.text = 'Dia todo';
    dropdownTime.appendChild(allDayOption);

    // Adiciona as opções de horários disponíveis para a data selecionada
    if (selectedDate && reportData[selectedDate]) {
        reportData[selectedDate].forEach(session => {
            const option = document.createElement('option');
            option.value = session.time;
            option.text = session.time;
            dropdownTime.appendChild(option);
        });
    }
});


 // Evento ao selecionar uma hora
dropdownTime.addEventListener('change', function() {
    clearCharts();

    const selectedDate = dropdownDate.value;
    const selectedTime = dropdownTime.value.trim();

    if (selectedTime === 'all') {
        // Exibe gráficos de linha e pizza para "Dia todo"
        displayLineChart(selectedDate);
        displayPieChart(selectedDate);
    } else if (selectedTime) {
        // Exibe gráfico de barras para hora específica
        displayBarChart(selectedDate, selectedTime);
    }
});

// Função para exibir o gráfico de barras para uma hora específica
function displayBarChart(selectedDate, selectedTime) {
    const sessions = reportData[selectedDate];
    const session = sessions.find(s => s.time.trim() === selectedTime.trim());

    if (!session) {
        alert('Nenhuma sessão encontrada para a data e hora selecionadas.');
        return;
    }

    // Criar o gráfico de barras
    barChart = new Chart(barChartContainer, {
        type: 'bar',
        data: {
            labels: ['Colisões', 'Tempo de Conclusão', 'Sequência', 'Velocidade', 'Saltos'],
            datasets: [{
                label: `Dados da Sessão - ${selectedDate} ${selectedTime}`,
                data: [
                    session.collisions || 0,
                    session.completionTime || 0,
                    session.maxStreak || 0,
                    session.speed || 0,
                    session.jumps || 0
                ],
                backgroundColor: 'rgba(75, 192, 192, 0.5)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                y: { beginAtZero: true }
            }
        }
    });
    document.getElementById('barChart').style.display = 'block';

    // Criar o gráfico de torta para a sessão específica
    pieChartForBar = new Chart(pieChartForBarContainer, {
        type: 'pie',
        data: {
            labels: ['Colisões', 'Tempo de Conclusão', 'Sequência', 'Velocidade', 'Saltos'],
            datasets: [{
                data: [
                    session.collisions || 0,
                    session.completionTime || 0,
                    session.maxStreak || 0,
                    session.speed || 0,
                    session.jumps || 0
                ],
                backgroundColor: [
                    'rgba(255, 99, 132, 0.6)',
                    'rgba(54, 162, 235, 0.6)',
                    'rgba(255, 206, 86, 0.6)',
                    'rgba(153, 102, 255, 0.6)',
                    'rgba(75, 192, 192, 0.6)'
                ]
            }]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: `Distribuição dos Dados - ${selectedDate} ${selectedTime}`
                }
            }
        }
    });
    document.getElementById('pieChartForBar').style.display = 'block';
}


    // Função para exibir o gráfico de linhas para "Dia todo"
    function displayLineChart(selectedDate) {
        const sessions = reportData[selectedDate];
        const times = sessions.map(s => s.time);
        const collisions = sessions.map(s => s.collisions || 0);
        const completionTimes = sessions.map(s => s.completionTime || 0);
        const maxStreaks = sessions.map(s => s.maxStreak || 0);
        const speeds = sessions.map(s => s.speed || 0);
        const jumps = sessions.map(s => s.jumps || 0);

        lineChart = new Chart(lineChartContainer, {
            type: 'line',
            data: {
                labels: times,
                datasets: [
                    {
                        label: 'Número de Colisões',
                        data: collisions,
                        borderColor: 'rgba(255, 99, 132, 1)',
                        backgroundColor: 'rgba(255, 99, 132, 0.2)',
                        borderWidth: 1,
                        fill: false
                    },
                    {
                        label: 'Tempo de Conclusão (s)',
                        data: completionTimes,
                        borderColor: 'rgba(54, 162, 235, 1)',
                        backgroundColor: 'rgba(54, 162, 235, 0.2)',
                        borderWidth: 1,
                        fill: false
                    },
                    {
                        label: 'Maior Sequência',
                        data: maxStreaks,
                        borderColor: 'rgba(255, 206, 86, 1)',
                        backgroundColor: 'rgba(255, 206, 86, 0.2)',
                        borderWidth: 1,
                        fill: false
                    },
                    {
                        label: 'Velocidade',
                        data: speeds,
                        borderColor: 'rgba(75, 192, 192, 1)',
                        backgroundColor: 'rgba(75, 192, 192, 0.2)',
                        borderWidth: 1,
                        fill: false
                    },
                    {
                        label: 'Saltos Dados',
                        data: jumps,
                        borderColor: 'rgba(153, 102, 255, 1)',
                        backgroundColor: 'rgba(153, 102, 255, 0.2)',
                        borderWidth: 1,
                        fill: false
                    }
                ]
            },
            options: {
                scales: {
                    x: { title: { display: true, text: 'Horários' } },
                    y: { beginAtZero: true }
                }
            }
        });
        document.getElementById('lineChart').style.display = 'block';
    }

    // Função para exibir o gráfico de pizza para "Dia todo"
    function displayPieChart(selectedDate) {
        const sessions = reportData[selectedDate];
        const totals = {
            collisions: 0,
            completionTime: 0,
            maxStreak: 0,
            speed: 0,
            jumps: 0
        };

        sessions.forEach(session => {
            totals.collisions += session.collisions || 0;
            totals.completionTime += session.completionTime || 0;
            totals.maxStreak += session.maxStreak || 0;
            totals.speed += session.speed || 0;
            totals.jumps += session.jumps || 0;
        });

        pieChart = new Chart(pieChartContainer, {
            type: 'pie',
            data: {
                labels: ['Colisões', 'Tempo de Conclusão', 'Sequência', 'Velocidade', 'Saltos'],
                datasets: [{
                    data: [
                        totals.collisions,
                        totals.completionTime,
                        totals.maxStreak,
                        totals.speed,
                        totals.jumps
                    ],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(54, 162, 235, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(153, 102, 255, 0.6)',
                        'rgba(75, 192, 192, 0.6)'
                    ]
                }]
            }
        });
        document.getElementById('pieChart').style.display = 'block';
    }

    loadData();
};
