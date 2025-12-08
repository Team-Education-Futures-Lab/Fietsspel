using System.Collections;
using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    public GameObject[] collectiblePrefabs;      // Must contain 6 unique prefabs
    public float spawnDelay = 180f;              // Time between spawns

    [Header("Spawn Settings")]
    public float spawnDistanceAhead = 40f;       // How far in front of player
    public float safeCheckRadius = 1.5f;         // How far to search for obstacles
    public string obstacleTag = "Obstacle";      // Your obstacle tag (trees, rocks, etc.)

    [Header("Lane & Player References")]
    public Transform[] laneMarkers;              // Same lane markers as PlayerController
    public Transform player;

    private void Start()
    {
        if (collectiblePrefabs.Length != 6)
        {
            Debug.LogError("CollectibleSpawner: You must assign exactly 6 collectible prefabs!");
            return;
        }

        if (laneMarkers == null || laneMarkers.Length < 3)
        {
            Debug.LogError("CollectibleSpawner: Missing lane markers!");
            return;
        }

        // Make sure left → right
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
        int attempts = 0;
        int maxAttempts = 10;

        while (attempts < maxAttempts)
        {
            attempts++;

            int lane = Random.Range(0, laneMarkers.Length);

            Vector3 spawnPos = new Vector3(
                laneMarkers[lane].position.x,
                player.position.y + 1f,
                player.position.z + spawnDistanceAhead
            );

            // Sphere check for obstacle TAG
            Collider[] hits = Physics.OverlapSphere(spawnPos, safeCheckRadius);

            bool blocked = false;
            foreach (Collider hit in hits)
            {
                if (hit.CompareTag(obstacleTag))
                {
                    blocked = true;
                    break;
                }
            }

            if (!blocked)
            {
                Instantiate(prefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("CollectibleSpawner: Could not find a safe spawn position!");
    }

    // Shuffle collectibles so each spawns once in random order
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
