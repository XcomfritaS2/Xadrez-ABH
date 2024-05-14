using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Jog2 : MonoBehaviour
{
    private bool canMove = true;
    public Rigidbody2D peao;
    public static Jog2 Jog2Instance;

    public float min_X = -8.5f, max_X = 8.5f, min_Y = -5f;

    public Vector3 playerStartPosition;
    public void Start()
    {
        Jog2Instance = this;
    }
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

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                peao.MovePosition(peao.position + Vector2.right);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                peao.MovePosition(peao.position + Vector2.left);
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                peao.MovePosition(peao.position + Vector2.up);
            }
        }
    }

    void CheckBounds()
    {
        Vector2 temp = transform.position;

        if (temp.x > max_X)
        {
            temp.x = max_X;
        }

        if (temp.x < min_X)
        {
            temp.x = min_X;
        }

        if (temp.y <= min_Y)
        {
            temp.y = -5f;
        }

        transform.position = temp;
    }
    void ResetPlayerPosition()
    {
        peao.MovePosition(playerStartPosition);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        // Se o peão bater no carro, a cena carregará novamente
        if (target.tag == "carro")
        {
            StopAllCoroutines();
            StartCoroutine(Animations(this, 
                () => { canMove = false; }, 
                () => { canMove = true; } ));
            ResetPlayerPosition();
        }
    }

    public static IEnumerator Animations(MonoBehaviour instance, Action func1, Action func2)
    {
        func1.Invoke();
        for (float i = 0; i <= 1; i += 0.04f)
        {
            Color color = instance.gameObject.GetComponent<SpriteRenderer>().color;
            instance.gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, i);
            yield return new WaitForSeconds(0.01f);
        }
        func2.Invoke();
    }
}
