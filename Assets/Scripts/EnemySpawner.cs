using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;

    public float spawnY = 8f; // cao hơn platform
    public float spawnXMin = -8f;
    public float spawnXMax = 8f;

    void Start()
    {
        SpawnWithDelay();
    }

    void SpawnWithDelay()
    {
        float delay = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke("SpawnEnemy", delay);
    }

    void SpawnEnemy()
    {
        float randomX = Random.Range(spawnXMin, spawnXMax);
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        SpawnWithDelay();
    }
}
