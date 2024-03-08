using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Capi : MonoBehaviour
{
    Vector2 yVelocity;

    public GameObject restartText;

    public float maxHeight;
    public float timeToPeak;

    public float jumpSpeed;
    public float gravity;

    public float groundPosition;
    public bool isGrounded = false;

    public Transform groundCheck;
    public bool isGrounded2;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //gravity = (2*maxHeight)/Mathf.Pow(timeToPeak, 2) * 2;
        //jumpSpeed = gravity*timeToPeak;
        
        Time.timeScale = 1;
        groundPosition = transform.position.y;
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded2 = Physics2D.OverlapCircle(groundCheck.position, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        yVelocity += gravity*Time.deltaTime*Vector2.down;

        if(transform.position.y <= groundPosition){
            transform.position = new Vector3(transform.position.x, groundPosition, transform.position.z);
            yVelocity = Vector3.zero;
            isGrounded = true;
        }
        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded && isGrounded2){
            yVelocity = jumpSpeed*Vector2.up;
        }
        
        transform.position += (Vector3)yVelocity*Time.deltaTime;

        if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Obstacle")
        {
            restartText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
