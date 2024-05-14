using System.Collections.Generic;
using UnityEngine;


public class CharacterController : MonoBehaviour
{
    // velocidade que o personagem se move
    float speed = 9;

    // aceleração
    float walkAcceleration = 75;

    // desaceleração
    float groundDeceleration = 70;

    private CapsuleCollider2D Collider;

    public Vector2 velocity;

    public Animator animador;

    public SpriteRenderer Renderizador;

    // definindo alguns componentes
    private void Awake()
    {
        Collider = GetComponent<CapsuleCollider2D>();
        Renderizador = gameObject.GetComponent<SpriteRenderer>();
    }

    // movimento (A ser refeito em breve)
    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            Renderizador.flipX = false;
        }else if(moveInput < 0)
        {
            Renderizador.flipX = true;
        }

        animador.SetFloat("Velocidade", Mathf.Abs(moveInput));


        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, walkAcceleration * Time.deltaTime);
        }else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        }
        transform.Translate(velocity * Time.deltaTime);
    }
}
