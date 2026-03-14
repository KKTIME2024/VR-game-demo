using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int maxEnemiesNumber;
    [SerializeField] private Transform player;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private float timeSinceLastSpawn;
    private float currentNumber;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= spawnInterval && currentNumber < maxEnemiesNumber)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0 || enemyPrefab == null) return;

        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        spawnedEnemies.Add(enemy);
        currentNumber++;
    }

}
