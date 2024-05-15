using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NoteVarsOne : MonoBehaviour
{
    MusicNoteOne musicNote;
    TutorialStart finishGameOne;

    AudioSource source;

    //Script possui sceneManagement pro botão de sair

    // Start is called before the first frame update
    void Start()
    {
        musicNote = GameObject.Find("SpawnerOne").GetComponent<MusicNoteOne>();
        finishGameOne = GameObject.Find("Timer").GetComponent<TutorialStart>();
        source = GameObject.Find("SpawnerTwo").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * musicNote.noteOneSpeed * Time.deltaTime;

        if (musicNote.canDestroy == true)
        {
            if (Input.GetKeyDown(musicNote.Key))
            {
                source.pitch = Random.Range(0.9f, 1.4f);
                source.Play();

                Destroy(gameObject);
                musicNote.canDestroy = false;
                musicNote.OneIsDone = true;

                //Point Attribution
                musicNote.pointsPlayerOne = musicNote.pointsPlayerOne + 10;

                //Speeding Up
                musicNote.noteOneSpeed = musicNote.noteOneSpeed + 5;

            }
        }

        if (musicNote.ActivateDelay == true)
        {
            StartCoroutine("Delay");
            musicNote.ActivateDelay = false;
        }

        //When game is finished.

        if(finishGameOne.gameFinished == true)
        {
            musicNote.noteOneSpeed = 0;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        musicNote.ActivateDelay = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        musicNote.canDestroy = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        musicNote.canDestroy = false;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        musicNote.OneIsDone = false;
        print("Starting Countdown: 7s / noteSpeed");
        yield return new WaitForSeconds(8.8f / musicNote.noteOneSpeed);

        //Point Removal
        musicNote.pointsPlayerOne = musicNote.pointsPlayerOne - 5;

        if(musicNote.pointsPlayerOne <= 0)
        {
            musicNote.pointsPlayerOne = 0;
        }

        //Slowing Down
        musicNote.noteOneSpeed = musicNote.noteOneSpeed - 10;

        if (musicNote.noteOneSpeed <= 1)
        {
            musicNote.noteOneSpeed = 5;
        }

        Destroy(this.gameObject);
        print("Deleted a Note!");

        musicNote.OneIsDone = true;
        
    }

    public void BotãoSair()
    {
        //Aqui seria o lugar aonde o código de salvar as peças de xadrez entraria. (Bruno)

        GameObject.Find("ButtonSair").SetActive(true);
        SceneManager.LoadScene(1);

    }
}
