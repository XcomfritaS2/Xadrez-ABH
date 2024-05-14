using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efeito_Fluatuar : MonoBehaviour
{
    public float floatSpeed = 1f; // Velocidade de flutua��o
    public float floatHeight = 0.5f; // Altura m�xima da flutua��o
    private float startY; // Posi��o inicial no eixo Y

    void Start()
    {
        startY = transform.position.y; // Armazena a posi��o inicial no eixo Y
    }

    void Update()
    {
        // Calcula a posi��o de flutua��o usando a fun��o seno
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Atualiza a posi��o apenas no eixo Y, mantendo as posi��es X e Z inalteradas
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

