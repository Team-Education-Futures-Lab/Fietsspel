using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> collectiblePrefabs;

    private GameObject currentCollectible;
    [SerializeField] private bool canSpawnHere = true;
    [SerializeField] private float yAxisValue;
    [SerializeField] private float[] xAxisValue;
    [SerializeField] private float zAxisValue;

    private int currentCollectibleIndex;
    private int xAxisIndex;


    [SerializeField] private GameObject FinishedBike;

    void Update()
    {
        if (currentCollectible == null)
        {
            CheckForPath();
            if (canSpawnHere)
            {
                SpawnCollectible();
            }
        }
        
        if(collectiblePrefabs.Count == 0) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            FinishedBike.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    private void SpawnCollectible()
    {
        Debug.Log($"Attempting to spawn collectible. canSpawnHere: {canSpawnHere}, collectibles.Count: {collectiblePrefabs.Count}, xAxisIndex: {xAxisIndex}");

        if (canSpawnHere && collectiblePrefabs.Count > 0)
        {
            currentCollectibleIndex = Random.Range(0, collectiblePrefabs.Count);
            GameObject collectiblePrefab = collectiblePrefabs[currentCollectibleIndex];
            Vector3 spawnPosition = new Vector3(xAxisValue[xAxisIndex], yAxisValue, zAxisValue);

            Debug.Log($"Spawning collectible '{collectiblePrefab.name}' at position {spawnPosition}");

            currentCollectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity, transform);

            xAxisIndex = (xAxisIndex + 1) % xAxisValue.Length;
        }
        else
        {
            Debug.Log("No valid spawn position or collectible array is empty.");
        }
    }

    private void CheckForPath()
    {
        Vector3[] directions = {
            transform.forward,
            transform.up,
            transform.right,
            -transform.forward,
            -transform.up,
            -transform.right
        };

        canSpawnHere = true;
        Debug.Log("Checking path for valid spawn location...");

        foreach (Vector3 direction in directions)
        {
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * 2f, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hitData, 2f))
            {
                string hitTag = hitData.collider.tag;
                Debug.Log($"Raycast hit: {hitTag}");

                if (hitTag == "Obstacle" || hitTag == "OutOfBounds")
                {
                    canSpawnHere = false;
                    Debug.Log("Spawn blocked by an obstacle or out-of-bounds area.");
                    break;
                }
            }
        }
    }

    public void OnCollectiblePassed()
    {
        if (currentCollectible != null)
        {
            Destroy(currentCollectible);
            currentCollectible = null;
        }
    }

    public void OnCollectibleCollected()
    {
        if (PlayerData.Instance == null)
        {
            Debug.LogError("PlayerData.Instance is null!");
            return;
        }

        if (currentCollectible == null)
        {
            Debug.LogError("currentCollectible is null!");
            return;
        }

        string cleanName = currentCollectible.name.Replace("(Clone)", "").Trim();

        PlayerData.Instance.AddCollectible(cleanName);
        Debug.Log($"Added {cleanName} to PlayerData.");

        Destroy(currentCollectible.gameObject);
        collectiblePrefabs.RemoveAt(currentCollectibleIndex);
        SpawnCollectible();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Triggered by: {other.name} with tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            OnCollectibleCollected();
        }
    }
}
