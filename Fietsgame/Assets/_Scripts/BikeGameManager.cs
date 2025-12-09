using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void EndGame()
    {
        if (isGameOver) return; // prevent double-triggering

        hasStarted = false;
        isGameOver = true;

        Debug.Log("Game Over!");

        // Load your Game Over scene
        SceneManager.LoadScene("GameOver");
    }
}
