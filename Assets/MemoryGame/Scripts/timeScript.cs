using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timeScript : MonoBehaviour
{
    //public TMP_Text counterText;
	public bool timeCounter = true;
	public float seconds, minutes;

	// Use this for initialization
	void Start () 
    {
		//counterText = GetComponent<TMP_Text>().text;
	}
	
	// Update is called once per frame
	void Update () 
    {
		if (timeCounter) {
			seconds = (int)(Time.timeSinceLevelLoad % 60f);
			GetComponent<TMP_Text>().text = seconds.ToString ("00") + " segundos";
		}
	}

	public void endGame() 
    {
		timeCounter = false;
		GetComponent<TMP_Text>().color = Color.yellow;
	}
}
