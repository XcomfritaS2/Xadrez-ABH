using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public static Goal GoalInstance;
    private GameObject peaoPlayer, p2Player;
    private Vector3 playerInitialPos, pip2;

    // Start is called before the first frame update
    public void Start()
    {
        GoalInstance = this;
    }
    void Awake()
    {
        peaoPlayer = GameObject.FindWithTag("Player");
        p2Player = GameObject.FindWithTag("Player2");
        playerInitialPos = peaoPlayer.transform.position;
        pip2 = p2Player.transform.position;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        GetComponent<SpriteRenderer>().color = Color.green;
        if (target.tag == "Player")
        {
            target.transform.position = playerInitialPos;
        }

        if (target.tag == "Player2")
        {
            target.transform.position = pip2;
        }
    }
}
