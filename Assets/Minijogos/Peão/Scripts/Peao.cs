using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Jog2;

public class Peao : MonoBehaviour
{
    public Rigidbody2D peao;

    public Vector3 playerStartPosition;

    private bool canMove = true;
    void Awake()
    {
        peao = GetComponent<Rigidbody2D>();
        playerStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            CheckBounds();

            if (Input.GetKeyDown(KeyCode.D))
            {
                peao.MovePosition(peao.position + Vector2.right);
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                peao.MovePosition(peao.position + Vector2.left);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                peao.MovePosition(peao.position + Vector2.up);
            }
        }
    }

    void CheckBounds()
    {
        Vector2 temp = transform.position;

        //Jog2Instance é o Jog2, estou pegando as variáveis dele
        if (temp.x > Jog2Instance.max_X)
        {
            temp.x = Jog2Instance.max_X;
        }

        if (temp.x < Jog2Instance.min_X)
        {
            temp.x = Jog2Instance.min_X;
        }

        if (temp.y <= Jog2Instance.min_Y)
        {
            temp.y = -5f;
        }

        transform.position = temp;
    }

    void ResetPlayerPosition()
    {
        StopAllCoroutines();
        StartCoroutine(Animations(this, () => { canMove = false; }, () => { canMove = true; }));
        peao.MovePosition(playerStartPosition);

        //StartCoroutine(ResetAnimator());
    }

    IEnumerator ResetAnimator()
    {
        yield return new WaitForSeconds(0.1f);

        canMove = false;
        yield return new WaitForSeconds(1f);
        canMove = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "carro")
        {
            ResetPlayerPosition();
        }
    }
}
