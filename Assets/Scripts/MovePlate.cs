using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Imager;

public class MovePlate : MonoBehaviour
{
    public GameObject controle;
    GameObject reference = null;

    int matrixX;
    int matrixY;

    public bool ataque = false;

    public void Start()
    {
        if (ataque)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controle = GameObject.FindGameObjectWithTag("GameController");
        GameObject xp = controle.GetComponent<Main>().GetPosition(matrixX, matrixY);

        

        if (ataque)
        {
            /*if (xp.name == "Rei_B") controle.GetComponent<Main>().Vencedor("Preto");
            if (xp.name == "Rei_P") controle.GetComponent<Main>().Vencedor("Branco");*/

            Debug.Log("Ocorreu um ataque na peça:" + xp.name);
            controle.GetComponent<Main>().JanelaModal.SetActive(true);

        }
        else
        {
            controle.GetComponent<Main>().SetPositionVazio(reference.GetComponent<Xax>().GetXCampo(),
                reference.GetComponent<Xax>().GetComponent<Xax>().GetYCampo());

            reference.GetComponent<Xax>().SetXCampo(matrixX);
            reference.GetComponent<Xax>().SetYCampo(matrixY);
            reference.GetComponent<Xax>().SetCoords();

            controle.GetComponent<Main>().SetPosition(reference);
            xp = controle.GetComponent<Main>().GetPosition(matrixX, matrixY);

            Debug.Log("Você clicou na peça:" + xp.name);

            //atualiza os assets
            IMAGER.UpdateImage();

            controle.GetComponent<Main>().ProxTurno();

            reference.GetComponent<Xax>().DestroyMovePlates();
        }
    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
