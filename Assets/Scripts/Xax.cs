using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static Imager;

public class Xax : MonoBehaviour
{

    public GameObject controle;
    public GameObject MovePlate;

    private int xCampo = -1;
    private int yCampo = -1;

    private string player;

    public Sprite Bispo_B, Cavalo_B, Peao_B, Rainha_B, Rei_B, Torre_B;
    public Sprite Bispo_P, Cavalo_P, Peao_P, Rainha_P, Rei_P, Torre_P;

    public void Activate()
    {
        controle = GameObject.FindGameObjectWithTag("GameController");

        SetCoords();

        switch (this.name)
        {
            case "Rainha_P": this.GetComponent<SpriteRenderer>().sprite = Rainha_P; player = "Preto"; break;
            case "Cavalo_P": this.GetComponent<SpriteRenderer>().sprite = Cavalo_P; player = "Preto"; break;
            case "Peao_P": this.GetComponent<SpriteRenderer>().sprite = Peao_P; player = "Preto"; break;
            case "Rei_P": this.GetComponent<SpriteRenderer>().sprite = Rei_P; player = "Preto"; break;
            case "Torre_P": this.GetComponent<SpriteRenderer>().sprite = Torre_P; player = "Preto"; break;
            case "Bispo_P": this.GetComponent<SpriteRenderer>().sprite = Bispo_P; player = "Preto"; break;

            case "Rainha_B": this.GetComponent<SpriteRenderer>().sprite = Rainha_B; player = "Branco"; break;
            case "Cavalo_B": this.GetComponent<SpriteRenderer>().sprite = Cavalo_B; player = "Branco"; break;
            case "Peao_B": this.GetComponent<SpriteRenderer>().sprite = Peao_B; player = "Branco"; break;
            case "Rei_B": this.GetComponent<SpriteRenderer>().sprite = Rei_B; player = "Branco"; break;
            case "Torre_B": this.GetComponent<SpriteRenderer>().sprite = Torre_B; player = "Branco"; break;
            case "Bispo_B": this.GetComponent<SpriteRenderer>().sprite = Bispo_B; player = "Branco"; break;

        }
    }

    public void SetCoords()
    {
        float x = xCampo;
        float y = yCampo;

        x *= 0.7f;
        y *= 0.7f;

        x += -2.45f;
        y += -2.5f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    public int GetXCampo()
    {
        return xCampo;
    }

    public int GetYCampo()
    {
        return yCampo;
    }

    public void SetXCampo(int x)
    {
        xCampo = x;
    }

    public void SetYCampo(int y)
    {
        yCampo = y;
    }

    private void OnMouseUp()
    {
        if (!controle.GetComponent<Main>().IsGameOver() && controle.GetComponent<Main>().GetCurrentPlayer() == player)
        {
            DestroyMovePlates();

            InitiateMovePlates();
        }
        else
        {
            DestroyMovePlates();
        }
    }

    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "Rainha_P":
            case "Rainha_B":
                LinhaMovePlate(1, 0);
                LinhaMovePlate(0, 1);
                LinhaMovePlate(1, 1);
                LinhaMovePlate(-1, 0);
                LinhaMovePlate(0, -1);
                LinhaMovePlate(-1, -1);
                LinhaMovePlate(-1, 1);
                LinhaMovePlate(1, -1);
                break;

            case "Cavalo_P":
            case "Cavalo_B":
                LMovePlate();
                break;

            case "Bispo_P":
            case "Bispo_B":
                LinhaMovePlate(1, 1);
                LinhaMovePlate(1, -1);
                LinhaMovePlate(-1, 1);
                LinhaMovePlate(-1, -1);
                break;

            case "Rei_P":
            case "Rei_B":
                EnvoltaMovePlate();
                break;

            case "Torre_P":
            case "Torre_B":
                LinhaMovePlate(1, 0);
                LinhaMovePlate(0, 1);
                LinhaMovePlate(-1, 0);
                LinhaMovePlate(0, -1);
                break;


            case "Peao_P":
                PeaoMovePlate(xCampo, yCampo - 1, -1, 6);
                break;
            case "Peao_B":
                PeaoMovePlate(xCampo, yCampo + 1, 1, 1);
                break;
        }
    }

    public void LinhaMovePlate(int xIncrement, int yIncrement)
    {
        Main sc = controle.GetComponent<Main>();

        int x = xCampo + xIncrement;
        int y = yCampo + yIncrement;

        while (sc.PositionNoCampo(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y, false);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionNoCampo(x, y) && sc.GetPosition(x, y).GetComponent<Xax>().player != player)
        {
            MovePlateAtaqueSpawn(x, y, true);
        }
    }

    public void LMovePlate()
    {
        PontoMovePlate(xCampo + 1, yCampo + 2);
        PontoMovePlate(xCampo - 1, yCampo + 2);
        PontoMovePlate(xCampo + 2, yCampo + 1);
        PontoMovePlate(xCampo + 2, yCampo - 1);
        PontoMovePlate(xCampo + 1, yCampo - 2);
        PontoMovePlate(xCampo - 1, yCampo - 2);
        PontoMovePlate(xCampo - 2, yCampo + 1);
        PontoMovePlate(xCampo - 2, yCampo - 1);
    }

    public void EnvoltaMovePlate()
    {
        PontoMovePlate(xCampo, yCampo + 1);
        PontoMovePlate(xCampo, yCampo - 1);
        PontoMovePlate(xCampo - 1, yCampo - 1);
        PontoMovePlate(xCampo - 1, yCampo - 0);
        PontoMovePlate(xCampo - 1, yCampo + 1);
        PontoMovePlate(xCampo + 1, yCampo - 1);
        PontoMovePlate(xCampo + 1, yCampo - 0);
        PontoMovePlate(xCampo + 1, yCampo + 1);
    }

    public void PontoMovePlate(int x, int y)
    {
        Main sc = controle.GetComponent<Main>();
        if (sc.PositionNoCampo(x, y))
        {
            GameObject xp = sc.GetPosition(x, y);

            if (xp == null)
            {
                MovePlateSpawn(x, y, false);
            }
            else if (xp.GetComponent<Xax>().player != player)
            {
                MovePlateAtaqueSpawn(x, y, true);
            }
        }
    }

    public void PeaoMovePlate(int x, int y, int d, int inicial)
    {
        Main sc = controle.GetComponent<Main>();
        if (sc.PositionNoCampo(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y, false);
            }

            if ((yCampo == inicial) && sc.GetPosition(x, y + d) == null)
            {
                MovePlateSpawn(x, y + d, false);
            }

            if (sc.PositionNoCampo(x + 1, y) && sc.GetPosition(x + 1, y) != null &&
                sc.GetPosition(x + 1, y).GetComponent<Xax>().player != player)
            {
                MovePlateAtaqueSpawn(x + 1, y, true);
            }

            if (sc.PositionNoCampo(x - 1, y) && sc.GetPosition(x - 1, y) != null &&
                sc.GetPosition(x - 1, y).GetComponent<Xax>().player != player)
            {
                MovePlateAtaqueSpawn(x - 1, y, true);
            }
        }
    }

    public void MovePlateSpawn(int matrixX, int matrixY, bool atk)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.7f;
        y *= 0.7f;

        x += -2.45f;
        y += -2.5f;

        GameObject mp = Instantiate(MovePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.ataque = false;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    public void MovePlateAtaqueSpawn(int matrixX, int matrixY, bool atk)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 0.7f;
        y *= 0.7f;

        x += -2.45f;
        y += -2.5f;

        GameObject mp = Instantiate(MovePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.ataque = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
