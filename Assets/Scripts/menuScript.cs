using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public TMP_InputField speedField;
    float timescl;

    public void PlayGame()
    {
        if(timescl <=0)
        {
            timescl = 1.0f;
        }

        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = timescl;
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Done()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        timescl = float.Parse(speedField.text);
        timescl = timescl/100;
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
