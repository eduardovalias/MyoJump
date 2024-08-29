using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsScript : MonoBehaviour
{
    public GameObject scrollRect;
    // Start is called before the first frame update

    public void resetScrollPosition()
    {
        //reset scroll position to top when scene is loaded
        ScrollRect scrollRect = GameObject.Find("scrollArea").GetComponent<ScrollRect>();
        scrollRect.verticalNormalizedPosition = 1;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
