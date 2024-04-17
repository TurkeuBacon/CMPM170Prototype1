using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerNew : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    void Start()
    {
        int numEnemies = Random.Range(5, 10);

        for (int i = 0; i < numEnemies; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            SpawnEnemy(3);
        }

    }

    public void SpawnEnemy(int enemyIndex)
    {
        Vector2 spawnLocation = new Vector2(Random.Range(-40, -20), Random.Range(15, 30));
        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], spawnLocation, Quaternion.identity);
    }
}