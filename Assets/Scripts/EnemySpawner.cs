using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Gán prefab enemy vào đây
    public float minSpawnTime = 1f;     // Thời gian spawn tối thiểu
    public float maxSpawnTime = 3f;     // Thời gian spawn tối đa

    public float spawnY = 6f;           // Vị trí trục Y cố định (trên trời)
    public float spawnXMin = -8f;       // Giới hạn trái
    public float spawnXMax = 8f;        // Giới hạn phải

    void Start()
    {
        SpawnWithDelay(); // bắt đầu vòng lặp
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

        // Gọi lại để tiếp tục spawn
        SpawnWithDelay();
    }
}
