using UnityEngine;
using System.Collections;

public class SnowFallEffect : MonoBehaviour
{
    public PhysicsMaterial2D iceMaterial;
    public PhysicsMaterial2D normalMaterial;

    private Collider2D collidedPlatform;
    private bool applied = false;
    private const float SNOWFLAKE_LIFETIME = 3f;

    private void Start()
    {
        // Gán layer Snowflake để hệ thống phát hiện overlap
        gameObject.layer = LayerMask.NameToLayer("Snowflake");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!applied && collision.collider != null && collision.gameObject.CompareTag("Platform"))
        {
            collidedPlatform = collision.collider;

            if (collidedPlatform.sharedMaterial != iceMaterial)
            {
                collidedPlatform.sharedMaterial = iceMaterial;
                Debug.Log($"❄ {collidedPlatform.name} trở nên trơn trượt.");
            }

            applied = true;
            StartCoroutine(HandleSnowflakeLifecycle()); // Bắt đầu đếm ngược khi chạm platform
        }
    }

    private IEnumerator HandleSnowflakeLifecycle()
    {
        yield return new WaitForSeconds(SNOWFLAKE_LIFETIME);

        if (applied && collidedPlatform != null)
        {
            // Kiểm tra xem còn tuyết nào khác trên platform không
            Collider2D[] snowflakes = Physics2D.OverlapBoxAll(
                collidedPlatform.bounds.center,
                collidedPlatform.bounds.size,
                0f,
                LayerMask.GetMask("Snowflake")
            );

            if (snowflakes.Length == 1) 
            {
                collidedPlatform.sharedMaterial = normalMaterial;
                Debug.Log($"🧊 {collidedPlatform.name} trở lại bình thường.");
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (collidedPlatform != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(collidedPlatform.bounds.center, collidedPlatform.bounds.size);
        }
    }
}
