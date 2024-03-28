using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public void LoadProxLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int IndexLevel)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(IndexLevel);
    }
}
