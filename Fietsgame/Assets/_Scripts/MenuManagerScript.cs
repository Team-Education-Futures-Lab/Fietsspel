using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject _COLLECTION;
    [SerializeField] private GameObject _MENU;
    [SerializeField] private GameObject _LEVELS;

    public void PlayLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void openCollection()
    {
        _COLLECTION.SetActive(true);
        _MENU.SetActive(false);
    }

    public void closeCollection()
    {
        _COLLECTION.SetActive(false);
        _MENU.SetActive(true);
    }

    public void openLevels()
    {
        _LEVELS.SetActive(true);
        _MENU.SetActive(false);
    }

    public void closeLevels()
    {
        _LEVELS.SetActive(false);
        _MENU.SetActive(true);
    }
}
