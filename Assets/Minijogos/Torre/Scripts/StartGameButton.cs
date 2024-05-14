using UnityEngine;
using UnityEngine.UI;

public class StartGameButton : MonoBehaviour
{
    public GameObject tutorialParent;
    public Button startButton;
    public GameObject gameManager; // Refer�ncia ao seu Garo script que controla o jogo

    private void Start()
    {
        //startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        gameManager.GetComponent<GameManager>().gameStarted = true; // Atribua true � vari�vel gameStarted no GameManager
        tutorialParent.gameObject.SetActive(false); // Desativa o bot�o para que n�o possa ser pressionado novamente
    }
}
