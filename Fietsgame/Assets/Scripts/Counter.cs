using UnityEngine;
using TMPro; // If you are using standard UI Text, change this to 'using UnityEngine.UI;'

public class counter : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI Reference")]
    public TextMeshProUGUI scoreText; // Drag your existing UI Text here

    private int count = 0;
    private const int total = 6;

    private void Awake() => Instance = this;

    private void Start()
    {
        UpdateDisplay();
    }

    public void IncrementScore()
    {
        count++;
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{count} / {total}";
        }
    }
}