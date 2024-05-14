using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TextureFunction;

public class EffectsLibrary : MonoBehaviour
{
    #region Efeitos
    //direto do jogo de pesca esse aqui
    public static IEnumerator FadeCanvasObj(Image obj, float time, bool up)
    {
        var cor = obj.color;
        //fadeout
        if (up)
        {
            for (float i = 1; i > 0; i -= 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, 0);
        }
        //fadein
        else
        {
            for (float i = 0; i < 1; i += 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, 1);
        }
    }
    //Faz efeito de fadeout ou fadein automaticamente
    public static IEnumerator SmoothAlphaChange(Image obj, float alpha, float time)
    {
        var cor = obj.color;
        if (obj.color.a < alpha)
        {
            for (float i = obj.color.a; i < alpha; i += 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, alpha);
        }
        else
        {
            for (float i = obj.color.a; i > alpha; i -= 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, alpha);
        }
    }
    //Faz efeito de fadeout ou fadein automaticamente para textos
    public static IEnumerator SmoothAlphaChange(TextMeshProUGUI obj, float alpha, float time)
    {
        var cor = obj.color;
        if (obj.color.a < alpha)
        {
            for (float i = obj.color.a; i < alpha; i += 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, alpha);
        }
        else
        {
            for (float i = obj.color.a; i > alpha; i -= 0.1f / time)
            {
                obj.color = new Color(cor.r, cor.g, cor.b, i);
                yield return new WaitForSeconds(0.1f);
            }
            obj.color = new Color(cor.r, cor.g, cor.b, alpha);
        }
    }
    //faz o background ir de mais pixelado pra menos pixelado
    public static IEnumerator PixelEfeito(Image obj, float time, bool up)
    {
        //fadeout
        if (up)
        {
            for (float i = 1; i > 0; i -= 0.1f / time)
            {
                obj.pixelsPerUnitMultiplier = i;
                yield return new WaitForSeconds(0.1f);
            }
            obj.pixelsPerUnitMultiplier = 0;
        }
        //fadein
        else
        {
            for (float i = 0; i < 1; i += 0.1f / time)
            {
                obj.pixelsPerUnitMultiplier = i;
                yield return new WaitForSeconds(0.1f);
            }
            obj.pixelsPerUnitMultiplier = 1;
        }
    }
    //faz os numeros ficarem grandes e diminuirem
    public static IEnumerator SetSizeSmoothDecrease(RectTransform objTransform, float multiplier, float time)
    {
        objTransform.localScale = Vector3.one * multiplier;
        for (float i = multiplier; i >= 1; i -= 0.1f)
        {
            objTransform.localScale = Vector3.one * i;
            yield return new WaitForSeconds(0.1f / time);
        }
        objTransform.localScale = Vector3.one;
    }
    //executa algo a cada tempo definido pelo tempo
    public static void ExecuteNumberOfTimes(MonoBehaviour instance, float time, int times, Action action)
    {
        instance.StartCoroutine(_ExecuteNumberOfTimes(time, times, action));
        IEnumerator _ExecuteNumberOfTimes(float time, int times, Action action)
        {
            for (int i = 0; i < times; i++)
            {
                action();
                yield return new WaitForSeconds(time);
            }
        }
    }
    #endregion
}
