using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TrackManager : MonoBehaviour
{
    public AssetReference[] prefabList;
    public List<GameObject> segmentsObject;
    public Player player;

    bool inGame = true;
    const int MIN_SEGMENT_COUNT = 20;
    int spawnedSegment = 0;
    int currentSegmentCount = 0;
    float segmentLenght = 2f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while(inGame)
        {
            while (currentSegmentCount < MIN_SEGMENT_COUNT)
            {
                prefabList[0].InstantiateAsync(segmentLenght * spawnedSegment * Vector3.forward, Quaternion.identity).Completed += OnLoadDone;
                spawnedSegment++;
                currentSegmentCount++;
                yield return null;
            }
            if (segmentsObject.Count == 0) yield return null;
            if (player.transform.position.z > GetOldestSegmentPosition() + segmentLenght)
            {
                var objToDelete = segmentsObject[0];
                segmentsObject.RemoveAt(0);
                Destroy(objToDelete);
                currentSegmentCount--;
                yield return null;
            }
            yield return null;

        }
    }

    private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // In a production environment, you should add exception handling to catch scenarios such as a null result.
        segmentsObject.Add(obj.Result);
    }

    private float GetOldestSegmentPosition()
    {
        return segmentsObject[0].transform.position.z + segmentLenght * 0.5f;
    }
}
