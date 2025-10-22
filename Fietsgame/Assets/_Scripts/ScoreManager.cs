using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;

    private float scoreIncreaseTimer = 0f;
    private float scoreIncreaseInterval = 0.05f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        score = 0;
        UpdateScoreUI();
        UpdateHighScoreUI();
    }

    void Update()
    {
        if (BikeGameManager.Instance != null && BikeGameManager.Instance.hasStarted)
        {
            scoreIncreaseTimer += Time.deltaTime;

            if (scoreIncreaseTimer >= scoreIncreaseInterval)
            {
                IncreaseScore(1);
                scoreIncreaseTimer = 0f;
            }
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            UpdateHighScoreUI();
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = score.ToString("D6");
    }

    void UpdateHighScoreUI()
    {
        highScoreText.text = "Hoogste: " + highScore.ToString("D6");
    }

    void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}