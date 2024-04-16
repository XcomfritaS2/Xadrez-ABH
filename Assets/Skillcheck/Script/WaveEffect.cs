using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TextureFunction;

public class WaveEffect : MonoBehaviour
{
    //esse é pra rodar nesse monobehaviour
    public void StartOnda(Transform parent, int initialSize, int finalSize, Color waveColor, float time)
    {
        StartCoroutine(Effect(parent, initialSize, finalSize, waveColor, time));
    }
    //mais um do pasce pq pai é pai né
    private IEnumerator Effect(Transform parent, int initialSize, int finalSize, Color waveColor, float time)
    {
        //obj creation
        Texture2D waveTexture;
        waveTexture = new(finalSize, finalSize);
        GameObject obj = new("Wave");
        obj.transform.SetParent(parent);
        obj.AddComponent<RectTransform>();
        obj.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;

        obj.GetComponent<RectTransform>().localScale = Vector3.one * 1.5f;

        obj.AddComponent<Image>();
        obj.GetComponent<Image>().color = waveColor;

        //obj.transform.parent = GAME.transform;
        float alper = (1 / ((float)finalSize - (float)initialSize));
        float alper2 = 0;
        for (int i = initialSize; i <= finalSize; i++)
        {
            //clear texture
            for (int x = 0; x < waveTexture.width; x++)
            {
                for (int y = 0; y < waveTexture.height; y++)
                {
                    waveTexture.SetPixel(x, y, Color.clear);
                }
            }
            waveTexture.Apply();

            // draw circle
            DrawCircle(ref waveTexture, new Color(0.9f, 0.9f, 0.9f, 1 - alper2), waveTexture.width / 2, waveTexture.height / 2, i / 2);

            // remove previous circle
            DrawCircle(ref waveTexture, Color.clear, waveTexture.width / 2, waveTexture.height / 2, (i / 2) - 1);

            //texture configuration
            waveTexture.filterMode = FilterMode.Point;
            if (waveTexture.width % 4 == 0)
                waveTexture.Compress(false);
            waveTexture.Apply();

            //sprite creation and application
            Sprite finalSprite = Sprite.Create(waveTexture, new Rect(0, 0, waveTexture.width, waveTexture.height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0), true);
            obj.GetComponent<Image>().sprite = finalSprite;

            //loop continuity
            alper2 += alper;
            yield return new WaitForSeconds(time / ((float)finalSize - (float)initialSize));
        }
        Destroy(obj);
    }
}
