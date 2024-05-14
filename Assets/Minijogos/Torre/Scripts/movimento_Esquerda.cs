using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimento_Esquerda : MonoBehaviour
{
   
    public float speed = 2.0f; // Velocidade de movimento
    public float minX = -5.0f; // Limite esquerdo
    public float maxX = 5.0f; // Limite direito

    private float targetX; // Alvo atual (minX ou maxX)
    private bool movingRight = true; // Dire��o de movimento

    void Start()
    {
        // Come�a movendo para a direita
        targetX = maxX;
    }

    void Update()
    {
        // Move o objeto na dire��o certa
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), step);

        // Verifica se o objeto chegou ao destino e muda a dire��o
        if (transform.position.x == targetX)
        {
            if (movingRight)
            {
                targetX = minX;
            }
            else
            {
                targetX = maxX;
            }
            movingRight = !movingRight;
        }
    }
}

