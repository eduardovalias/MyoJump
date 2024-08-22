using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class sliderScript : MonoBehaviour
{
    public TMP_Text speedText;
    public GameObject speedSlider;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //FUNCTION TO CHANGE THE TEXT OF THE SPEED TEXT WHEN SLIDEBAR CHANGES VALUE
    public void changeSpeedText()
    {
        speedText.text = speedSlider.GetComponent<UnityEngine.UI.Slider>().value.ToString();
    }
}
