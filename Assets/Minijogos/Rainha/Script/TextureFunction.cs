using UnityEngine;
using System.IO;
using System;

public sealed class TextureFunction : MonoBehaviour
{
    /// <summary>
    /// Fun��o que retorna a textura da textura fornecida
    /// </summary>
    /// <returns>A mesma textura fornecida.</returns>
    public static Texture2D GetTexture(Texture2D toReceive)
    {
        Texture2D toApply; 
        toApply = new Texture2D(toReceive.width, toReceive.height);
        for (int x = 0; x < toApply.width; x++)
        {
            for (int y = 0; y < toApply.height; y++)
            {
                toApply.SetPixel(x, y, toReceive.GetPixel(x, y));
            }
        }
        toApply.Apply();
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return toApply;
    }
    /// <summary>
    /// Fun��o que retorna a textura da textura fornecida.
    /// </summary>
    [Obsolete("M�todo ultrapassado por passagens por refer�ncias")]
    public static void TextureToOther(ref Texture2D toApply, Texture2D toReceive)
    {
        toApply = new Texture2D(toReceive.width, toReceive.height);
        for (int x = 0; x < toApply.width; x++)
        {
            for (int y = 0; y < toApply.height; y++)
            {
                toApply.SetPixel(x, y, toReceive.GetPixel(x, y));
            }
        }
        Resources.UnloadUnusedAssets();
        GC.Collect();
        toApply.Apply();
    }
    /// <summary>
    /// Fun��o que transforma Color.black (sem considerar o alfa) em Color.clear
    /// </summary>
    /// <returns>Nova textura.</returns>
    public static Texture2D TurnBlackToClear(Texture2D texture)
    {
        Texture2D tex; 
        tex = GetTexture(texture);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                if (tex.GetPixel(x, y).r == 0 && tex.GetPixel(x, y).g == 0 && tex.GetPixel(x, y).b == 0)
                {
                    tex.SetPixel(x, y, Color.clear);
                }
            }
        }
        tex.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Fun��o que usa o RGB como base para alfa
    /// </summary>
    /// <returns>Nova textura.</returns>
    [Obsolete("M�todo falho")]
    public static Texture2D TurnBlurIntoClear(Texture2D texture)
    {
        Texture2D tex; 
        tex = GetTexture(texture);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                float a = (tex.GetPixel(x, y).r + tex.GetPixel(x, y).g + tex.GetPixel(x, y).b) / 3;
                tex.SetPixel(x, y, new Color(tex.GetPixel(x, y).r, tex.GetPixel(x, y).g, tex.GetPixel(x, y).b, a));
            }
        }
        tex.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Fun��o que usa o elemento red do alphaTex para deixar pixeis transparentes.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="alphaTex">Textura preto e branca ou com gradiente.</param>
    /// <returns>Nova textura transparente.</returns>
    public static Texture2D GetWhiteAlpha(Texture2D texture, Texture2D alphaTex)
    {
        Texture2D tex;
        tex = GetTexture(texture);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                tex.SetPixel(x, y, new Color(tex.GetPixel(x, y).r, tex.GetPixel(x, y).g, tex.GetPixel(x, y).b, alphaTex.GetPixel(x, y).r));
            }
        }
        tex.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Fun��o que transforma uma cor em outra em uma textura.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="col1">Cor que ser� alterada.</param>
    /// <param name="col2">Cor que ir� substituir.</param>
    /// <returns>Nova textura com a cor alterada.</returns>
    public static Texture2D TurnColorTo(Texture2D texture, Color col1, Color col2)
    {
        Texture2D tex; 
        tex = GetTexture(texture);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                if (texture.GetPixel(x, y) == col1)
                    tex.SetPixel(x, y, col2);
            }
        }
        tex.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Fun��o que expande cores exceto preto.
    /// </summary>
    /// <returns>Nova textura com as cores alteradas.</returns>
    [Obsolete("M�todo sem fun��o")]
    public static Texture2D ExpandColorsWhitoutBlack(Texture2D texture)
    {
        Texture2D tex;
        tex = GetTexture(texture);
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {

            }
        }
        tex.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Transforma cores diversas em preto ou branco.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="which">Se verdadeiro, ser� branco, se falso, ser� preto.</param>
    /// <returns>Nova textura com as cores alteradas.</returns>
    public static void OtherColorsToTwo(Texture2D texture, bool which)
    {
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (textureComparative.GetPixel(x, y) != Color.black && textureComparative.GetPixel(x, y) != Color.white)
                {
                    if (which == true)
                        texture.SetPixel(x, y, Color.white);
                    else
                        texture.SetPixel(x, y, Color.black);
                }
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Fun��o que transforma todas as cores em preto exceto a cor fornecida, que ser� branco.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="color">A cor que ser� usada para criar branco</param>
    /// <returns>Nova textura com as cores alteradas.</returns>
    public static Texture2D PreserveOneColor(Texture2D texture, Color color)
    {
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (textureComparative.GetPixel(x, y) != color)
                    textureComparative.SetPixel(x, y, Color.black);
                if (textureComparative.GetPixel(x, y) == color)
                    textureComparative.SetPixel(x, y, Color.white);
            }
        }
        textureComparative.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return textureComparative;
    }
    /// <summary>
    /// Inverte preto e branco.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <returns>Nova textura com as cores invertidas.</returns>
    public static void InverseWhiteBlack(Texture2D texture)
    {
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (textureComparative.GetPixel(x, y) == Color.black)
                    texture.SetPixel(x, y, Color.white);
                else if (textureComparative.GetPixel(x, y) == Color.white)
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Polariza a escala cinza entre preto e branco usando a vari�vel tolerance como base.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="tolerance">Toler�ncia, se maior mais preto, quanto menor mais branco.</param>
    [Obsolete("M�todo ultrapassado por passagens por refer�ncias")]
    public static void PolarizeGray(Texture2D texture, float tolerance)
    {
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (texture.GetPixel(x, y).grayscale > tolerance)
                    texture.SetPixel(x, y, Color.white);
                else
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Polariza a escala cinza entre preto e branco usando a vari�vel tolerance como base.
    /// </summary>
    /// <param name="tex">Textura original.</param>
    /// <param name="tolerance">Toler�ncia, se maior mais preto, quanto menor mais branco.</param>
    /// <returns>Nova textura com as cores polarizadas.</returns>
    public static Texture2D PolarizedGray(Texture2D tex, float tolerance)
    {
        Texture2D texture;
        texture = GetTexture(tex);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (texture.GetPixel(x, y).grayscale > tolerance)
                    texture.SetPixel(x, y, Color.white);
                else
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return texture;
    }
    /// <summary>
    /// Qualquer pixel onde o elemento alfa n�o � igual a zero se torna Color.white.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <returns>Nova textura com a cor branca e transparente.</returns>
    public static Texture2D TurnWhite(Texture2D texture)
    {
        Texture2D tex = new Texture2D(texture.width, texture.height);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (texture.GetPixel(x, y).a == 0)
                    tex.SetPixel(x, y, Color.clear);
                else
                    tex.SetPixel(x, y, Color.white);
            }
        }
        tex.Apply();
        tex.filterMode = FilterMode.Point;
        //tex.Compress(false);
        tex.name = texture.name + "_Sub";
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Preenche a parte de baixo da textura com branco dependendo da altura definida em height.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="height">Altura do preenchimento.</param>
    /// <returns>Nova textura com a cor branca preenchendo e a parte transparente acima.</returns>
    public static Texture2D GenerateHeightWhiteTex(Texture2D texture, int height)
    {
        Texture2D tex = new Texture2D(texture.width, texture.height);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (y >= height)
                    tex.SetPixel(x, y, Color.clear);
                else
                    tex.SetPixel(x, y, Color.white);
            }
        }
        tex.Apply();
        tex.filterMode = FilterMode.Point;
        //tex.Compress(false);
        tex.name = texture.name + "_Heg";
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return tex;
    }
    /// <summary>
    /// Expande a �rea branca.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    [Obsolete("M�todo ultrapassado por passagens por refer�ncias")]
    public static void ExpandWhiteArea(Texture2D texture)
    {
        Resources.UnloadUnusedAssets();
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (textureComparative.GetPixel(x, y) == Color.white)
                    continue;

                if (textureComparative.GetPixel(x - 1, y) == Color.white || textureComparative.GetPixel(x + 1, y) == Color.white || textureComparative.GetPixel(x, y - 1) == Color.white || textureComparative.GetPixel(x, y + 1) == Color.white)
                    texture.SetPixel(x, y, Color.white);
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Expande a �rea preta.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <returns>Nova textura com a cor preta expandida.</returns>
    public static Texture2D ExpandBlackArea(Texture2D tex)
    {
        Texture2D texture;
        texture = GetTexture(tex);
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int x = 0; x < texture.width; x++)
        {
            for (int y = 0; y < texture.height; y++)
            {
                if (textureComparative.GetPixel(x, y) == Color.black)
                    continue;

                if (textureComparative.GetPixel(x - 1, y) == Color.black || textureComparative.GetPixel(x + 1, y) == Color.black || textureComparative.GetPixel(x, y - 1) == Color.black || textureComparative.GetPixel(x, y + 1) == Color.black)
                    texture.SetPixel(x, y, Color.black);
            }
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return texture;
    }
    /// <summary>
    /// Expande a �rea branca.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="times">Quantas vezes expandir�.</param>
    [Obsolete("M�todo ultrapassado por passagens por refer�ncias")]
    public static void ExpandWhiteArea(Texture2D texture, int times)
    {
        Texture2D textureComparative = new Texture2D(texture.width, texture.height);
        TextureToOther(ref textureComparative, texture);
        for (int i = 0; i < times; i++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    if (textureComparative.GetPixel(x, y) == Color.white)
                        continue;

                    if (textureComparative.GetPixel(x - 1, y) == Color.white || textureComparative.GetPixel(x + 1, y) == Color.white || textureComparative.GetPixel(x, y - 1) == Color.white || textureComparative.GetPixel(x, y + 1) == Color.white)
                        texture.SetPixel(x, y, Color.white);
                }
            }
            texture.Apply();
            TextureToOther(ref textureComparative, texture);
        }
        texture.Apply();
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Borra a textura.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <returns>Nova textura borrada.</returns>
    public static Texture2D ApplyBlur(Texture2D texture, int times)
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        Texture2D previousTexture = texture;
        for (int a = 0; a < times; a++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color color = Color.black;
                    float[] rgb = { 0, 0, 0 };

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i + x >= texture.width || x + i < 0 || j + y >= texture.height || y + j < 0)
                                continue;
                            
                            rgb[0] += previousTexture.GetPixel(x + i, y + j).r;
                            rgb[1] += previousTexture.GetPixel(x + i, y + j).g;
                            rgb[2] += previousTexture.GetPixel(x + i, y + j).b;
                        }
                    }
                    for (int i = 0; i < rgb.Length; i++)
                    {
                        rgb[i] /= 9;
                    }
                    color = new Color(rgb[0], rgb[1], rgb[2]);
                    newTexture.SetPixel(x, y, color);
                }
            }
            newTexture.Apply();
            previousTexture = newTexture;
            previousTexture.Apply();
        }
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return newTexture;
    }
    /// <summary>
    /// Borra a textura com o elemento alfa.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <returns>Nova textura borrada.</returns>
    public static Texture2D ApplyBlurAlpha(Texture2D texture, int times)
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        Texture2D previousTexture = GetTexture(texture);
        for (int a = 0; a < times; a++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color color = Color.clear;
                    float[] rgba = { 0, 0, 0, 0};

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i + x >= texture.width || x + i < 0 || j + y >= texture.height || y + j < 0)
                                continue;

                            rgba[0] += previousTexture.GetPixel(x + i, y + j).r;
                            rgba[1] += previousTexture.GetPixel(x + i, y + j).g;
                            rgba[2] += previousTexture.GetPixel(x + i, y + j).b;
                            rgba[3] += previousTexture.GetPixel(x + i, y + j).a;
                        }
                    }
                    for (int i = 0; i < rgba.Length; i++)
                    {
                        rgba[i] /= 9;
                    }
                    color = new Color(rgba[0], rgba[1], rgba[2], rgba[3]);
                    newTexture.SetPixel(x, y, color);
                }
            }
            newTexture.Apply();
            previousTexture = GetTexture(newTexture);
            previousTexture.Apply();
        }
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return newTexture;
    }
    /// <summary>
    /// Borra a textura somente na cor preta.
    /// </summary>
    /// <param name="texture">Textura original.</param>
    /// <param name="times">Quantas vezes repetir o processo.</param>
    /// <returns>Nova textura borrada.</returns>
    public static Texture2D ApplyBlurOnlyInBlack(Texture2D texture, int times)
    {
        Texture2D newTexture = new Texture2D(texture.width, texture.height);
        Texture2D previousTexture = texture;
        for (int a = 0; a < times; a++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                for (int y = 0; y < texture.height; y++)
                {
                    Color color = Color.black;
                    float[] rgb = { 0, 0, 0 };

                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            if (i + x >= texture.width || x + i < 0 || j + y >= texture.height || y + j < 0)
                                continue;

                            rgb[0] += previousTexture.GetPixel(x + i, y + j).r;
                            rgb[1] += previousTexture.GetPixel(x + i, y + j).g;
                            rgb[2] += previousTexture.GetPixel(x + i, y + j).b;
                        }
                    }
                    for (int i = 0; i < rgb.Length; i++)
                    {
                        rgb[i] /= 9;
                    }
                    color = new Color(rgb[0], rgb[1], rgb[2], 1);
                    if (texture.GetPixel(x, y) != Color.white)
                        newTexture.SetPixel(x, y, color);
                }
            }
            newTexture.Apply();
            previousTexture = newTexture;
            previousTexture.Apply();
        }
        Resources.UnloadUnusedAssets();
        GC.Collect();
        return newTexture;
    }
    /// <summary>
    /// Mescla m�ltiplos sprites.
    /// </summary>
    /// <param name="width">Largura.</param>
    /// <param name="height">Altura.</param>
    /// <param name="sprites">Lista de sprites a ser fornecida, cada uma ser� mesclada com a outra, a textura de menor index ser� escrevida por cima da textura de maior index.</param>
    /// <param name="name">Nome do sprite.</param>
    /// <returns>Novo sprite mesclado.</returns>
    public static Sprite MergeSprites(int width, int height, Sprite[] sprites, string name)
    {
        Texture2D newTexture = new Texture2D(width, height);
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                newTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }
        for (int i = 0; i < sprites.Length; i++)
        {
            int xx = 0, yy = 0;
            for (int x = (int)sprites[i].textureRect.position.x; x < (int)sprites[i].textureRect.position.x + newTexture.width; x++)
            {
                for (int y = (int)sprites[i].textureRect.position.y; y < (int)sprites[i].textureRect.position.y + newTexture.height; y++)
                {
                    Color color = sprites[i].texture.GetPixel(x, y);
                    if (color.a > 0)
                        newTexture.SetPixel(x, y, color);
                    /*else if (i - 1 >= 0 && color.a == 0)
                        newTexture.SetPixel(x, y, sprites[i - 1].texture.GetPixel((int)sprites[i - 1].textureRect.position.x + xx, (int)sprites[i - 1].textureRect.position.y + yy));*/
                    yy++;
                }
                xx++;
            }
            newTexture.Apply();
        }
        newTexture.filterMode = FilterMode.Point;
        newTexture.Compress(false);
        Sprite finalSprite = Sprite.Create(newTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0), true);
        finalSprite.name = name;
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return finalSprite;
    }
    /// <summary>
    /// Mescla m�ltiplos sprites com alpha.
    /// </summary>
    /// <param name="width">Largura.</param>
    /// <param name="height">Altura.</param>
    /// <param name="sprites">Lista de sprites a ser fornecida, cada uma ser� mesclada com a outra, a textura de menor index ser� escrevida por cima da textura de maior index.</param>
    /// <param name="name">Nome do sprite.</param>
    /// <returns>Novo sprite mesclado.</returns>
    [Obsolete("M�todo n�o funcional")]
    public static Sprite MergeSpritesWithAlpha(int width, int height, Sprite[] sprites, string name)
    {
        Texture2D newTexture = new Texture2D(width, height);
        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                newTexture.SetPixel(x, y, new Color(1, 1, 1, 0));
            }
        }
        for (int i = 0; i < sprites.Length; i++)
        {
            for (int x = (int)sprites[i].textureRect.position.x; x < (int)sprites[i].textureRect.position.x + newTexture.width; x++)
            {
                for (int y = (int)sprites[i].textureRect.position.y; y < (int)sprites[i].textureRect.position.y + newTexture.height; y++)
                {
                    float nA = 1f - sprites[i].texture.GetPixel(x, y).a;
                    Color color = sprites[i].texture.GetPixel(x, y);
                    if (i - 1 >= 0)
                    {
                        color = new Color(
                            (sprites[i].texture.GetPixel(x, y).r - nA) + (sprites[i - 1].texture.GetPixel(x, y).r - sprites[i].texture.GetPixel(x, y).a),
                            (sprites[i].texture.GetPixel(x, y).g - nA) + (sprites[i - 1].texture.GetPixel(x, y).g - sprites[i].texture.GetPixel(x, y).a),
                            (sprites[i].texture.GetPixel(x, y).b - nA) + (sprites[i - 1].texture.GetPixel(x, y).b - sprites[i].texture.GetPixel(x, y).a),
                            1f);
                        if (sprites[i].texture.GetPixel(x, y).a > 0 && sprites[i - 1].texture.GetPixel(x, y).a == 1)
                            newTexture.SetPixel(x, y, color);
                    }
                    else
                    {
                        if (color.a > 0)
                            newTexture.SetPixel(x, y, color);
                    }
                }
            }
            newTexture.Apply();
        }
        newTexture.filterMode = FilterMode.Point;
        newTexture.Compress(false);
        Sprite finalSprite = Sprite.Create(newTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0), true);
        finalSprite.name = name;
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return finalSprite;
    }
    /// <summary>
    /// Move pixeis de um sprite para um lado X ou Y.
    /// </summary>
    /// <param name="sprite">Sprite que ser� modificado.</param>
    /// <param name="moveX">Dire��o X.</param>
    /// <param name="moveY">Dire��o Y.</param>
    /// <returns>Novo sprite com os pixeis movidos.</returns>
    public static Sprite MovePixelsInSprite(Sprite sprite, int moveX, int moveY)
    {
        Texture2D newTexture = new Texture2D(sprite.texture.width, sprite.texture.height);
        for (int x = (int)sprite.textureRect.position.x; x < (int)sprite.textureRect.position.x + newTexture.width; x++)
        {
            for (int y = (int)sprite.textureRect.position.y; y < (int)sprite.textureRect.position.y + newTexture.height; y++)
            {
                int whereX = x + moveX, whereY = y + moveY;

                if (x + moveX >= sprite.texture.width)//x
                    whereX = (x + moveX) - sprite.texture.width;
                else if (x + moveX < 0)
                    whereX = (x + moveX) + sprite.texture.width;
                if (y + moveY >= sprite.texture.height)//y
                    whereY = (y + moveY) - sprite.texture.height;
                else if (y + moveY < 0)
                    whereY = (y + moveY) + sprite.texture.height;

                newTexture.SetPixel(whereX, whereY, sprite.texture.GetPixel(x, y));
            }
        }
        newTexture.Apply();
        newTexture.filterMode = FilterMode.Point;
        newTexture.Compress(false);
        Sprite finalSprite = Sprite.Create(newTexture, new Rect(sprite.textureRect.position.x, sprite.textureRect.position.y, sprite.texture.width, sprite.texture.height), new Vector2(0.5f, 0.5f), sprite.pixelsPerUnit, 0, 0, new Vector4(0, 0, 0, 0), true);
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return finalSprite;
    }
    /// <summary>
    /// Desenha um c�rculo na textura fornecida.
    /// </summary>
    /// <param name="tex">Textura que ser� modificada.</param>
    /// <param name="color">A cor do c�rculo.</param>
    /// <param name="x">Posi��o X.</param>
    /// <param name="y">Posi��o Y.</param>
    /// <param name="radius">Raio do c�rculo.</param>
    /// <returns>Nova textura com um c�rculo desenhado.</returns>
    public static Texture2D DrawCircle(ref Texture2D tex, Color color, int x, int y, int radius = 3)
    {
        float rSquared = radius * radius;

        for (int u = x - radius; u < x + radius + 1; u++)
            for (int v = y - radius; v < y + radius + 1; v++)
                if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                    tex.SetPixel(u, v, color);
        tex.filterMode = FilterMode.Point;
        tex.Apply();
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return tex;
    }
    /// <summary>
    /// Gera um PNG com a textura fornecida.
    /// </summary>
    /// <param name="tex">Textura que ser� usada.</param>
    public static void GeneratePng(Texture2D tex)
    {
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("Assets/Resources/" + tex.name + ".png", bytes);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /// <summary>
    /// Gera um PNG com a textura fornecida transformada em cinza, usado para tiles.
    /// </summary>
    /// <param name="tex">Textura que ser� usada.</param>
    public static void GeneratePngTilesAlpha(Texture2D tex)
    {
        GC.Collect();
        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                tex.SetPixel(x, y, new Color(0.5f, 0.5f, 0.5f, tex.GetPixel(x, y).a));
            }
        }
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes("Assets/Resources/a.png", bytes);
        Resources.UnloadUnusedAssets();
        GC.Collect();
    }
    /*public static Color[] GeneratePallete()
    {

    }
    public static Texture2D GenerateMap(GameObject[,] map, Texture2D tex)
    {

    }*/
    /// <summary>
    /// Passa o valor alfa do borderSprite para criar um tile.
    /// </summary>
    /// <param name="tile">Sprite que ser� usado como tile.</param>
    /// <param name="borderSprite">Sprite que ser� usado para criar a borda.</param>
    /// <returns>Novo sprite com bordas novas.</returns>
    public static Sprite PassBorderAlpha(Sprite tile, Sprite borderSprite)
    {
        Texture2D newTexture = new(tile.texture.width, tile.texture.height);
        newTexture = GetTexture(tile.texture);
        newTexture.Apply();
        for (int x = 0; x < tile.texture.width; x++)
        {
            for (int y = 0; y < tile.texture.height; y++)
            {
                newTexture.SetPixel(x, y, new Color(newTexture.GetPixel(x, y).r, newTexture.GetPixel(x, y).g, newTexture.GetPixel(x, y).b, borderSprite.texture.GetPixel(x + (int)borderSprite.textureRect.position.x, y + (int)borderSprite.textureRect.position.y).a));
            }
        }
        newTexture.Apply();
        newTexture.filterMode = FilterMode.Point;
        newTexture.Compress(false);
        Sprite finalSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0), true);
        //return borderSprite;
        //Resources.UnloadUnusedAssets();
        //GC.Collect();
        return finalSprite;
    }
}