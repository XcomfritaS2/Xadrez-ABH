using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        if (ataque)
        {
            GameObject xp = controle.GetComponent<Main>().GetPosition(matrixX, matrixY);

            if (xp.name == "Rei_B") controle.GetComponent<Main>().Vencedor("Preto");
            if (xp.name == "Rei_P") controle.GetComponent<Main>().Vencedor("Branco");

            Destroy(xp);
        }

        controle.GetComponent<Main>().SetPositionVazio(reference.GetComponent<Xax>().GetXCampo(),
            reference.GetComponent<Xax>().GetComponent<Xax>().GetYCampo());

        reference.GetComponent<Xax>().SetXCampo(matrixX);
        reference.GetComponent<Xax>().SetYCampo(matrixY);
        reference.GetComponent<Xax>().SetCoords();

        controle.GetComponent<Main>().SetPosition(reference);

        controle.GetComponent<Main>().ProxTurno();

        reference.GetComponent<Xax>().DestroyMovePlates();
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
