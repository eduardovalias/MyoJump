using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    private Rigidbody2D rb;

    public float speed;

    Vector2 endPosition = new Vector2(12.3f, 2.58f);
    Vector2 startPosition = new Vector2(-12.3f, 2.58f);
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(transform.position.x >= 12.3)
        {
            transform.position = startPosition;
        }
    }
}
