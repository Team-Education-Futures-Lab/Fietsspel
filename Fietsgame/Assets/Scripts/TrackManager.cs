using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [Header("Track Settings")]
    public GameObject groundPrefab;
    public int numSegments = 5;
    public float segmentLength = 20f;

    [Header("Spawning Settings")]
    public GameObject obstaclePrefab;
    public GameObject coinPrefab;
    public float laneOffset = 3f; // distance between lanes
    public int lanes = 3;         // left, middle, right
    public int obstaclesPerSegment = 2;
    public int coinsPerSegment = 3;

    private Queue<GameObject> segments = new Queue<GameObject>();
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;

        for (int i = 0; i < numSegments; i++)
        {
            SpawnSegment(i * segmentLength);
        }
    }

    void Update()
    {
        if (player.position.z - segments.Peek().transform.position.z > segmentLength)
        {
            GameObject old = segments.Dequeue();
            Destroy(old);
            SpawnSegment(segments.Last().transform.position.z + segmentLength);
        }
    }

    void SpawnSegment(float zPos)
    {
        // Spawn ground
        GameObject seg = Instantiate(groundPrefab, new Vector3(0, 0, zPos), Quaternion.identity);
        segments.Enqueue(seg);

        // Spawn obstacles
        for (int i = 0; i < obstaclesPerSegment; i++)
        {
            int lane = Random.Range(0, lanes); // pick lane (0 = left, 1 = middle, 2 = right)
            float xPos = (lane - 1) * laneOffset;
            float localZ = Random.Range(2f, segmentLength - 2f); // random position within segment

            Vector3 pos = new Vector3(xPos, 0.5f, zPos + localZ);
            Instantiate(obstaclePrefab, pos, Quaternion.identity, seg.transform);
        }

        // Spawn coins
        for (int i = 0; i < coinsPerSegment; i++)
        {
            int lane = Random.Range(0, lanes);
            float xPos = (lane - 1) * laneOffset;
            float localZ = Random.Range(2f, segmentLength - 2f);

            Vector3 pos = new Vector3(xPos, 1f, zPos + localZ);
            Instantiate(coinPrefab, pos, Quaternion.identity, seg.transform);
        }
    }

}
