using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capiUpdated : MonoBehaviour
{
    private CharacterController player;
    private Vector3 moveDirection;

    public float gravity = 10f;
    public float jumpForce = 10f;

    private Animator animator;

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
        moveDirection += Vector3.down * gravity * Time.deltaTime;

        if(player.isGrounded)
        {
            moveDirection = Vector3.down;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection = Vector3.up * jumpForce;
                animator.Play("capi_jumping");
            }
        }

        player.Move(moveDirection * Time.deltaTime);
    }
}
