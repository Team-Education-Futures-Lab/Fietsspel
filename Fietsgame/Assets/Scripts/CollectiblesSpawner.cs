using System.Collections;
using UnityEngine;

public class CollectiblesSpawner : MonoBehaviour
{
    [Header("Collectibles")]
    public GameObject[] collectiblePrefabs;      // Must contain 6 unique prefabs
    public float spawnDelay = 180f;              // Time between spawns

    [Header("Spawn Settings")]
    public float spawnDistanceAhead = 40f;
    public float safeCheckRadius = 1.5f;
    public string obstacleTag = "Obstacle";

    [Header("Lane & Player References")]
    public Transform[] laneMarkers;
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
                // --- NEW LOGIC START ---
                GameObject spawnedItem = Instantiate(prefab, spawnPos, Quaternion.identity);

                // Add the destruction component automatically at runtime
                CollectibleDestruction handler = spawnedItem.AddComponent<CollectibleDestruction>();
                handler.playerTag = "Player"; // Ensure your player has this tag
                // --- NEW LOGIC END ---
                return;
            }
        }

        Debug.LogWarning("CollectibleSpawner: Could not find a safe spawn position!");
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

// Small helper class included in the same file to handle the "Destroy"
public class CollectibleDestruction : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            // You can add score logic here later!
            Destroy(gameObject);
        }
    }
}