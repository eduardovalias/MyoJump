using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngBio : MonoBehaviour
{
    public float speed;

    private 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if(transform.position.x <= -12)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
