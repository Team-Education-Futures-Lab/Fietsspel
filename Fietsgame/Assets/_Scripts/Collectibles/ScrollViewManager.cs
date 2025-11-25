using UnityEngine;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField] private GameObject[] collectibleCards;

    private void Start()
    {
        UpdateCollectedItems();
    }

    public void UpdateCollectedItems()
    {
        foreach (GameObject card in collectibleCards)
        {
            string itemName = card.name;
            bool hasCollected = PlayerData.Instance.HasCollected(itemName);
            Debug.Log($"Checking item: {itemName}, Collected: {hasCollected}");
            card.SetActive(hasCollected);
        }
    }

}
