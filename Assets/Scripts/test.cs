using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float jumpForce = 20;
    public float gravity = -9.8f;
    public LayerMask groundLayer; // Adiciona uma camada para o chão
    private Rigidbody2D rigid;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();
        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void ApplyGravity()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + gravity * Time.deltaTime);
    }

    private void CheckGround()
    {
        // Verifica se o objeto está tocando o chão usando Raycast
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(1, 0.1f), 0, Vector2.down, 0.2f, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * 0.2f, Color.red); // Visualiza o Raycast no editor
        if (hit.collider != null)
        {
            isGrounded = true;
            rigid.velocity = new Vector2(rigid.velocity.x, 0); // Para o movimento descendente
            transform.position = new Vector2(transform.position.x, hit.point.y + 0.1f); // Ajusta a posição para ficar no chão
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Jump()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
    }
}
