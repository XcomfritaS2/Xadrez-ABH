using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Efeito_Fluatuar : MonoBehaviour
{
    public float floatSpeed = 1f; // Velocidade de flutuação
    public float floatHeight = 0.5f; // Altura máxima da flutuação
    private float startY; // Posição inicial no eixo Y

    void Start()
    {
        startY = transform.position.y; // Armazena a posição inicial no eixo Y
    }

    void Update()
    {
        // Calcula a posição de flutuação usando a função seno
        float newY = startY + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Atualiza a posição apenas no eixo Y, mantendo as posições X e Z inalteradas
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

