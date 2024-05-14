using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Goal;

public class Carro : MonoBehaviour
{
    public Sprite fumo;
    private Rigidbody2D carro;
    private SpriteRenderer sR;

    public float speed = 1f;

    public float minSpeed = 8f;
    public float maxSpeed = 12f;

    void Awake()
    {
        carro = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine(Fumacinha(GoalInstance));
        speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    void FixedUpdate()
    {
        Vector2 forward = new Vector2 (transform.right.x, transform.right.y);
        carro.MovePosition(carro.position + forward * Time.fixedDeltaTime * speed);
    }

    private IEnumerator Fumacinha(MonoBehaviour instance)
    {
        for (int l = 0; l <= 1; l++)
        {
            yield return new WaitForSeconds(0.01f);
        }
        while (true)
        {
            instance.StartCoroutine(Fumo(new GameObject("FumoFumo", typeof(SpriteRenderer))));
            yield return new WaitForSeconds(0.035f);
        }
        IEnumerator Fumo(GameObject obj)
        {
            obj.GetComponent<SpriteRenderer>().sprite = fumo;
            obj.GetComponent<SpriteRenderer>().sortingOrder = 2;
            obj.transform.position = gameObject.transform.position;
            obj.transform.localScale = Vector3.one * 0.1f;
            Vector3 v = gameObject.transform.right;
            obj.transform.position = new Vector2(gameObject.transform.position.x - v.x, gameObject.transform.position.y - 0.5f);
            for (float i = 0; i <= 1; i += 0.04f)
            {
                obj.transform.position = obj.transform.position + new Vector3(0, 0.035f, 0);
                obj.transform.localScale = Vector3.one * (0.1f + (i / 3));
                Color col = obj.GetComponent<SpriteRenderer>().color;
                obj.GetComponent<SpriteRenderer>().color = new Color(col.r, col.g, col.b, 1 - i);
                yield return new WaitForSeconds(0.01f);
            }
            Destroy(obj);
        }
    }
}
