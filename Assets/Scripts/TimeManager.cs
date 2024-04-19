using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    private int hours = 0;
    private int minutes = 0;
    private int seconds = 0;

    public DateTime GetTime()
    {
        return DateTime.Now;
    }

    public string FormatTime(double time)
    {
        if(time >= 60)
        {
            hours = (int)time / 3600;
            time = time % 3600;
            minutes = (int)time / 60;
            seconds = (int)time % 60;
        }
        else
        {
            seconds = (int)time;
        }
        return hours.ToString("D2") + ":" + minutes.ToString("D2") + ":" + seconds.ToString("D2");
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
