using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class relGenerator : MonoBehaviour
{
    public GameObject capi;
    public void CreateRelFile(int jumps, int collisions, float time, int maxSequence, float speed, int jumpsConf)
    {
        string path = Application.dataPath + "/../Resultados/Relatorio.txt";
        
        if(!File.Exists(path))
        {
            File.WriteAllText(path, "Relatório do paciente \n\n");
        }

        string content = "Número de colisões: " + collisions + "\n" +
                         "Tempo para conclusão da atividade: " + time + "\n" +
                         "Maior sequência de acertos: " + maxSequence + "\n" +
                         "Número de saltos dados pelo paciente: " + jumps + "\n" +
                         "Velocidade configurada na fase: " + speed + "\n" +
                         "Saltos configurados na fase: " + jumpsConf + "\n\n\n";
                
        File.AppendAllText(path, content);
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
