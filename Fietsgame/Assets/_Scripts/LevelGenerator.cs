using System.Globalization;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Level : MonoBehaviour
{

    public GameObject[] segment;

    [SerializeField] int zpos = 215;
    [SerializeField] bool creatingSegments = false;
    [SerializeField] int segmentNum;
    void Update()
    {
        if (creatingSegments == false)
        {
            creatingSegments = true;
            StartCoroutine(SegmentGen());
        }
    }

    IEnumerator SegmentGen()
    {
        segmentNum = Random.Range(0, 4);
        Instantiate(segment[segmentNum], new Vector3(0, 0, zpos), Quaternion.identity);
        zpos += 83;
        yield return new WaitForSeconds(4);
        creatingSegments = false;

    }
}
