using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class menu : MonoBehaviour
{
    public TMP_InputField speedField;
    public GameObject speedSlider;
    public TMP_InputField jumpField;
    public static float speed;

    public void PlayGame()
    {
        if(jumpField.text == "")
        {
            Capi.jumps = 10;
            Capi.jumpsConf = 10;
        }
        else
        {
            Capi.jumps = int.Parse(jumpField.text);
            Capi.jumpsConf = int.Parse(jumpField.text);
        }
        
        speed = (float)speedSlider.GetComponent<UnityEngine.UI.Slider>().value;
        Capi.speedConf = speed;

       // if(speedField.text == "")
       // {
       //     speed = 1.0f;
       //     Capi.speedConf = 1.0f;
       // }
       // else
       // {
       //     speed = float.Parse(speedField.text) / 100;
       //     Capi.speedConf = float.Parse(speedField.text) / 100;
       // }
    }//

    // Start is called before the first frame update
    void Start()    
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
