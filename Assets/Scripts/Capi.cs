using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

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

    public GameObject jumpText;
    public int jumps;
    public bool finished = false;

    private EnemySpawn enemySpawn;
    public GameObject enemy;
    bool hasChanged = false;
    public GameObject relGenerator;

    //variables of relGenerator
    private int totalJumps = 0; 
    private int totalCollisions = 0; 
    private double timeToComplete; //how to add timer to this?
    private DateTime startTime;
    private DateTime endTime;
    public TimeManager timeManager;
    private int maxSequence = 0; 
    private int actualSequence = 0;
    private bool justHit = false;
    private float speedConf = 0; 
    private int jumpsConf = 0;
    public AudioSource audioSource;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        enemySpawn = Camera.main.GetComponent<EnemySpawn>();
        //gravity = (2*maxHeight)/Mathf.Pow(timeToPeak, 2) * 2;
        //jumpSpeed = gravity*timeToPeak;
        
        Time.timeScale = 0;
        groundPosition = transform.position.y;
        rb = this.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }
    
    public void SetStartTime(DateTime time)
    {
        startTime = time;
    }

    void FixedUpdate()
    {
        isGrounded2 = Physics2D.OverlapCircle(groundCheck.position, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject enemy in enemySpawn.enemies){ // Access the enemies list from the EnemySpawn script
            hasChanged = false;

            if(enemy.transform.position.x < transform.position.x){
                jumps -= 1;
                hasChanged = true;
                jumpText.GetComponent<TMP_Text>().text = jumps.ToString();
                enemySpawn.enemies.Remove(enemy); // Remove the enemy from the list

                actualSequence++;
                if(!justHit)
                {
                    if(maxSequence < actualSequence)
                    {
                        maxSequence = actualSequence;
                    }
                }
                else
                {
                    actualSequence--;
                    actualSequence = 0;
                    justHit = false;
                }
                break;
            }
        }




        

        yVelocity += gravity*Time.deltaTime*Vector2.down;

        if(transform.position.y <= groundPosition){
            transform.position = new Vector3(transform.position.x, groundPosition, transform.position.z);
            yVelocity = Vector3.zero;
            isGrounded = true;
        }
        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded && isGrounded2){
            yVelocity = jumpSpeed*Vector2.up;
            totalJumps++;
        }
        
        transform.position += (Vector3)yVelocity*Time.deltaTime;

        if(jumps <= 0 && !isGrounded2)
        {
            finished = true;
        }

        if(finished && isGrounded2)
        {
            endTime = timeManager.GetTime();
            timeToComplete = (endTime - startTime).TotalSeconds;
            relGenerator.GetComponent<relGenerator>().CreateRelFile(totalJumps, totalCollisions, timeToComplete, maxSequence, speedConf, jumpsConf);
            restartText.SetActive(true);
            Time.timeScale = 0;
            enabled = false;
        }

        //if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R))
        //{
        //    SceneManager.LoadScene(0);
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Obstacle")
        {
            audioSource.Play();
            justHit = true; //related to maxSequence
            totalCollisions++;
            jumps += 1;
            if(hasChanged){
                jumpText.GetComponent<TMP_Text>().text = jumps.ToString();
                hasChanged = false;
            }
            //jumpText.GetComponent<TMP_Text>().text = jumps.ToString();
            //restartText.SetActive(true);
            //Time.timeScale = 0;
        }
    }
}
