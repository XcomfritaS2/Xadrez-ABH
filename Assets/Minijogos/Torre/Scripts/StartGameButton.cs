using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject tutorialParent;
    public Button startButton;
    public GameObject gameManager; // Referência ao seu Garo script que controla o jogo

    private void Start()
    {
        //startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().gameStarted = true; // Atribua true à variável gameStarted no GameManager
        tutorialParent.gameObject.SetActive(false); // Desativa o botão para que não possa ser pressionado novamente
    }
}
