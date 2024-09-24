using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class relGenerator : MonoBehaviour
{
    public GameObject capi;
    public TimeManager timeManager;

    public void CreateRelFile(int jumps, int collisions, double time, int maxSequence, float speed, int jumpsConf)
    {
        DateTime currentTime = DateTime.Now;
        string path = Application.dataPath + "/../Resultados/Relatorio.txt";
        
        string oldContent = "";
        if(File.Exists(path))
        {
            oldContent = File.ReadAllText(path);
           // File.WriteAllText(path, "Relatório do paciente \n\n\n");
        }

        string content = "[" + currentTime.ToString() + "] \n" +
                         "Número de colisões: " + collisions + "\n" +
                         "Tempo para conclusão da atividade: " + timeManager.FormatTime(time) + "\n" +
                         "Maior sequência de acertos: " + maxSequence + "\n" +
                         "Número de saltos dados pelo paciente: " + jumps + "\n" +
                         "Velocidade configurada na fase: " + speed + "\n" +
                         "Número de saltos configurado na fase: " + jumpsConf + "\n\n\n";

        string title = "Relatório do paciente \n\n\n";
        if (!oldContent.StartsWith(title))
        {
            oldContent = title + oldContent;
        }
                
        File.WriteAllText(path, oldContent.Replace(title, title + content));
        //File.AppendAllText(path, content);
    }

   // public void SetConfs(int jumps, float speed)
   // {
   //     this.speed = speed;
   //     this.jumps = jumps;
   // }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
