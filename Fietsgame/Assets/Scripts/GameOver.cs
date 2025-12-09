using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1 Final");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }

    // Update is called once per frame
    void Update()
    {
        PlayGame();
        QuitGame();
    }
}
