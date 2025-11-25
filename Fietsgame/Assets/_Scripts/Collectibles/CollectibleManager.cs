using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    [Header("Collectible Settings")]
    [SerializeField] private List<GameObject> collectiblePrefabs;
    [SerializeField] private float yAxisValue = 1f;
    [SerializeField] private float[] xAxisValues;
    [SerializeField] private float zAxisValue = 10f;

    [Header("Spawn Logic")]
    [SerializeField] private EndlessRunner worldScript;
    [SerializeField] private GameObject finishScreen;

    private GameObject currentCollectible;
    private int currentCollectibleIndex;

    private void Start()
    {
        TrySpawnCollectible();
    }

    private void Update()
    {
        if (currentCollectible == null)
        {
            TrySpawnCollectible();
        }
    }

    private void TrySpawnCollectible()
    {
        if (collectiblePrefabs.Count <= 0)
        {
            Debug.Log("All collectibles collected!");
            CollectedAllParts();
            return;
        }

        foreach (float xAxisValue in xAxisValues)
        {
            Vector3 spawnPosition = new Vector3(xAxisValue, yAxisValue, zAxisValue);
            if (IsValidSpawnPosition(spawnPosition))
            {
                SpawnCollectibleAt(spawnPosition);
                return;
            }
        }

        Debug.LogWarning("No valid spawn position available for collectible.");
    }

    private bool IsValidSpawnPosition(Vector3 position)
    {
        Vector3[] directions = {
            Vector3.forward,
            Vector3.up,
            Vector3.right,
            -Vector3.forward,
            -Vector3.up,
            -Vector3.right
        };

        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(position, direction);
            if (Physics.Raycast(ray, out RaycastHit hit, 3f))
            {
                if (hit.collider.CompareTag("Obstacle") || hit.collider.CompareTag("OutOfBounds"))
                {
                    Debug.Log($"Position {position} blocked by {hit.collider.tag}.");
                    return false;
                }
            }
        }

        return true;
    }

    private void SpawnCollectibleAt(Vector3 position)
    {
        currentCollectibleIndex = Random.Range(0, collectiblePrefabs.Count);
        GameObject collectiblePrefab = collectiblePrefabs[currentCollectibleIndex];

        currentCollectible = Instantiate(collectiblePrefab, position, Quaternion.identity, transform);
        Debug.Log($"Spawned collectible: {collectiblePrefab.name} at position: {position}");
    }

    public void OnCollectibleCollected()
    {
        if (currentCollectible == null)
        {
            Debug.LogError("No collectible to collect!");
            return;
        }

        string collectibleName = currentCollectible.name.Replace("(Clone)", "").Trim();
        Debug.Log($"Collectible collected: {collectibleName}");

        PlayerData.Instance?.AddCollectible(collectibleName);

        Destroy(currentCollectible);
        collectiblePrefabs.RemoveAt(currentCollectibleIndex);

        TrySpawnCollectible();
    }

    public void OnCollectiblePassed()
    {
        if (currentCollectible != null)
        {
            Debug.Log($"Collectible missed: {currentCollectible.name}");
            Destroy(currentCollectible);
            currentCollectible = null;
            TrySpawnCollectible();
        }
    }

    public void CollectedAllParts()
    {
        worldScript.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        finishScreen.SetActive(true);
    }
}
