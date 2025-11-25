using System.Collections.Generic;
using UnityEngine;

public class CollectibleSpawnerManager : MonoBehaviour
{
    public GameObject player;
    public PlayerCollectiblesManager playerCollectiblesManager;
    public List<GameObject> collectiblePrefabs;

    private List<int> availableCollectibles = new List<int>();
    private int totalNumberOfCollectibles;

    void Start()
    {

        if (player != null)
        {
            playerCollectiblesManager = player.GetComponent<PlayerCollectiblesManager>();
        }

        for (int i = 0; i < collectiblePrefabs.Count; i++)
        {
            availableCollectibles.Add(i);
        }
    }

    void SpawnCollectible()
    {
        int itemIndex = Random.Range(0, availableCollectibles.Count);
        int collectibleIndex = availableCollectibles[itemIndex];

        float xPos = Random.Range(-3f, 3f);
        Vector3 spawnPosition = new Vector3(xPos, 0f, player.transform.position.z + 10f);

        GameObject collectible = Instantiate(collectiblePrefabs[collectibleIndex], spawnPosition, Quaternion.identity);
        availableCollectibles.RemoveAt(itemIndex);
        totalNumberOfCollectibles++;
    }
}
