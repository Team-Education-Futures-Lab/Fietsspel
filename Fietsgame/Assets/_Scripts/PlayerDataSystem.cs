using UnityEngine;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    private const string CollectedItemsKey = "CollectedItems";
    private HashSet<string> collectedItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadCollectedItems();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCollectible(string collectibleName)
    {
        if (!collectedItems.Contains(collectibleName))
        {
            collectedItems.Add(collectibleName);
            PlayerPrefs.SetString("CollectedItems", string.Join(",", collectedItems));
            PlayerPrefs.Save();
            Debug.Log($"Added {collectibleName} to PlayerData. Current items: {string.Join(", ", collectedItems)}");
        }
        else
        {
            Debug.Log($"{collectibleName} is already in the collection.");
        }
    }


    public bool HasCollected(string itemName)
    {
        return collectedItems.Contains(itemName);
    }

    private void SaveCollectedItems()
    {
        PlayerPrefs.SetString(CollectedItemsKey, string.Join(",", collectedItems));
        PlayerPrefs.Save();
    }

    private void LoadCollectedItems()
    {
        if (PlayerPrefs.HasKey(CollectedItemsKey))
        {
            string savedItems = PlayerPrefs.GetString(CollectedItemsKey);
            collectedItems = new HashSet<string>(savedItems.Split(','));
        }
    }

    public void DebugCollectedItems()
    {
        if (PlayerPrefs.HasKey(CollectedItemsKey))
        {
            string savedItems = PlayerPrefs.GetString(CollectedItemsKey);
            Debug.Log($"Saved items in PlayerPrefs: {savedItems}");
        }
        else
        {
            Debug.Log("No items found in PlayerPrefs.");
        }
    }

}
