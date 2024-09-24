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
            capiUpdated.jumps = 10;
            capiUpdated.jumpsConf = 10;
        }
        else
        {
            capiUpdated.jumps = int.Parse(jumpField.text);
            capiUpdated.jumpsConf = int.Parse(jumpField.text);
        }
        
        switch(speedSlider.GetComponent<UnityEngine.UI.Slider>().value)
        {
            case 1:
                speed = 0.75f;
                break;
            case 2:
                speed = 1.0f;
                break;
            case 3:
                speed = 1.25f;
                break;
            case 4:
                speed = 1.5f;
                break;
            case 5:
                speed = 2.5f;
                break;
            default:
                speed = 1.0f;
                break;
        }

        capiUpdated.speedConf = speedSlider.GetComponent<UnityEngine.UI.Slider>().value;

       // if(speedField.text == "")
       // {
       //     speed = 1.0f;
       //     capiUpdated.speedConf = 1.0f;
       // }
       // else
       // {
       //     speed = float.Parse(speedField.text) / 100;
       //     capiUpdated.speedConf = float.Parse(speedField.text) / 100;
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
