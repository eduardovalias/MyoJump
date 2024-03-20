using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class menuScript : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject jumpText;
    public GameObject jumpObj;
    public TMP_InputField speedField;
    public TMP_InputField jumpField;
    float timescl;

    public void PlayGame()
    {
        if(timescl <=0)
        {
            timescl = 1.0f;
        }

        if(jumpField.text == "")
        {
            jumpText.GetComponent<TMP_Text>().text = "10";
        }

        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        jumpObj.SetActive(true);
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

        if(speedField.text != "")
        {
            timescl = float.Parse(speedField.text);
            timescl = timescl/100;
        }
        else 
            timescl = 1.0f;

        jumpText.GetComponent<TMP_Text>().text = jumpField.text;
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
