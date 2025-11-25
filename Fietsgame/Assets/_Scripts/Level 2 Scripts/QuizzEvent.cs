using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizzEvent : MonoBehaviour
{
    public GameObject QuizzUIPanel;
    public TMP_Text QuizzQuestion;
    public Button Button1;
    public Button Button2;
    public Button Button3;

    private string currentSign;

    public string[] quizzAnswers;

    private EventSystemBase currentEvent;

    private void Start()
    {
        QuizzUIPanel.SetActive(false);
        Button1.onClick.AddListener(OnButtonOneClick);
        Button2.onClick.AddListener(OnButtonTwoClick);
        Button3.onClick.AddListener(OnButtonThreeClick);
    }

    public void ShowQuizz(string signName, EventSystemBase eventSystem)
    {
        currentSign = signName;
        currentEvent = eventSystem;
        QuizzQuestion.text = "Wat is dit voor bord?";
        QuizzUIPanel.SetActive(true);
    }

    public void OnButtonOneClick()
    {
        Debug.Log("Player pressed button 1");
        QuizzUIPanel.SetActive(false);

        if (currentEvent != null)
        {
            currentEvent.DeactivateEvent();
        }
        else
        {
            Debug.LogWarning("No event reference passed!");
        }
    }

    public void OnButtonTwoClick()
    {
        Debug.Log("Player pressed button 2");
        QuizzUIPanel.SetActive(false);

        if (currentEvent != null)
        {
            currentEvent.DeactivateEvent();
        }
        else
        {
            Debug.LogWarning("No event reference passed!");
        }
    }

    public void OnButtonThreeClick()
    {
        Debug.Log("Player pressed button 3");
        QuizzUIPanel.SetActive(false);

        if (currentEvent != null)
        {
            currentEvent.DeactivateEvent();
        }
        else
        {
            Debug.LogWarning("No event reference passed!");
        }
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("All data has been reset");
    }
}
