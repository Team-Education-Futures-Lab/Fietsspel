using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EndlessRunner : MonoBehaviour
{
    #region Singleton

    private static EndlessRunner _instance;
    public static EndlessRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EndlessRunner>();

                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(EndlessRunner).Name;
                    _instance = obj.AddComponent<EndlessRunner>();
                }
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private CheckForPath _checkforpath;

    public GameObject[] obstaclePatterns;
    public float scrollSpeed;
    public float laneDistance;
    public Transform playerTransform;
    public float patternSpawnRate;
    public float maxSpawnedSegments;

    [SerializeField] private GameObject startingSegment;

    public GameObject[] obstaclePatternsWithEvents;

    public int segmentPoolSize;
    public float offscreenSpawnPositionZ;
    public float spawnMoveDuration;

    private float nextSpawnTime;
    private float segmentLength;

    private int initialSegmentCount;
    private Vector3 offscreenSpawnPosition;

    private List<GameObject> segmentPool;
    private int segmentSpawnedCount;
    [SerializeField] private GameObject lastSegment;
    [SerializeField] private Animator animator;

    [SerializeField] internal protected bool hasStarted;
    [SerializeField] internal protected bool isPaused;

    void Start()
    {
        segmentPool = new List<GameObject>();
        offscreenSpawnPosition = new Vector3(0f, 0f, offscreenSpawnPositionZ);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        nextSpawnTime = Time.time + patternSpawnRate;
        segmentLength = laneDistance;

        InitializeSegmentPool();
        SpawnInitialSegments();

        segmentPool.Add(startingSegment);

        segmentSpawnedCount++;
        SpawnObstaclePattern();
    }

    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("StartRunning", true);

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
            {
                hasStarted = true;
            }

        }
        else
        {
            Debug.Log("Animator not found");
        }

        if (hasStarted && !_checkforpath.hasDied && !isPaused)
        {
            MoveEnvironment();

            if (hasStarted && Time.time >= nextSpawnTime)
            {
                segmentSpawnedCount++;
                SpawnObstaclePattern();
                nextSpawnTime = Time.time + patternSpawnRate;
            }
        }
    }

    void MoveEnvironment()
    {
        float targetZ = playerTransform.position.z + offscreenSpawnPositionZ;
        Vector3 movement = new Vector3(0f, 0f, -targetZ) * scrollSpeed * Time.deltaTime;

        transform.Translate(movement);

        foreach (Transform child in transform)
        {
            child.Translate(movement);
        }
    }

    void InitializeSegmentPool()
    {
        for (int i = 0; i < segmentPoolSize; i++)
        {
            GameObject segment = Instantiate(obstaclePatterns[Random.Range(0, obstaclePatterns.Length)], offscreenSpawnPosition, Quaternion.identity);
            segment.SetActive(false);
            segmentPool.Add(segment);
        }
    }

    void SpawnInitialSegments()
    {
        for (int i = 0; i < initialSegmentCount; i++)
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject segment = GetSegmentFromPool();

        float adjustedOffscreenSpawnPositionZ = offscreenSpawnPositionZ - (segmentSpawnedCount * segmentLength);
        segment.transform.position = new Vector3(0f, 0f, adjustedOffscreenSpawnPositionZ);

        segment.transform.parent = transform;

        segment.SetActive(true);

        StartCoroutine(MoveSegment(segment, new Vector3(0f, 0f, adjustedOffscreenSpawnPositionZ - segmentLength), spawnMoveDuration));

        if (segmentSpawnedCount > maxSpawnedSegments)
        {
            DeleteLastSegment();
        }
    }


    GameObject GetSegmentFromPool()
    {
        foreach (var segment in segmentPool)
        {
            if (!segment.activeSelf)
            {
                return segment;
            }
        }

        GameObject newSegment = Instantiate(obstaclePatterns[Random.Range(0, obstaclePatterns.Length)], offscreenSpawnPosition, Quaternion.identity);
        newSegment.SetActive(false);
        segmentPool.Add(newSegment);
        return newSegment;
    }

    IEnumerator MoveSegment(GameObject segment, Vector3 targetPosition, float duration)
    {
        float elapsed = 0f;
        Vector3 initialPosition = segment.transform.position;

        while (elapsed < duration)
        {
            segment.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        segment.transform.position = targetPosition;
    }

    void SpawnObstaclePattern()
    {
        SpawnSegment();
    }

    void DeleteLastSegment()
    {
        if(lastSegment != null)
        {
            Destroy(lastSegment);
        }

        Destroy(segmentPool[0]);
        segmentPool.RemoveAt(0);
    }
}