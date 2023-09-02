using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnRate;

    [SerializeField] float spawnPosX;
    [SerializeField] float spawnPosY;

    [SerializeField] List<Vector3> spawnPositions;
    float timeBtwSpawn;

    float minMaxPos;

    int enemySpawnCount;
    [SerializeField] int maxEnemySpawn = 20;

    void Start()
    {
        minMaxPos = Random.Range(0f, 1f);
    }

    void Update()
    {
        SettingSpawnPos();

        if (enemySpawnCount < maxEnemySpawn)
        {
            if (timeBtwSpawn <= 0)
            {
                minMaxPos = Random.Range(0f, 1f);
                Instantiate(enemy, spawnPositions[Random.Range(0, spawnPositions.Count)], Quaternion.identity);
                enemySpawnCount++;
                Debug.Log("Total Enemies : " + enemySpawnCount);
                timeBtwSpawn = spawnRate;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }

            if (spawnRate >= 1f) //might be replaced to a float variable
            {
                spawnRate -= Time.deltaTime * 0.1f;
            }
        }

    }

    private void SettingSpawnPos()
    {
        spawnPositions.Add(SpawnPosition(minMaxPos, -spawnPosY));
        spawnPositions.Add(SpawnPosition(minMaxPos, spawnPosY));
        spawnPositions.Add(SpawnPosition(spawnPosX, minMaxPos));
        spawnPositions.Add(SpawnPosition(-spawnPosX, minMaxPos));
    }

    private Vector3 SpawnPosition(float x, float y)
    {
        Vector3 viewPortMovement = Camera.main.ViewportToWorldPoint(new Vector3(x, y));
        Vector3 movement = Camera.main.WorldToViewportPoint(viewPortMovement);
        return movement;
    }

}
