using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public GameObject snowflakePrefab;
    public GameObject[] platforms;             // Danh sách platform
    public float spawnYOffset = 2f;            // Độ cao rơi từ trên platform
    public float snowLifetime = 1.5f;          // Thời gian tồn tại của tuyết
    public float minDelay = 3f;                // Delay giữa các lượt spawn
    public float maxDelay = 4f;

    void Start()
    {
        // Nếu chưa gán trong Inspector thì tự tìm theo tag
        if (platforms == null || platforms.Length == 0)
        {
            platforms = GameObject.FindGameObjectsWithTag("Platform");
        }

        // Bắt đầu vòng lặp sinh tuyết
        SpawnWithDelay();
    }

    void SpawnWithDelay()
    {
        float delay = Random.Range(minDelay, maxDelay);
        Invoke("SpawnSnow", delay);
    }

    void SpawnSnow()
    {
        if (platforms.Length == 0) return;

        // Chọn 1 platform ngẫu nhiên
        GameObject platform = platforms[Random.Range(0, platforms.Length)];
        Collider2D col = platform.GetComponent<Collider2D>();
        if (col == null) return;

        // Tính vị trí random theo chiều ngang trong platform
        float width = col.bounds.size.x;
        float randomXOffset = Random.Range(-width / 2f, width / 2f);
        Vector3 spawnPos = platform.transform.position + new Vector3(randomXOffset, spawnYOffset, 0f);

        // Sinh bông tuyết
        GameObject snow = Instantiate(snowflakePrefab, spawnPos, Quaternion.identity);

        // Hủy tuyết sau 1.5 giây
        Destroy(snow, snowLifetime);

        // Tiếp tục vòng lặp
        SpawnWithDelay();
    }
}
