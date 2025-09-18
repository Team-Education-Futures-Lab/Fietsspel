using System.Collections;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [Header("Tutorial Settings")]
    public GameObject UIHolder;
    public Transform img;
    public TextMeshProUGUI text;

    public float slowWorldSpeed;

    public float smallScaleLimit;
    public float largeScaleLimit;

    public float scaleSpeed;
    public string requiredInput;

    private float targetScale;
    private float currentLerpTime;

    private bool isTutorialActive;

    private void Start()
    {
        targetScale = smallScaleLimit;
        isTutorialActive = false;
    }

    private void Update()
    {
        if (!isTutorialActive)
            return;

        currentLerpTime += Time.deltaTime * scaleSpeed;
        if (currentLerpTime >= 1f)
        {
            currentLerpTime = 0f;
            targetScale = (targetScale == smallScaleLimit) ? largeScaleLimit : smallScaleLimit;
        }

        float newScale = Mathf.Lerp(text.transform.localScale.x, targetScale, currentLerpTime);
        text.transform.localScale = new Vector3(newScale, newScale, newScale);

        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(requiredInput))
            {
                ResetWorldSpeed();
            }
            else
            {
                Debug.Log("Invalid input. Please press the correct button.");
            }
        }
    }

    public void SlowDownWorld()
    {
        UIHolder.SetActive(true);
        Time.timeScale = slowWorldSpeed;
        isTutorialActive = true;
    }

    public void ResetWorldSpeed()
    {
        UIHolder.SetActive(false);
        Time.timeScale = 1f;
        isTutorialActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SlowDownWorld();
        }
    }
}
