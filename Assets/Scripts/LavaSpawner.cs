using UnityEngine;

public class LavaSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemyPrefab;
    public Transform[] platforms; // Mảng chứa 3 platform
    public float spawnInterval = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        // Chọn ngẫu nhiên một platform
        Transform platform = platforms[Random.Range(0, platforms.Length)];

        // Lấy chiều rộng platform (giả sử scale.x là chiều dài)
        float platformWidth = platform.localScale.x;
        float leftBound = platform.position.x - platformWidth / 2f;
        float rightBound = platform.position.x + platformWidth / 2f;

        // Tạo vị trí spawn ngẫu nhiên trên nền
        float randomX = Random.Range(leftBound, rightBound);
        float y = platform.position.y + 0.5f;

        Vector2 spawnPos = new Vector2(randomX, y);
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        Destroy(enemy, 3f); // Tự xoá sau 3 giây
    }
}
