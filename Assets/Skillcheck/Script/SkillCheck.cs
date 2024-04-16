using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static TextureFunction;
using static EffectsLibrary;

public class SkillCheck : MonoBehaviour
{
    #region instâncias

    //Gerais
    public static SkillCheck Instance;
    public Color ClearWhite { get => Color.white - Color.black; }
    public Color ClearMask { get => new Color(1, 1, 1, 0.01f); }
    private Image Background { get => GetComponent<Image>(); }

    //game
    private TextMeshProUGUI GameTitle { get => transform.Find("Title").GetComponent<TextMeshProUGUI>(); }
    private Image Number { get => transform.Find("Number").GetComponent<Image>(); }
    private TextMeshProUGUI Letter1 { get => transform.Find("Letter1").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI Letter2 { get => transform.Find("Letter2").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI Timer { get => transform.Find("Timer").GetComponent<TextMeshProUGUI>(); }

    //Player1 / branco
    private Image Circle1 { get => transform.Find("Circle1").GetComponent<Image>(); }
    private Image Circle1back { get => transform.Find("Circle1back").GetComponent<Image>(); }
    private Image Default1 { get => Circle1.transform.Find("Default").GetComponent<Image>(); }
    private GameObject Seta1 { get => Default1.transform.Find("Seta").gameObject; }
    private GameObject Spawn1 { get => Seta1.transform.Find("Spawn").gameObject; }
    private TextMeshProUGUI Message1 { get => transform.Find("Message1").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI PointsTitle1 { get => transform.Find("Points1Title").gameObject.GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI PointsSpace1 { get => transform.Find("Points1").gameObject.GetComponent<TextMeshProUGUI>(); }
    private GameObject Wave1 { get => transform.Find("Wave1").gameObject; }

    //Player1 / preto
    private Image Circle2 { get => transform.Find("Circle2").GetComponent<Image>(); }
    private Image Circle2back { get => transform.Find("Circle2back").GetComponent<Image>(); }
    private Image Default2 { get => Circle2.transform.Find("Default").GetComponent<Image>(); }
    private GameObject Seta2 { get => Default2.transform.Find("Seta").gameObject; }
    private GameObject Spawn2 { get => Seta2.transform.Find("Spawn").gameObject; }
    private TextMeshProUGUI Message2 { get => transform.Find("Message2").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI PointsTitle2 { get => transform.Find("Points2Title").gameObject.GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI PointsSpace2 { get => transform.Find("Points2").gameObject.GetComponent<TextMeshProUGUI>(); }
    private GameObject Wave2 { get => transform.Find("Wave2").gameObject; }

    //Tutorial
    private Image TutorialCircle { get => transform.Find("TutorialCircle").GetComponent<Image>(); }
    private Image TutorialDefault { get => TutorialCircle.transform.Find("Default").GetComponent<Image>(); }
    private GameObject TutorialSeta { get => TutorialDefault.transform.Find("Seta").gameObject; }

    //fade
    private Image Fade { get => transform.Find("Fade").GetComponent<Image>(); }

    //tutorial
    private TextMeshProUGUI Title { get => transform.Find("TutorialTitle").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI TutorialDescription { get => transform.Find("TutorialDesc").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI TutorialGood { get => transform.Find("BoaMessageTu").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI TutorialPerfect { get => transform.Find("PerfeitoMessageTu").GetComponent<TextMeshProUGUI>(); }
    private TextMeshProUGUI SkipTutorial { get => transform.Find("SkipTu").GetComponent<TextMeshProUGUI>(); }

    #endregion

    #region Variáveis & Status
    [Header("Preferências", order = 1)]
    public float timer = 20;
    public bool ondaAtivada = true;
    public bool tutorialAtivado = true;

    private string PerfectMessage = "Perfeito!";
    private string GoodMessage = "Boa!";
    private string MissMessage = "Erro";
    private string LossMessage = "Perda";

    public Color PerfectColor;
    public Color GoodColor;
    public Color MissColor;
    public Color NotTouchColor;
    //Wave
    public int initialSize = 3;
    public int finalSize = 5;

    public float waveTimer = 0.3f;

    public Sprite[] numberSprites;

    [Header("Status", order = 1)]
    private bool timerIsUnderFinal = false;

    public int points1 = 0;
    public int points2 = 0;

    public bool player1Rotate = false;
    public bool player2Rotate = false;
    public bool tutorialRotate = false;
    public bool gameStarted = false;
    public bool gameFinished = false;

    public bool canSkipTutorial = false;
    public bool onTutorial = true;

    public float player1SpeedMultiplier = 1f;
    public float player2SpeedMultiplier = 1f;

    #endregion

    #region constantes
    //Não mexer
    const float GOOD_EULER_ANGLE = 57.6f;
    const float PERFECT_EULER_ANGLE = 14.4f;
    const int UPDATE_TIMES = 5;

    //bonus & punição
    const int PERFECT_BONUS = 15;
    const int GOOD_BONUS = 10;
    const int MISS_PUNISHMENT = 0;
    const int NOT_TOUCHING_PUNISHMENT = 5;

    //multiplicação de velocidade
    const float PERFECT_SPEED_PLUS = 0.15f;
    const float GOOD_SPEED_PLUS = 0.1f;
    const float MISS_SPEED_PLUS = 0.05f;
    const float NOT_TOUCHING_PLUS = 0f;
    const float MAX_VELOCITY = 5f;

    //gráficos
    const float CIRCLE_MESSAGES_SPEED = 20f;
    #endregion

    //iniciar as variáveis
    void Start()
    {
        //Application.targetFrameRate = 120;
        Instance = this;

        timerIsUnderFinal = false;
        canSkipTutorial = false;
        onTutorial = true;

        player1Rotate = false;
        player2Rotate = false;
        tutorialRotate = false;
        gameStarted = false;

        Timer.color = ClearWhite;
        Timer.text = timer.ToString();

        Fade.color = Color.black;

        Title.color = ClearWhite;
        TutorialDescription.color = ClearWhite;

        //Circle1.color = ClearWhite;
        //Circle2.color = ClearWhite;
        PointsSpace1.color = ClearWhite;
        PointsSpace2.color = ClearWhite;
        PointsTitle1.color = ClearWhite;
        PointsTitle2.color = ClearWhite;

        GameTitle.color -= Color.black;

        Default1.color = ClearWhite;
        Default2.color = ClearWhite;

        Letter1.color = ClearWhite;
        Letter2.color = ClearWhite;

        TutorialDefault.color = ClearWhite;

        Message1.color -= Color.black;
        Message2.color -= Color.black;

        TutorialGood.color -= Color.black;

        foreach (Transform imageObj in TutorialDefault.gameObject.transform)
        {
            imageObj.gameObject.GetComponent<Image>().color = ClearWhite;
        }
        foreach (Transform imageObj in Default1.gameObject.transform)
        {
            imageObj.gameObject.GetComponent<Image>().color = ClearWhite;
        }
        foreach (Transform imageObj in Default2.gameObject.transform)
        {
            imageObj.gameObject.GetComponent<Image>().color = ClearWhite;
        }
        StartCoroutine(EntradaTriunfal());
    }
    //gráficos
    void Update()
    {
        #region Temporizador
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
                ExecuteNumberOfTimes(this, 1f, 5, () => { StartCoroutine(SetSizeSmoothDecrease(Timer.GetComponent<RectTransform>(), 1.7f, 0.9f)); });
            }
            if (timer <= 5)
            {
                timerIsUnderFinal = true;
            }
        }
        #endregion

        #region Tutorial Circulo
        if (onTutorial)
        {
            if (TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z > PERFECT_EULER_ANGLE && TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z <= GOOD_EULER_ANGLE)
            {
                StartCoroutine(SmoothAlphaChange(TutorialGood, 1f, 0.4f));
            }
            else if (TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z > GOOD_EULER_ANGLE)
            {
                StartCoroutine(SmoothAlphaChange(TutorialGood, 0f, 0.4f));
            }
            if (TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z >= 0 && TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z <= PERFECT_EULER_ANGLE)
            {
                StartCoroutine(SmoothAlphaChange(TutorialPerfect, 1f, 0.4f));
            }
            else if (TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z > PERFECT_EULER_ANGLE)
            {
                StartCoroutine(SmoothAlphaChange(TutorialPerfect, 0f, 0.4f));
            }
        }
        #endregion

        #region Players circulos
        //mudar posição das mensagens
        Message1.GetComponent<RectTransform>().position = Message1.color.a > 0 ? Message1.GetComponent<RectTransform>().position + new Vector3(0, CIRCLE_MESSAGES_SPEED * Time.deltaTime, 0) : Message1.GetComponent<RectTransform>().position;
        Message2.GetComponent<RectTransform>().position = Message2.color.a > 0 ? Message2.GetComponent<RectTransform>().position + new Vector3(0, CIRCLE_MESSAGES_SPEED * Time.deltaTime, 0) : Message2.GetComponent<RectTransform>().position;

        //mudar transparência das mensagens
        Message1.color -= Color.black * Time.deltaTime * 1f;
        Message2.color -= Color.black * Time.deltaTime * 1f;
        #endregion

        #region Sair do tutorial
        if (((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) && canSkipTutorial && !gameStarted) || (!tutorialAtivado && canSkipTutorial && !gameStarted))
        {
            tutorialAtivado = true;
            onTutorial = false;
            StopAllCoroutines();

            StartCoroutine(SmoothAlphaChange(Title, 0f, 1f));
            StartCoroutine(SmoothAlphaChange(TutorialDescription, 0f, 1f));

            StartCoroutine(SmoothAlphaChange(TutorialPerfect, 0f, 0.4f));
            StartCoroutine(SmoothAlphaChange(TutorialGood, 0f, 0.4f));
            StartCoroutine(SmoothAlphaChange(TutorialDefault, 0f, 1f));
            StartCoroutine(SmoothAlphaChange(SkipTutorial, 0f, 1f));

            foreach (Transform imageObj in TutorialDefault.gameObject.transform)
            {
                StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 0f, 1f));
            }
            StartCoroutine(SmoothAlphaChange(Fade, 0f, 1.5f));
            StartCoroutine(StartGame(1.5f));
        }
        #endregion

        #region Terminar o jogo
        if (gameFinished)
        {
            player1Rotate = false;
            player2Rotate = false;

            StartCoroutine(EndGame(0.1f));
        }
        #endregion
    }
    //para operações técnicas e de alta velocidade (gameplay)
    private void FixedUpdate()
    {
        #region Resetar o 360
        if (TutorialSeta.GetComponent<RectTransform>().localEulerAngles.z >= 360)
            TutorialSeta.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        if (Seta1.GetComponent<RectTransform>().localEulerAngles.z >= 360)
            Seta1.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        if (Seta2.GetComponent<RectTransform>().localEulerAngles.z >= 360)
            Seta2.GetComponent<RectTransform>().localRotation = Quaternion.identity;
        #endregion

        #region Players controles
        //player 1
        if (player1Rotate && Input.GetKey(KeyCode.A))
        {
            Color col = Color.black;
            //perfect
            if (Seta1.GetComponent<RectTransform>().localEulerAngles.z >= 0 && Seta1.GetComponent<RectTransform>().localEulerAngles.z <= PERFECT_EULER_ANGLE)
            {
                //pontuação
                points1 += PERFECT_BONUS;
                player1SpeedMultiplier += PERFECT_SPEED_PLUS;
                //messagem
                Message1.text = PerfectMessage;
                col = PerfectColor;
            }
            //good
            else if (Seta1.GetComponent<RectTransform>().localEulerAngles.z > PERFECT_EULER_ANGLE && Seta1.GetComponent<RectTransform>().localEulerAngles.z <= GOOD_EULER_ANGLE)
            {
                //pontuação
                points1 += GOOD_BONUS;
                player1SpeedMultiplier += GOOD_SPEED_PLUS;
                //messagem
                Message1.text = GoodMessage;
                col = GoodColor;
            }
            //miss
            else if (Seta1.GetComponent<RectTransform>().localEulerAngles.z > GOOD_EULER_ANGLE && Seta1.GetComponent<RectTransform>().localEulerAngles.z < 360)
            {
                //pontuação
                points1 -= MISS_PUNISHMENT;
                player1SpeedMultiplier += MISS_SPEED_PLUS;
                //messagem
                Message1.text = MissMessage;
                col = MissColor;
            }
            Message1.color = col;
            Message1.GetComponent<RectTransform>().position = Spawn1.GetComponent<RectTransform>().position;

            if (ondaAtivada)
                Wave1.GetComponent<WaveEffect>().StartOnda(Instance.transform, initialSize, finalSize, col, waveTimer);

            player1SpeedMultiplier = player1SpeedMultiplier > MAX_VELOCITY ? MAX_VELOCITY : player1SpeedMultiplier;
            points1 = points1 < 0 ? 0 : points1;
            PointsSpace1.text = points1.ToString();
            StartCoroutine(ChangeCircle(1));
        }
        //player 2
        if (player2Rotate && Input.GetKey(KeyCode.L))
        {
            Color col = Color.black;
            //perfect
            if (Seta2.GetComponent<RectTransform>().localEulerAngles.z >= 0 && Seta2.GetComponent<RectTransform>().localEulerAngles.z <= PERFECT_EULER_ANGLE)
            {
                //pontuação
                points2 += PERFECT_BONUS;
                player2SpeedMultiplier += PERFECT_SPEED_PLUS;
                //messagem
                Message2.text = PerfectMessage;
                col = PerfectColor;
            }
            //good
            else if (Seta2.GetComponent<RectTransform>().localEulerAngles.z > PERFECT_EULER_ANGLE && Seta2.GetComponent<RectTransform>().localEulerAngles.z <= GOOD_EULER_ANGLE)
            {
                //pontuação
                points2 += GOOD_BONUS;
                player2SpeedMultiplier += GOOD_SPEED_PLUS;
                //messagem
                Message2.text = GoodMessage;
                col = GoodColor;
            }
            //miss
            else if (Seta2.GetComponent<RectTransform>().localEulerAngles.z > GOOD_EULER_ANGLE && Seta2.GetComponent<RectTransform>().localEulerAngles.z < 360)
            {
                //pontuação
                points2 -= MISS_PUNISHMENT;
                player2SpeedMultiplier += MISS_SPEED_PLUS;
                //messagem
                Message2.text = MissMessage;
                col = MissColor;
            }
            Message2.color = col;
            Message2.GetComponent<RectTransform>().position = Spawn2.GetComponent<RectTransform>().position;

            if (ondaAtivada)
                Wave2.GetComponent<WaveEffect>().StartOnda(Instance.transform, initialSize, finalSize, col, waveTimer);

            player2SpeedMultiplier = player2SpeedMultiplier > MAX_VELOCITY ? MAX_VELOCITY : player2SpeedMultiplier;
            points2 = points2 < 0 ? 0 : points2;
            PointsSpace2.text = points2.ToString();
            StartCoroutine(ChangeCircle(2));
        }
        #endregion

        #region Rolagem
        if (tutorialRotate)
            Rolagem(TutorialSeta, 0.2f);
        if (player1Rotate)
            Rolagem(Seta1, player1SpeedMultiplier);
        if (player2Rotate)
            Rolagem(Seta2, player2SpeedMultiplier);
        #endregion
    }

    #region Técnico
    void Rolagem(GameObject seta, float markplier)
    { for (int i = 0; i <= 100; i += UPDATE_TIMES)
        {
            //simplesmente não apertar
            if (player1Rotate && Seta1.GetComponent<RectTransform>().eulerAngles.z >= 0 && Seta1.GetComponent<RectTransform>().eulerAngles.z <= 0.5f)
            {
                //pontuação
                points1 -= NOT_TOUCHING_PUNISHMENT;
                player1SpeedMultiplier += NOT_TOUCHING_PLUS;

                //mensagem
                Message1.GetComponent<RectTransform>().position = Spawn1.GetComponent<RectTransform>().position;
                Message1.color = NotTouchColor;
                Message1.text = LossMessage;

                //atualização
                player1SpeedMultiplier = player1SpeedMultiplier > MAX_VELOCITY ? MAX_VELOCITY : player1SpeedMultiplier;
                points1 = points1 < 0 ? 0 : points1;
                PointsSpace1.text = points1.ToString();
                StartCoroutine(ChangeCircle(1));
            }
            //simplesmente não apertar
            if (player2Rotate && Seta2.GetComponent<RectTransform>().eulerAngles.z >= 0 && Seta2.GetComponent<RectTransform>().eulerAngles.z <= 0.5f)
            {
                //pontuação
                points2 -= NOT_TOUCHING_PUNISHMENT;
                player2SpeedMultiplier += NOT_TOUCHING_PLUS;

                //mensagem
                Message2.GetComponent<RectTransform>().position = Spawn2.GetComponent<RectTransform>().position;
                Message2.color = NotTouchColor;
                Message2.text = LossMessage;

                //atualização
                player2SpeedMultiplier = player2SpeedMultiplier > MAX_VELOCITY ? MAX_VELOCITY : player2SpeedMultiplier;
                points2 = points2 < 0 ? 0 : points2;
                PointsSpace2.text = points2.ToString();
                StartCoroutine(ChangeCircle(2));
            }
            seta.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, seta.GetComponent<RectTransform>().localEulerAngles.z + (UPDATE_TIMES * markplier * Time.fixedDeltaTime));
        }
    }
    IEnumerator StartGame(float wait)
    {
        for (int a = 0; a < 2; a++)
        {
            yield return new WaitForSeconds(wait / 2);
        }
        StartCoroutine(SmoothAlphaChange(Letter1, 1f, 2f));
        StartCoroutine(SmoothAlphaChange(Letter2, 1f, 2f));
        for (int a = 0; a < numberSprites.Length; a++)
        {
            Number.color -= (Color.cyan / (numberSprites.Length - 1));
            Number.color += Color.black;
            Number.sprite = numberSprites[a];
            yield return new WaitForSeconds(1f);
        }
        gameStarted = true;
        ResetSeta(1);
        ResetSeta(2);
        player1Rotate = true;
        player2Rotate = true;
    }
    IEnumerator EndGame(float wait)
    {
        for (int a = 0; a < 2; a++)
        {
            yield return new WaitForSeconds(wait / 2);
        }
        StartCoroutine(SmoothAlphaChange(Letter1, 0f, 2f));
        StartCoroutine(SmoothAlphaChange(Letter2, 0f, 2f));

        StartCoroutine(SmoothAlphaChange(Default1, 0f, 1f));
        StartCoroutine(SmoothAlphaChange(Default2, 0f, 1f));

        foreach (Transform imageObj in Default1.gameObject.transform)
        {
            StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 0f, 0.9f));
        }
        foreach (Transform imageObj in Default2.gameObject.transform)
        {
            StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 0f, 0.9f));
        }

        StartCoroutine(SmoothAlphaChange(Timer, 0f, 1f));

        for (int a = 0; a < 2; a++)
        {
            yield return new WaitForSeconds(1f / 2);
        }

        StartCoroutine(SmoothAlphaChange(Circle1back, 0f, 1f));
        StartCoroutine(SmoothAlphaChange(Circle2back, 0f, 1f));
    }
    void ResetSeta(int which)
    {
        switch (which)
        {
            case 1:
                Circle1.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(75f, 290f));
                Seta1.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -Circle1.GetComponent<RectTransform>().eulerAngles.z + 0.6f);
                //Seta1.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0.5f);
                //Seta1.GetComponent<RectTransform>().rotation = Quaternion.identity;
                //Debug.Log(Seta1.GetComponent<RectTransform>().eulerAngles);
                break;
            case 2:
                Circle2.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(75f, 290f));
                Seta2.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, -Circle2.GetComponent<RectTransform>().eulerAngles.z + 0.6f);
                //Seta2.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, 0.5f);
                //Seta1.GetComponent<RectTransform>().rotation = Quaternion.identity;
                //Debug.Log(Seta2.GetComponent<RectTransform>().eulerAngles);
                break;
        }
    }
    IEnumerator ChangeCircle(int which)
    {
        ResetSeta(which);
        switch (which)
        {
            case 1:
                player1Rotate = false;
                Circle1.color = Color.clear;
                Seta1.GetComponent<Image>().color = Color.clear;
                for (int a = 0; a < 2; a++)
                {
                    yield return new WaitForSeconds(1f);
                }
                player1Rotate = true;
                Circle1.color = ClearMask;
                Seta1.GetComponent<Image>().color = Color.white;
                break;
            case 2:
                player2Rotate = false;
                Circle2.color = Color.clear;
                Seta2.GetComponent<Image>().color = Color.clear;
                for (int a = 0; a < 2; a++)
                {

                    yield return new WaitForSeconds(1f);
                }
                player2Rotate = true;
                Circle2.color = ClearMask;
                Seta2.GetComponent<Image>().color = Color.white;
                break;
        }
    }
    #endregion

    IEnumerator EntradaTriunfal()
    {
        //entrada
        StartCoroutine(FadeCanvasObj(Fade, 1.5f, true));
        StartCoroutine(PixelEfeito(Background, 1.5f, false));

        StartCoroutine(SmoothAlphaChange(GameTitle, 1f, 2f));

        StartCoroutine(SmoothAlphaChange(PointsSpace1, 1f, 2.5f));
        StartCoroutine(SmoothAlphaChange(PointsTitle1, 1f, 2.5f));

        StartCoroutine(SmoothAlphaChange(PointsSpace2, 1f, 2.5f));
        StartCoroutine(SmoothAlphaChange(PointsTitle2, 1f, 2.5f));
        for (int a = 0; a < 2; a++)
        {
            yield return new WaitForSeconds(0.6f);
        }

        //aparecimento e animação
        GetComponent<SkillCheckBackgroundAnimation>().continueAnimation = true;
        StartCoroutine(SmoothAlphaChange(Default1, 1f, 1f));
        StartCoroutine(SmoothAlphaChange(Default2, 1f, 1f));
        StartCoroutine(SmoothAlphaChange(Timer, 1f, 1f));
        foreach (Transform imageObj in Default1.gameObject.transform)
        {
            StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 1f, 1f));
        }
        foreach (Transform imageObj in Default2.gameObject.transform)
        {
            StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 1f, 1f));
        }
        for (int a = 0; a < 2; a++)
        {
            yield return new WaitForSeconds(0.2f);
        }
        //se tutorial estiver ativado
        if (tutorialAtivado)
        {
            //escurecimento tutorial
            StartCoroutine(SmoothAlphaChange(Fade, 0.75f, 1f));
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(0.4f);
            }

            //título
            StartCoroutine(SmoothAlphaChange(Title, 1f, 1f));
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(0.2f);
            }

            //texto do tutorial
            StartCoroutine(SmoothAlphaChange(TutorialDescription, 1f, 1f));
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(0.2f);
            }

            //amostragem
            //StartCoroutine(SmoothAlphaChange(TutorialCircle, 1f, 1f));
            TutorialCircle.color = ClearMask;
            StartCoroutine(SmoothAlphaChange(TutorialDefault, 1f, 1f));
            foreach (Transform imageObj in TutorialDefault.gameObject.transform)
            {
                StartCoroutine(SmoothAlphaChange(imageObj.gameObject.GetComponent<Image>(), 1f, 1f));
            }
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(0.2f);
            }
            tutorialRotate = true;
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(3.5f);
            }
            StartCoroutine(SmoothAlphaChange(SkipTutorial, 1f, 1.5f));
        }
        else
        {
            for (int a = 0; a < 2; a++)
            {
                yield return new WaitForSeconds(1f);
            }
        }
        canSkipTutorial = true;
    }
}
