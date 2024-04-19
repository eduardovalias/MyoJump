using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class relGenerator : MonoBehaviour
{
    public GameObject capi;
    private int jumps = 0;
    private float speed = 0.0f;

    public void CreateRelFile(int jumps, int collisions, double time, int maxSequence, float speed, int jumpsConf)
    {
        DateTime currentTime = DateTime.Now;
        speed = this.speed * 100;
        jumpsConf = this.jumps;
        string path = Application.dataPath + "/../Resultados/Relatorio.txt";
        
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "Relatório do paciente \n\n\n");
        }

        string content = "[" + currentTime.ToString() + "] \n" +
                         "Número de colisões: " + collisions + "\n" +
                         "Tempo para conclusão da atividade: " + time + "\n" +
                         "Maior sequência de acertos: " + maxSequence + "\n" +
                         "Número de saltos dados pelo paciente: " + jumps + "\n" +
                         "Velocidade configurada na fase: " + speed + "% \n" +
                         "Número de saltos configurado na fase: " + jumpsConf + "\n\n\n";
                
        File.AppendAllText(path, content);
    }

    public void SetConfs(int jumps, float speed)
    {
        this.speed = speed;
        this.jumps = jumps;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
