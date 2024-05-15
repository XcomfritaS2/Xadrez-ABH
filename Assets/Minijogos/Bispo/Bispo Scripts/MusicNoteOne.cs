using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNoteOne : MonoBehaviour
{
    public bool OneIsDone = false;
    public bool ActivateDelay = false;
    public GameObject musicNote;
    public string Key;

    public int noteOneSpeed;
    public bool canDestroy = false;

    public int pointsPlayerOne = 0;
    public Text scoreText;

    AudioSource source;


    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNote();
        Detect();
        scoreText.text = pointsPlayerOne.ToString() + " Pontos";

    }

    private void SpawnNote()
    {
        if (OneIsDone == true)
        {

            Instantiate(musicNote, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, transform.parent);

        }
        OneIsDone = false;
    }

    public void Detect()
    {

        if (Input.GetKeyDown("space") && OneIsDone == true)
        {
            canDestroy = true;

            pointsPlayerOne = pointsPlayerOne + 10;
            OneIsDone = true;

        }
    }
}

