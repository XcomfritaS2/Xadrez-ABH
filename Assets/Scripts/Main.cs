using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Imager;

public class Main : MonoBehaviour
{
    public GameObject Piece;
    public GameObject JanelaModal;

    //defini essas variáveis pra facilitar a implementação da class Imager
    public static Main MAIN { get; private set; }
    public GameObject[,] positions = new GameObject[8, 8];

    public GameObject[] playerPreto = new GameObject[16];
    public GameObject[] playerBranco = new GameObject[16];

    private string atualPlayer = "Branco";

    private bool gameOver = false;

    public void Start()
    {
        //defini a instancia do Main aqui
        MAIN = this;
    }
    public void StartMain()
    {
        /*playerBranco = new GameObject[]{
            Create("Torre_B", 0, 0), Create("Cavalo_B", 1, 0), Create("Bispo_B", 2, 0), Create("Rainha_B", 3, 0),Create("Rei_B", 4, 0), Create("Bispo_B", 5, 0),
            Create("Cavalo_B", 6, 0), Create("Torre_B", 7, 0), Create("Peao_B", 0, 1), Create("Peao_B", 1, 1), Create("Peao_B", 2, 1), Create("Peao_B", 3, 1),
            Create("Peao_B", 4, 1), Create("Peao_B", 5, 1), Create("Peao_B", 6, 1), Create("Peao_B", 7, 1)};

        playerPreto = new GameObject[]{
            Create("Torre_P", 0, 7), Create("Cavalo_P", 1, 7), Create("Bispo_P", 2, 7), Create("Rainha_P", 3, 7),Create("Rei_P", 4, 7), Create("Bispo_P", 5, 7),
            Create("Cavalo_P", 6, 7 ), Create("Torre_P", 7, 7), Create("Peao_P", 0, 6), Create("Peao_P", 1, 6), Create("Peao_P", 2, 6), Create("Peao_P", 3, 6),
            Create("Peao_P", 4, 6), Create("Peao_P", 5, 6), Create("Peao_P", 6, 6), Create("Peao_P", 7, 6)};

        for (int i = 0; i < playerPreto.Length; i++)
        {
            SetPosition(playerPreto[i]);
            SetPosition(playerBranco[i]);
        }*/
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(Piece, new Vector3(0, 0, -1), Quaternion.identity);
        Xax xa = obj.GetComponent<Xax>();
        xa.name = name;
        xa.SetXCampo(x);
        xa.SetYCampo(y);
        xa.Activate();
        xa.tag = "piece";
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Xax xa = obj.GetComponent<Xax>();

        positions[xa.GetXCampo(), xa.GetYCampo()] = obj;
    }

    public void SetPositionVazio(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionNoCampo(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return atualPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void ProxTurno()
    {
        //Modificações feitas para atender ao save
        Color32 col = IMAGER.ImageCopy.texture.GetPixel(0, 0);
        if (col.b == 255)
        {
            atualPlayer = "Branco";
        }
        else
        {
            atualPlayer = "Preto";
        }
    }

    public void Vencedor(string playerVencedor)
    {
        gameOver = true;

        GameObject.FindGameObjectWithTag("TextoVence").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("TextoVence").GetComponent<Text>().text = playerVencedor + " ganhou foi tudo";
        GameObject.FindGameObjectWithTag("TextoReset").GetComponent<Text>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}