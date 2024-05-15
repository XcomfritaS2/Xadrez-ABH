using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class TutorialStart : MonoBehaviour
{
    public bool gameStarted;
    public bool gameFinished;
    public bool timerIsUnderFinal;

    public Text Timer;
    public float timer;
    public GameObject leaveButton;

    MusicNoteOne musicNoteOne;
    MusicNoteTwo musicNoteTwo;
    AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        musicNoteOne = GameObject.Find("SpawnerOne").GetComponent<MusicNoteOne>();
        musicNoteTwo = GameObject.Find("SpawnerTwo").GetComponent<MusicNoteTwo>();
        source = GameObject.Find("Timer").GetComponent<AudioSource>();
        leaveButton = GameObject.Find("ButtonSair");
        leaveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //When game is finished.

        if (gameFinished == true)
        {
            leaveButton.SetActive(true); // AQUI O ERRO

            musicNoteOne.noteOneSpeed = 0;
            musicNoteTwo.noteTwoSpeed = 0;
        }


        if (gameStarted == true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                Timer.text = "0";
                if (!gameFinished)
                    gameFinished = true;
            }
            else
            {
                Timer.text = "";
                char[] letters = timer.ToString().ToCharArray();
                int maxLetters = 0;
                if ((int)timer >= 10)
                    maxLetters = 5;
                else
                    maxLetters = 4;
                for (int i = 0; i < maxLetters; i++)
                {
                    if (i >= letters.Length)
                        continue;
                    if (letters[i] != ',')
                        Timer.text += letters[i];
                    else
                        Timer.text += ':';
                }
            }
            if (timer <= 5 && !timerIsUnderFinal)
            {
                Timer.color = Color.red;
            }
        }
    }
    public void OnButtonClick()
    {
        source.Play();

        gameStarted = true;
        musicNoteOne.OneIsDone = true;
        musicNoteTwo.TwoIsDone = true;
    }
}
