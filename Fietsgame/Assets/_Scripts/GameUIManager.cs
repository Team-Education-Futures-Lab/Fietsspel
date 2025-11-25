using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager1 : MonoBehaviour
{
    [SerializeField] private CheckForPath CFP;
    [SerializeField] private GameObject FinishedBike;
    [SerializeField] private EndlessRunner worldScript;
    [SerializeField] private CollectibleManager colManager;

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        colManager.enabled = false;
        worldScript.isPaused = false;
        CFP.hasDied = false;
        FinishedBike.SetActive(false);
    }
}
