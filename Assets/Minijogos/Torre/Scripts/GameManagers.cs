using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text jogador1UltimaLetraText;
    public Text jogador2UltimaLetraText;

    public Text player1Text;
    public Text player2Text;
    public Text player1ScoreText;
    public Text player2ScoreText;
    public Text winnerText; // Texto do vencedor
    public int maxScore = 28; // Pontuação máxima para vencer
    public WinnerScreenManager winnerScreenManager;

    public bool gameStarted = false;
    public Text startText;

    private Dictionary<string, List<KeyCode>> keysDictionary = new Dictionary<string, List<KeyCode>>()
    {
        { "PlayerWhite", new List<KeyCode>{ KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D } },
        { "PlayerBlack", new List<KeyCode>{ KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow } }
    };

    private Dictionary<string, List<KeyCode>> letrasPressionadas = new Dictionary<string, List<KeyCode>>()
    {
        { "PlayerWhite", new List<KeyCode>() },
        { "PlayerBlack", new List<KeyCode>() }
    };

    #region 
    //Criei essa parte pois não consegui fazer a base por cima do seu código sorry
    private List<KeyCode> keysPlayer1;
    private List<KeyCode> keysPlayer2;

    public KeyCode currentKey1;
    public KeyCode currentKey2;
    #endregion

    private int player1Score = 0;
    private int player2Score = 0;
    private bool gameOver = false;

    private void Start()
    {
        //não nulo
        keysPlayer1 = new List<KeyCode>();
        keysPlayer2 = new List<KeyCode>();

        //lista 1 (branco)
        keysPlayer1.Add(KeyCode.W);
        keysPlayer1.Add(KeyCode.S);
        keysPlayer1.Add(KeyCode.A);
        keysPlayer1.Add(KeyCode.D);

        //lista 2 (preto)
        keysPlayer2.Add(KeyCode.UpArrow);
        keysPlayer2.Add(KeyCode.DownArrow);
        keysPlayer2.Add(KeyCode.LeftArrow);
        keysPlayer2.Add(KeyCode.RightArrow);

        //define as primeiras keys aleatórias
        currentKey1 = GetNextKey(keysPlayer1);
        currentKey2 = GetNextKey(keysPlayer2);

        //mostrar a primeira key
        player1Text.text = GetKeyName(currentKey1);
        player2Text.text = GetKeyName(currentKey2);

        startText.text = "Mini Tutorial: Neste jogo, os jogadores devem pressionar os botões o mais rápido possível para vencer. O Player 1 usa as teclas  W, A, S, D, enquanto o Player 2 usa as setas (CIMA, BAIXO, ESQUERDA, DIREITA). O primeiro jogador a alcançar 50 pontos ganha. Pressione o botão para começar o jogo";

        UpdateScore();
        /*NextKey("PlayerWhite");
        NextKey("PlayerBlack");*/

        // Esconder a tela de vencedor ao iniciar o jogo
        winnerScreenManager.HideWinner();
    }

    private void Update()
    {
        if (!gameOver && gameStarted) 
        {

            //entendi lukas eu entendi agora :o
            if (Input.anyKeyDown)
                for (int i = 0; i < keysPlayer1.Count; i++)
                {
                    //se clicado
                    if (Input.GetKeyDown(keysPlayer1[i]))
                    {
                        //se acertar
                        if (keysPlayer1[i] == currentKey1)
                        {
                            player1Score++;

                            //adiciona uma letra pra lista de letras apertadas
                            jogador1UltimaLetraText.text += GetKeyName(keysPlayer1[i]);

                            //pegar a key passada para não haver repetições
                            var previousKey = currentKey1;

                        redefine1:
                            //definir próxima key aleatória
                            currentKey1 = GetNextKey(keysPlayer1);

                            //sem repetições!!!
                            if (currentKey1 == previousKey)
                                goto redefine1;

                            //mostrar a próxima key
                            player1Text.text = GetKeyName(currentKey1);
                        }
                        //senao
                        else
                        {
                            //sem números negativos! >:)
                            if (player1Score > 0)
                                player1Score--;
                        }
                    }
                    if (Input.GetKeyDown(keysPlayer2[i]))
                    {
                        //se acertar
                        if (keysPlayer2[i] == currentKey2)
                        {
                            player2Score++;

                            //adiciona uma letra pra lista de letras apertadas
                            jogador2UltimaLetraText.text += GetKeyName(currentKey2);

                            //pegar a key passada para não haver repetições
                            var previousKey = currentKey2;

                        redefine2:
                            //definir próxima key aleatória
                            currentKey2 = GetNextKey(keysPlayer2);

                            //sem repetições!!!
                            if (currentKey2 == previousKey)
                                goto redefine2;

                            //mostrar a próxima key
                            player2Text.text = GetKeyName(currentKey2);
                        }
                        //senao
                        else
                        {
                            //sem números negativos! >:)
                            if (player2Score > 0)
                                player2Score--;
                        }
                    }
                }
            UpdateScore();

            /*bool keyPressed = false;
            foreach (var playerKeys in keysDictionary)
            {
                if (Input.GetKeyDown(playerKeys.Value[0]))
                {
                    if (playerKeys.Value.Contains(keysDictionary["PlayerWhite"][0]) || playerKeys.Value.Contains(keysDictionary["PlayerBlack"][0]))
                    {
                        // Tecla correta pressionada, aumenta o score do jogador
                        UpdatePlayerScore(playerKeys.Key);
                        UpdateUltimaLetra(playerKeys.Key, letrasPressionadas[playerKeys.Key], playerKeys.Key == "PlayerWhite" ? jogador1UltimaLetraText : jogador2UltimaLetraText);
                    }
                    else
                    {
                        // Tecla errada pressionada, diminui o score do jogador correspondente
                        if (playerKeys.Key == "PlayerWhite")
                        {
                            player1Score--;
                        }
                        else if (playerKeys.Key == "PlayerBlack")
                        {
                            player2Score--;
                        }
                        UpdateScore();
                    }
                    keyPressed = true;
                    break; // Saia do loop assim que uma tecla for pressionada
                }

            }

            // Se nenhuma tecla válida foi pressionada, o jogador que pressionou perde um ponto
            if (!keyPressed)
            {
                if (player1Score > 0)
                {
                    player1Score--;
                }
                if (player2Score > 0)
                {
                    player2Score--;
                }
                UpdateScore();
            }*/
        }

        if (!gameOver && (player1Score >= maxScore || player2Score >= maxScore))
        {
            gameOver = true;
            Debug.Log("Fim de jogo!");

            // Mostrar tela de vencedor
            string winner = player1Score >= maxScore ? "Player 1" : "Player 2";
            winnerText.text = "Vencedor: " + winner; // Define o texto do vencedor
            winnerScreenManager.ShowWinner(winner);
        }
    }

    private void UpdatePlayerScore(string playerKey)
    {
        if (playerKey == "PlayerWhite")
        {
            player1Score++;
            letrasPressionadas["PlayerWhite"].Add(keysDictionary[playerKey][0]);
            NextKey(playerKey);
        }
        else if (playerKey == "PlayerBlack")
        {
            player2Score++;
            letrasPressionadas["PlayerBlack"].Add(keysDictionary[playerKey][0]);
            NextKey(playerKey);
        }
        UpdateScore();
    }

    private void UpdateKeyText(Text text, KeyCode key)
    {
        text.text = GetKeyName(key);
    }

    private string GetKeyName(KeyCode keyCode)
    {
        switch (keyCode)
        {
            case KeyCode.W:
                return "W";
            case KeyCode.UpArrow:
                return "Cima";
            case KeyCode.DownArrow:
                return "Baixo";
            case KeyCode.LeftArrow:
                return "Esquerda";
            case KeyCode.RightArrow:
                return "Direita";
            default:
                return keyCode.ToString();
        }
    }

    private void UpdateScore()
    {
        player1ScoreText.text = " " + player1Score.ToString();
        player2ScoreText.text = " " + player2Score.ToString();
    }

    private KeyCode NextKey(string player)
    {
        List<KeyCode> keys = keysDictionary[player];

        // Remove a tecla atual da lista
        KeyCode currentKey = keys[0];
        keys.Remove(currentKey);

        // Embaralha a lista de teclas
        Shuffle(keys);

        // Adiciona a tecla atual de volta à lista
        keys.Add(currentKey);

        // Define a próxima tecla a ser pressionada
        KeyCode nextKey = keys[0];

        // Atualiza o texto da tecla para exibir o nome mais amigável
        UpdateKeyText(player == "PlayerWhite" ? player1Text : player2Text, nextKey);

        return nextKey;
    }

    private void UpdateUltimaLetra(string playerKey, List<KeyCode> letras, Text textoUltimaLetra)
    {
        string letrasText = " ";
        foreach (var letra in letras)
        {
            letrasText += letra.ToString() + " ";
        }
        textoUltimaLetra.text = letrasText;
    }

    private void Shuffle(List<KeyCode> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            KeyCode temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public List<KeyCode> GetProximasTeclas(string player, int quantidade)
    {
        List<KeyCode> proximasTeclas = new List<KeyCode>();

        List<KeyCode> keys = keysDictionary[player];

        // Embaralha as teclas
        List<KeyCode> teclasEmbaralhadas = new List<KeyCode>(keys);
        Shuffle(teclasEmbaralhadas);

        // Adiciona a próxima tecla correta no início da lista
        proximasTeclas.Add(keys[0]);

        // Adiciona as demais teclas embaralhadas à lista de próximas teclas
        for (int i = 0; i < quantidade - 1 && i < teclasEmbaralhadas.Count; i++)
        {
            proximasTeclas.Add(teclasEmbaralhadas[i]);
        }

        return proximasTeclas;
    }
    public KeyCode GetNextKey(List<KeyCode> list) => list[Random.Range(0, list.Count)];
}
