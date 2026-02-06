using UnityEngine;
using TMPro;

public class Counter : MonoBehaviour
{
    public static Counter Instance;

    public TextMeshProUGUI scoreText;
    public GameObject victoryMessage;

    private int currentCount = 0;
    private const int maxItems = 6;

    private void Awake()
    {
        Instance = this;
        if (victoryMessage != null) victoryMessage.SetActive(false);
    }

    private void Start() => UpdateUI();

    public void AddItem()
    {
        currentCount++;
        UpdateUI();
        if (currentCount >= maxItems && victoryMessage != null)
        {
            victoryMessage.SetActive(true);
        }
    }

    private void UpdateUI()
    {
        if (scoreText != null) scoreText.text = $"{currentCount}/{maxItems}";
    }
}