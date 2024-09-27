using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

public class capiUpdated : MonoBehaviour
{
    //movement variables
    private CharacterController player;
    private Vector3 moveDirection;
    public float gravity = 10f;
    public float jumpForce = 10f;

    //animation variable
    private Animator animator;

    //media variables
    public GameObject colScreen; //collision screen
    public AudioSource audioSource;
    public AudioClip audioClip;

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
    public static float speedConf; 
    public static int jumpsConf;

    //variables of jumpText
    public GameObject jumpText;
    public static int jumps;
    public bool finished = false;

    //variables of enemySpawn
    private EnemySpawn enemySpawn;
    public GameObject enemy;
    bool hasChanged = false;
    public GameObject relGenerator;


    public void Start()
    {
        enemySpawn = Camera.main.GetComponent<EnemySpawn>();

        Time.timeScale = menu.speed;
        jumpText.GetComponent<TMP_Text>().text = jumps.ToString();
        startTime = timeManager.GetTime();
    }

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        moveDirection = Vector3.zero;
    }

    private void Update()
    {
        foreach (GameObject enemy in enemySpawn.enemies){ // Access the enemies list from the EnemySpawn script
            hasChanged = false;

            if(enemy.transform.position.x < transform.position.x - 2){
                if(!justHit)
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

        if(colScreen != null)
        {
            if(colScreen.GetComponent<Image>().color.a > 0)
            {
                var colour = colScreen.GetComponent<Image>().color;
                colour.a -= 0.01f;
                colScreen.GetComponent<Image>().color = colour;
            }
        }





        moveDirection += Vector3.down * gravity * Time.deltaTime;

        if(player.isGrounded)
        {
            if(jumps <= 0)
            {
                finished = true;
            }

            if(finished)
            {
                endTime = timeManager.GetTime();
                timeToComplete = (endTime - startTime).TotalSeconds;
                relGenerator.GetComponent<relGenerator>().CreateRelFile(totalJumps, totalCollisions, timeToComplete, maxSequence, speedConf, jumpsConf);
                SceneManager.LoadScene("MemoryScene");
            }

            moveDirection = Vector3.down;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                totalJumps++;
                moveDirection = Vector3.up * jumpForce;
                animator.Play("capi_jumping");
            }
        }

        if(Time.timeScale < 1)
            player.Move(moveDirection * Time.unscaledDeltaTime);
        else
            player.Move(moveDirection * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider hit)
    {
        if(hit.CompareTag("Obstacle"))
        {
            wasHit();
            audioSource.Play();
            justHit = true; //related to maxSequence
            totalCollisions++;
            if(hasChanged){
                jumpText.GetComponent<TMP_Text>().text = jumps.ToString();
                hasChanged = false;
            }
        }
    }

    private void wasHit()
    {
        var colour = colScreen.GetComponent<Image>().color;
        colour.a = 0.5f;
        colScreen.GetComponent<Image>().color = colour;
    }
}
