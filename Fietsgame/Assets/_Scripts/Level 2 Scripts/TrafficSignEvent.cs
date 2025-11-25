using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TrafficSignEvent : EventSystemBase
{
    [Header("Traffic Sign Data")]
    public TrafficSignData trafficSign;
    private bool eventTriggered = false;

    private QuizzEvent quizzEv;

    private void Start()
    {
        quizzEv = FindObjectOfType<QuizzEvent>();
    }

    private void Update()
    {
        if (eventTriggered && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueLevel();
        }
    }

    private void SetTrafficSignUI()
    {
        signTitle.text = trafficSign.signName;
        descriptionText.text = trafficSign.description;
        trafficSignUI.gameObject.SetActive(true);
        ActivateEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (eventTriggered || !isActive) return;
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player encountered traffic sign: " + trafficSign.signName);

            if (HasSeenSign(trafficSign.signName))
            {
              
                Debug.Log("Player has already seen this sign. Showing quiz.");
                quizzEv.ShowQuizz(trafficSign.signName, this);
                ActivateEvent();
            }
            else
            { 
                Debug.Log("First time encountering this sign. Showing traffic sign UI.");
                eventTriggered = true; 
                SetTrafficSignUI();
            }
        }
    }

    public void ContinueLevel()
    {
        Debug.Log("Continuing level after traffic sign: " + trafficSign.signName);

        if (trafficSignUI != null)
        {
            trafficSignUI.gameObject.SetActive(false);
        }

        DeactivateEvent(); 
        SaveSignEncounter(trafficSign.signName); 
    }

    public void SaveSignEncounter(string signName)
    {
        Debug.Log("Saving encounter for sign: " + signName);
        PlayerPrefs.SetInt(signName, 1); 
        PlayerPrefs.Save();
    }

    private bool HasSeenSign(string signName)
    {
        return PlayerPrefs.GetInt(signName, 0) == 1;
    }
}
