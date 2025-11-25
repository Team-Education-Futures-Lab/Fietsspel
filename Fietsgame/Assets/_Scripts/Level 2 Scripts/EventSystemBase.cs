using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public abstract class EventSystemBase : MonoBehaviour 
{
    [Header("Event Settings")]
    public string eventName; 
    public bool isActive = true;


    [Header("CameraSettings")]
    private protected Camera mainCamera;
    public Transform cameraAnchor;
    public float camSmoothTime;
    private Vector3 initialPosition;
    private Transform playerTransform;
    private EndlessRunner sSpawner;

    [Header("UI Settings")]
    public Canvas trafficSignUI;
    public TMP_Text signTitle;
    public TMP_Text descriptionText;
    public Image signImage;

    private ScoreManager scoreManager;

    private void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player object not found!");
        }

        mainCamera = Camera.main;
        if (mainCamera != null && playerTransform != null)
        {
            initialPosition = new Vector3(playerTransform.position.x, playerTransform.position.y + 2.25f, playerTransform.position.z - 5);
        }

        sSpawner = FindObjectOfType<EndlessRunner>();
        if (sSpawner == null)
        {
            Debug.LogError("EndlessRunner instance not found! Ensure EndlessRunner script is present in the scene.");
        }

        GameObject uiParent = GameObject.Find("--UI--");

        if (uiParent != null)
        {
            trafficSignUI = uiParent.transform.Find("TrafficSignCanvas")?.GetComponent<Canvas>();

            if (trafficSignUI != null)
            {
                Transform imgTransform = trafficSignUI.transform.Find("TrafficSignImg");
                Transform titleTransform = trafficSignUI.transform.Find("TrafficSignTitle");
                Transform explanationTransform = trafficSignUI.transform.Find("TrafficSignExplanation");

                if (imgTransform != null)
                {
                    signImage = imgTransform.GetComponent<Image>();
                }
                else
                {
                    Debug.LogError("TrafficSignImg not found in TrafficSignCanvas");
                }

                if (titleTransform != null)
                {
                    signTitle = titleTransform.GetComponent<TMP_Text>();
                }
                else
                {
                    Debug.LogError("TrafficSignTitle not found in TrafficSignCanvas");
                }

                if (explanationTransform != null)
                {
                    descriptionText = explanationTransform.GetComponent<TMP_Text>();
                }
                else
                {
                    Debug.LogError("TrafficSignExplanation not found in TrafficSignCanvas");
                }
            }
            else
            {
                Debug.LogError("TrafficSignCanvas not found as a child of --UI--");
            }
        }
        else
        {
            Debug.LogError("--UI-- object not found in the scene");
        }
    }

    public virtual void TriggerEvent()
    {
        if (!isActive) return;

        Debug.Log($"Event {eventName} triggered.");
    }

    public void ActivateEvent()
    {
        if (sSpawner == null)
        {
            Debug.LogError("EndlessRunner instance is null. Cannot activate event.");
            return;
        }

        isActive = true;
        Debug.Log($"{eventName} event activated.");
        ZoomInOnEvent();
    }
    
    public void DeactivateEvent()
    {
        isActive = false;
        Debug.Log($"{eventName} event deactivated.");
        ZoomOutOnEvent();
    }

    public void ZoomOutOnEvent()
    {
        StartCoroutine(ZoomCoroutine(camSmoothTime));
    }


    public void ZoomInOnEvent()
    {
        if (cameraAnchor != null)
        {
            StartCoroutine(ZoomInCoroutine(cameraAnchor.position, camSmoothTime));
        }
        else
        {
            Debug.LogError("Camera anchor is not set for this event!");
        }
    }

    private IEnumerator ZoomInCoroutine(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        Vector3 currentPosition = mainCamera.transform.position;

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(currentPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        mainCamera.transform.position = targetPosition;
    }


    private IEnumerator ZoomCoroutine(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (playerTransform != null)
            {
                Vector3 targetPosition = new(
                    playerTransform.position.x,
                    playerTransform.position.y + 2.25f,
                    playerTransform.position.z - 5
                );

                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, elapsedTime / duration);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (playerTransform != null)
        {
            mainCamera.transform.position = new(
                playerTransform.position.x,
                playerTransform.position.y + 2.25f,
                playerTransform.position.z - 5
            );
        }
    }
}
