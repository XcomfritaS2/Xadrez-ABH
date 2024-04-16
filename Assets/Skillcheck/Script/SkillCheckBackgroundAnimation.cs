using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TextureFunction;

public class SkillCheckBackgroundAnimation : MonoBehaviour
{
    private Image BackgroundImage { get => GetComponent<Image>(); }
    public bool continueAnimation = true;
    public float timer, counter;
    public int velX, velY;
    void Start()
    {
        //timer = 1;
    }
    void Update()
    {
        if (continueAnimation)
            counter += Time.deltaTime;
        else
            counter = 0;
        if (counter >= timer && continueAnimation)
        {
            counter = 0f;
            BackgroundImage.sprite = MovePixelsInSprite(BackgroundImage.sprite, velX, velY);
        }
    }
}
