using UnityEngine;

public class SnowSpawner : MonoBehaviour
{
    public GameObject snowflakePrefab;        // Prefab của tuyết
    public float spawnInterval = 1f;          // Thời gian giữa mỗi đợt tuyết rơi
    public float spawnYOffset = 2f;           // Vị trí tuyết spawn trên platform

    private void Start()
    {
        InvokeRepeating(nameof(SpawnSnowflakesOnClearPlatforms), 0f, spawnInterval);
    }

    void SpawnSnowflakesOnClearPlatforms()
    {
        Debug.Log("⛄ Kiểm tra các platform để spawn tuyết...");

        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");

        foreach (GameObject platform in platforms)
        {
            Collider2D platformCol = platform.GetComponent<Collider2D>();
            if (platformCol == null) continue;

            Collider2D[] snowflakes = Physics2D.OverlapBoxAll(
                platformCol.bounds.center,
                platformCol.bounds.size,
                0f,
                LayerMask.GetMask("Snowflake")
            );

            if (snowflakes.Length == 0)
            {
                Debug.Log($"→ Spawn tuyết trên: {platform.name}");

                Vector3 spawnPos = platform.transform.position + new Vector3(0f, spawnYOffset, 0f);
                GameObject snow = Instantiate(snowflakePrefab, spawnPos, Quaternion.identity);
                snow.layer = LayerMask.NameToLayer("Snowflake");
            }
        }
    }
}