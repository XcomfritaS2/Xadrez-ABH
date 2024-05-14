using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaScenas : MonoBehaviour
{
   
    public void TrocarParaCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}

