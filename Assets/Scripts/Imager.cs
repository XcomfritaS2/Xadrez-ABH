using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TextureFunction;
using static Main;
using UnityEngine.UI;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Imager : MonoBehaviour
{
    public static Imager IMAGER;

    public string imageName;
    public string copyName;

    private string copyPath = "Assets/Resources/";

    //coletar a imagem original
    public Sprite OriginalImage { get => CollectSpriteInResources(imageName); }
    public Sprite ImageCopy;

    public List<GameObject> piecesList;

    void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/a.png"))
        {
            GeneratePng(OriginalImage.texture, Application.persistentDataPath + "/a.png");
        }
        IMAGER = this;
        ImageCopy = CollectSprite(Application.persistentDataPath + "/a.png");
        GameObject.FindGameObjectWithTag("DEBUGLOG").transform.parent.Find("Tab").gameObject.GetComponent<Image>().sprite = ImageCopy;
        GameObject.FindGameObjectWithTag("DEBUGLOG").GetComponent<TextMeshProUGUI>().text = Application.persistentDataPath;
        MAIN.ProxTurno();
        //MAIN.StartMain();

        piecesList = new List<GameObject>();
        RegeneratePieces();
        //criar a cópia em um arquivo
        //Debug.Log(OriginalImage.texture != null);
        //ModifyMeta(copyPath + copyName);
        //coletar a cópia
    }
    /// <summary>
    /// Atualiza a cópia da imagem através dos primeiros dois caracteres, dos últimos e o player através do elemento B da cor do pixel X0; Y0.
    /// </summary>
    /// <returns>Nenhum Valor.</returns>
    public void UpdateImage()
    {
        //string text = "";
        Texture2D tex = new Texture2D(8, 8);

        for (int x = 0; x < MAIN.positions.GetLength(0); x++)
        {
            for (int y = 0; y < MAIN.positions.GetLength(1); y++)
            {
                if (MAIN.GetPosition(x, y) == null)
                {
                    Color32 c = ImageCopy.texture.GetPixel(x, y);
                    tex.SetPixel(x, y, new Color32(0, 0, c.b, 0));
                    continue;
                }
                //text += MAIN.GetPosition(x, y).name + " Position: x = " + x.ToString() + ", y = " + y.ToString() + "\n";

                char[] OnePieceName = MAIN.GetPosition(x, y).name.ToCharArray();
                Color32 color = UnityEngine.Color.black;
                //peça
                switch (OnePieceName[0])
                {
                    //RAINHA
                    case 'R' when OnePieceName[1] == 'a':
                        color = new Color32(0, color.g, color.b, 255);
                        break;
                    //CAVALO
                    case 'C' when OnePieceName[1] == 'a':
                        color = new Color32(25, color.g, color.b, 255);
                        break;
                    //NOTA DE MATEMÁTICA
                    case 'B' when OnePieceName[1] == 'i':
                        color = new Color32(50, color.g, color.b, 255);
                        break;
                    //REI
                    case 'R' when OnePieceName[1] == 'e':
                        color = new Color32(75, color.g, color.b, 255);
                        break;
                    //ROOK
                    case 'T' when OnePieceName[1] == 'o':
                        color = new Color32(100, color.g, color.b, 255);
                        break;
                    //PEÃO
                    case 'P' when OnePieceName[1] == 'e':
                        color = new Color32(125, color.g, color.b, 255);
                        break;
                    default:
                        Debug.Log("O tipo da peça não foi encontrado.");
                        break;
                }
                //cor jogador
                if (OnePieceName[OnePieceName.Length - 1] == 'P')
                {
                    color = new Color32(color.r, 0, color.b, 255);
                }
                else if (OnePieceName[OnePieceName.Length - 1] == 'B')
                {
                    color = new Color32(color.r, 255, color.b, 255);
                }
                tex.SetPixel(x, y, color);
            }
        }
        Color32 col = ImageCopy.texture.GetPixel(0, 0);
        col.b = CurrentPlayer ? (byte)0 : (byte)255;
        tex.SetPixel(0, 0, col);
        //Debug.Log(text);
        tex.filterMode = FilterMode.Point;
        tex.Apply();
        GeneratePng(tex, Application.persistentDataPath + "/a.png");
        ImageCopy = CollectSprite(Application.persistentDataPath + "/a.png");
        GameObject.FindGameObjectWithTag("TextoVence").transform.parent.Find("Tab").GetComponent<Image>().sprite = ImageCopy;
    }
    /// <summary>
    /// Chama Destroy() em todos os GameObjects em playerPreto e playerBranco, refaz as listas e recoloca as peças usando os pixeis da cópia da imagem como referência.
    /// </summary>
    /// <returns>Nenhum Valor.</returns>
    public void RegeneratePieces()
    {
        for (int i = 0; i < MAIN.playerPreto.Length; i++)
        {
            if (MAIN.playerPreto[i] != null)
                Destroy(MAIN.playerPreto[i]);
            if (MAIN.playerBranco[i] != null)
                Destroy(MAIN.playerBranco[i]);
        }
        MAIN.playerPreto = new GameObject[16];
        MAIN.playerBranco = new GameObject[16];
        string pName;
        for (int x = 0; x < MAIN.positions.GetLength(0); x++)
        {
            for (int y = 0; y < MAIN.positions.GetLength(1); y++)
            {
                pName = GetPiece(x, y);
                if (pName == "null")
                    continue;
                GameObject obj = MAIN.Create(pName, x, y);
                MAIN.SetPosition(obj);

                char[] OnePieceName = MAIN.GetPosition(x, y).name.ToCharArray();
                if (OnePieceName[OnePieceName.Length - 1] == 'P')
                    for (int i = 0; i < MAIN.playerPreto.Length; i++)
                    {
                        if (MAIN.playerPreto[i] == null)
                        {
                            MAIN.playerPreto[i] = obj;
                        }
                    }
                else
                    for (int i = 0; i < MAIN.playerBranco.Length; i++)
                    {
                        if (MAIN.playerBranco[i] == null)
                        {
                            MAIN.playerBranco[i] = obj;
                        }
                    }
            }
        }
    }
    /// <summary>
    /// Retorna o nome da peça usando a os elementos RG e A das cores.
    /// <para>R é usado para o nome da peça;</para>
    /// <para>G é usado para a cor da peça;</para>
    /// <para>A define se tem ou não uma peça no slot.</para>
    /// </summary>
    /// <returns>O nome da peça completo.</returns>
    public string GetPiece(int x, int y)
    {
        string pieceName = "";
        Color32 col = ImageCopy.texture.GetPixel(x, y);
        if (col.a == 0)
            return "null";
        switch (col.r)
        {
            case 0:
                pieceName += "Rainha";
                break;
            case 25:
                pieceName += "Cavalo";
                break;
            case 50:
                pieceName += "Bispo";
                break;
            case 75:
                pieceName += "Rei";
                break;
            case 100:
                pieceName += "Torre";
                break;
            case 125:
                pieceName += "Peao";
                break;
        }
        pieceName = col.g == 255 ? pieceName + "_B" : pieceName + "_P";
        return pieceName;
    }
    public bool CurrentPlayer { get { Color32 col = ImageCopy.texture.GetPixel(0, 0); return col.b == 255; } }
    /*private void ModifyMeta(string name)
    {
        FileStream CopyMeta = File.OpenRead(name + ".png.meta");
        BinaryWriter writer = new BinaryWriter(CopyMeta);

        Debug.Log(CopyMeta);
    }*/
    void OnApplicationQuit()
    {
        GeneratePng(OriginalImage.texture, copyPath + copyName);
#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
    /// <summary>
    /// Gera um novo arquivo de imagem no caminho especificado através da textura fornecida.
    /// </summary>
    /// <param name="tex">A textura usada para ser salva em PNG.</param>
    /// <param name="name">O caminho.</param>
    /// <returns>Nenhum Valor.</returns>
    private void GeneratePng(Texture2D tex, string name)
    {
        if (tex == null)
            return;
        byte[] bytes = tex.EncodeToPNG();
/*#if UNITY_EDITOR
    File.WriteAllBytes(name + ".png", bytes);
    Resources.UnloadUnusedAssets();
    GC.Collect();
    AssetDatabase.Refresh();
#endif*/
        //FileStream fs;
        /*if (!File.Exists(name + ".png"))
        {
           fs = File.Create(name + ".png");
        }
        else
        {
            
        }*/
        /*fs = File.Open(name + ".png", FileMode.OpenOrCreate);
        fs.Write(bytes, 0, bytes.Length);*/
        /*FileStream stream = new FileStream(name + ".png", FileMode.OpenOrCreate, FileAccess.ReadWrite);
        BinaryWriter writer = new BinaryWriter(stream);
        writer.Write(bytes);*/
        string path = Application.persistentDataPath + "/a.png";
        using (FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
        {
            Debug.Log(file.CanWrite + " " + file.CanRead);
            //StreamWriter writer = new StreamWriter(file);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(bytes);

            //File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //File.WriteAllBytes(path, bytes);
            writer.Close();
            file.Close();
            //file.Dispose();
        }
    }
    /// <summary>
    /// Cria um novo sprite a partir do arquivo achado no caminho fornecido.
    /// </summary>
    /// <param name="path">O caminho.</param>
    /// <returns>Retorna um novo Sprite com a textura do caminho fornecido em name</returns>
    private Sprite CollectSprite(string path)
    {
        Texture2D tex = new Texture2D(8, 8);
        //tex = Resources.Load<Sprite>(path).texture;

        byte[] info = File.ReadAllBytes(path);
        //ImageConversion.LoadImage(tex, info, false);
        tex.LoadImage(info, false);

        //tex.Apply();

        if (tex == null) return null;
        tex.filterMode = FilterMode.Point;
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0));
    }
    /// <summary>
    /// Cria um novo sprite a partir do arquivo achado no caminho fornecido a partir do RESOURCES.
    /// </summary>
    /// <param name="path">O caminho.</param>
    /// <returns>Retorna um novo Sprite com a textura do caminho fornecido em name</returns>
    private Sprite CollectSpriteInResources(string path)
    {
        Texture2D tex = Resources.Load<Sprite>(path).texture;
        if (tex == null) return null;
        tex.filterMode = FilterMode.Point;
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 16, 0, 0, new Vector4(0, 0, 0, 0));
    }
    public void Teste()
    {
        SceneManager.LoadScene(0);
    }
}
