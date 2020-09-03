using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    int spawnCount;
    public Transform[] spawnPoints;
    public int minCount, maxCount;
    int count;

    private void Start()
    {
        count = Random.Range(minCount, maxCount);

        for (int i = 0; i < count; i++)
        {
            spawnCount = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[spawnCount].transform.position, Quaternion.Euler(0, Random.Range(0,359), 0));
        }
    }
}
