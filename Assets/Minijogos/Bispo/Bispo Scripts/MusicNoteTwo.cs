using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicNoteTwo : MonoBehaviour
{
    public bool TwoIsDone = false;
    public bool ActivateDelay = false;
    public GameObject musicNote;
    public string Key;

    public int noteTwoSpeed;
    public bool canDestroy = false;

    public int pointsPlayerTwo = 0;
    public Text scoreText;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        SpawnNote();
        Detect();
        scoreText.text = pointsPlayerTwo.ToString() + " Pontos";

      
    }

    private void SpawnNote()
    {
        if (TwoIsDone == true)
        {

            Instantiate(musicNote, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, transform.parent);

        }
        TwoIsDone = false;
    }

    public void Detect()
    {

        if (Input.GetKeyDown("space") && TwoIsDone == true)
        {
            canDestroy = true;

            pointsPlayerTwo = pointsPlayerTwo + 10;
            TwoIsDone = true;
            source.Play();
        }
    }
}
