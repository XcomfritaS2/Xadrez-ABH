using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NoteVarsTwo : MonoBehaviour
{
    MusicNoteTwo musicNote;
    TutorialStart finishGameTwo;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        musicNote = GameObject.Find("SpawnerTwo").GetComponent<MusicNoteTwo>();
        finishGameTwo = GameObject.Find("Timer").GetComponent<TutorialStart>();
        source = GameObject.Find("SpawnerTwo").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * musicNote.noteTwoSpeed * Time.deltaTime;

        if (musicNote.canDestroy == true)
        {
            if (Input.GetKeyDown(musicNote.Key))
            {
                source.pitch = Random.Range(0.9f, 1.4f);
                source.Play();

                Destroy(gameObject);
                musicNote.canDestroy = false;
                musicNote.TwoIsDone = true;

                //Point Attribution
                musicNote.pointsPlayerTwo = musicNote.pointsPlayerTwo + 10;

                //Speeding Up
                musicNote.noteTwoSpeed = musicNote.noteTwoSpeed + 5;

            }
        }

        if (musicNote.ActivateDelay == true)
        {
            StartCoroutine("Delay");
            musicNote.ActivateDelay = false;
        }

        //When game is finished.

        if (finishGameTwo.gameFinished == true)
        {
            musicNote.noteTwoSpeed = 0;
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
        musicNote.TwoIsDone = false;
        print("Starting Countdown: 7s / noteSpeed");
        yield return new WaitForSeconds(8.8f / musicNote.noteTwoSpeed);

        //Point Removal
        musicNote.pointsPlayerTwo = musicNote.pointsPlayerTwo - 5;

        if (musicNote.pointsPlayerTwo <= 0)
        {
            musicNote.pointsPlayerTwo = 0;
        }

        //Slowing Down
        musicNote.noteTwoSpeed = musicNote.noteTwoSpeed - 10;

        if (musicNote.noteTwoSpeed <= 1)
        {
            musicNote.noteTwoSpeed = 5;
        }

        Destroy(this.gameObject);
        print("Deleted a Note!");

        musicNote.TwoIsDone = true;

    }
}