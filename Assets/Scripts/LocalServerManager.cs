using System.Diagnostics;
using UnityEngine;

public class LocalServerManager : MonoBehaviour
{
    private Process serverProcess;

    // Método para iniciar o servidor e abrir o navegador
    public void StartLocalServer()
    {
        // Verifica se o servidor já está em execução
        if (serverProcess != null && !serverProcess.HasExited)
        {
            UnityEngine.Debug.Log("O servidor já está em execução.");
            return;
        }

        // Configura e inicia o processo do servidor local
        serverProcess = new Process();
        serverProcess.StartInfo.FileName = "cmd.exe";
        serverProcess.StartInfo.Arguments = "/c http-server . -p 8080";  // Ajuste o caminho, se necessário
        serverProcess.StartInfo.WorkingDirectory = Application.dataPath;  // Usa a pasta Assets como diretório de trabalho
        serverProcess.StartInfo.CreateNoWindow = true;
        serverProcess.StartInfo.UseShellExecute = false;
        serverProcess.Start();

        UnityEngine.Debug.Log("Servidor local iniciado.");

        // Abre o navegador apontando para o servidor
        OpenBrowser("http://localhost:8080");
    }

    // Método para abrir o navegador no endereço especificado
    private void OpenBrowser(string url)
    {
        Application.OpenURL(url);
    }

    // Método para encerrar o servidor local manualmente, se necessário
    public void StopLocalServer()
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            serverProcess.Kill();
            serverProcess.Dispose();
            serverProcess = null;
            UnityEngine.Debug.Log("Servidor local encerrado.");
        }
    }

    // Encerra o servidor automaticamente ao fechar o jogo
    private void OnApplicationQuit()
    {
        StopLocalServer();
    }
}
