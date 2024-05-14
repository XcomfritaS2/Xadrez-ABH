using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerScreenManager : MonoBehaviour
{
    public Text winnerText;
    public Canvas winnerCanvas; // Referência ao Canvas que contém a imagem e o texto do vencedor
    private bool gamePaused = false;

    void Start()
    {
        // Desativa o Canvas no início do jogo
        winnerCanvas.gameObject.SetActive(false);
    }

    public void ShowWinner(string winnerName)
    {
        // Ativa o Canvas e exibe o nome do vencedor
        winnerCanvas.gameObject.SetActive(true);
        winnerText.text = "O vencedor é: " + winnerName;

        // Pausa o jogo
        Time.timeScale = 0;
        gamePaused = true;
    }



    public void ResumeGame() // caso o jogo seja mais de uma rodada configurar essa parte ;]
    {
        // Retoma o jogo
        Time.timeScale = 1;
        gamePaused = false;
        // Esconde o Canvas
        winnerCanvas.gameObject.SetActive(false);
    }


    public void HideWinner()
    {
        // Desativa o Canvas
        winnerCanvas.gameObject.SetActive(false);
    }
}