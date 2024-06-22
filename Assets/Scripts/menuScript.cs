using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class menuScript : MonoBehaviour
{
  /*  public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject menus;
    public GameObject jumpText;
    public GameObject jumpObj;
    public TMP_InputField speedField;
    public TMP_InputField jumpField;
    float timescl;
    public AudioSource audioSource;
    public AudioClip audioClip; //game music
    public AudioClip clickClip; //click sound
    public Capi capi;
    public relGenerator rel;
    private DateTime startTime;
    public TimeManager timeManager;

    public void PlayGame()
    {
        if(timescl <=0)
        {
            timescl = 1.0f;
        }

        if(jumpField.text == "")
        {
            capi.jumps = 10;
            jumpText.GetComponent<TMP_Text>().text = "10";

            if(timescl <=0)
                rel.SetConfs(10, 1.0f);
            else
                rel.SetConfs(10, timescl);
        }
        else
        {
            capi.jumps = int.Parse(jumpField.text);
            
            if(timescl <=0)
                rel.SetConfs(int.Parse(jumpField.text), 1.0f);
            else
                rel.SetConfs(int.Parse(jumpField.text), timescl);
        }
        
        startTime = timeManager.GetTime();
        capi.SetStartTime(startTime);

        //play click sound
        audioSource.clip = clickClip;
        audioSource.loop = false;
        audioSource.Play();

        //play game audio on loop
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.Play();

        menus.SetActive(false);
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        jumpObj.SetActive(true);
        Time.timeScale = timescl;
    }

    public void Options()
    {
        audioSource.clip = clickClip;
        audioSource.loop = false;
        audioSource.Play();

        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void Done()
    {
        audioSource.clip = clickClip;
        audioSource.loop = false;
        audioSource.Play();

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
    */
}
