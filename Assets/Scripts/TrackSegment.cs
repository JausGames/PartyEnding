using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TrackSegment : MonoBehaviour
{
    public AssetReference[] obstaclesPrefabs;
    public List<GameObject> obstacleObjects;
    public List<GameObject> bonusObjects;


    void OnEnable()
    {
        StartCoroutine(SetUpObstacles());
    }

    IEnumerator SetUpObstacles()
    {
        obstaclesPrefabs[0].InstantiateAsync(transform.position, Quaternion.identity).Completed += OnObstaclesLoadDone;
        yield return null;
    }

    private void OnObstaclesLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        // In a production environment, you should add exception handling to catch scenarios such as a null result.
        obstacleObjects.Add(obj.Result);
    }
}
