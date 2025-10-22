using UnityEngine;

public class BikeGameManager : MonoBehaviour
{
    public static BikeGameManager Instance;

    [Header("Game State")]
    public bool hasStarted = false;
    public bool isGameOver = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        hasStarted = true;
        isGameOver = false;
    }


    [Header("UI")]
    public GameObject gameOverScreen;

    public void EndGame()
    {
        hasStarted = false;
        isGameOver = true;


        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        Debug.Log("Game Over!");
    }

}