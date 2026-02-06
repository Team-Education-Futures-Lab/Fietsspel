using System.Collections;
using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    public GameObject[] collectiblePrefabs;
    public float spawnDelay = 10f;

    [Header("Spawn Settings")]
    public float spawnDistanceAhead = 40f;
    public float safeCheckRadius = 2.0f; // Increase this if it still spawns too close to trees
    public string obstacleTag = "Obstacle"; // Set this to "Obstacle" in the Inspector

    [Header("Lane & Player References")]
    public Transform[] laneMarkers;
    public Transform player;

    private void Start()
    {
        if (collectiblePrefabs == null || collectiblePrefabs.Length == 0) return;
        if (player == null) return;

        System.Array.Sort(laneMarkers, (a, b) => a.position.x.CompareTo(b.position.x));

        ShuffleCollectibles();
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < collectiblePrefabs.Length; i++)
        {
            SpawnCollectibleSafe(collectiblePrefabs[i]);
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void SpawnCollectibleSafe(GameObject prefab)
    {
        int maxAttempts = 20;
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            attempts++;

            int laneIndex = Random.Range(0, laneMarkers.Length);
            Transform selectedLane = laneMarkers[laneIndex];

            Vector3 spawnPos = new Vector3(
                selectedLane.position.x,
                player.position.y + 1.2f,
                player.position.z + spawnDistanceAhead
            );

            // --- TAG CHECK LOGIC ---
            // Find all colliders in the area
            Collider[] colliders = Physics.OverlapSphere(spawnPos, safeCheckRadius);
            bool isBlocked = false;

            foreach (var col in colliders)
            {
                if (col.CompareTag(obstacleTag))
                {
                    isBlocked = true;
                    break;
                }
            }

            if (!isBlocked)
            {
                GameObject item = Instantiate(prefab, spawnPos, Quaternion.identity);
                item.AddComponent<CollectiblePickup>();
                return;
            }
        }

        Debug.LogWarning("Spawner: Skip spawn - could not find a lane without an Obstacle tag.");
    }

    private void ShuffleCollectibles()
    {
        for (int i = collectiblePrefabs.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            var temp = collectiblePrefabs[i];
            collectiblePrefabs[i] = collectiblePrefabs[rand];
            collectiblePrefabs[rand] = temp;
        }
    }
}