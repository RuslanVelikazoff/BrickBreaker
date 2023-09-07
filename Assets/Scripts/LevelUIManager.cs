using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelUIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;

    public Button homeButton;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        InitializedButtons();
    }

    private void Update()
    {
        UpdateScoreText();
        UpdateLivesText();
    }

    private void InitializedButtons()
    {
        if (homeButton != null)
        {
            homeButton.onClick.RemoveAllListeners();
            homeButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(0);
                gameManager.lives = 3;
                gameManager.score = 0;
            });
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + gameManager.score;
    }

    private void UpdateLivesText()
    {
        livesText.text = "Lives: " + gameManager.lives;
    }
}
