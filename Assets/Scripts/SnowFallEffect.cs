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
        Destroy(gameObject, SNOWFLAKE_LIFETIME);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!applied && collision.collider != null && collision.gameObject.CompareTag("Platform"))
        {
            collidedPlatform = collision.collider;

            if (collidedPlatform.sharedMaterial != iceMaterial)
            {
                collidedPlatform.sharedMaterial = iceMaterial;
                Debug.Log($"[Platform] {collidedPlatform.name} trở nên trơn trượt.");
                applied = true;
            }

            gameObject.layer = LayerMask.NameToLayer("Snowflake");
        }
    }

    private void OnDestroy()
    {
        if (applied && collidedPlatform != null)
        {
            StartCoroutine(DelayedRevertMaterial(SNOWFLAKE_LIFETIME));
        }
    }

    private IEnumerator DelayedRevertMaterial(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (collidedPlatform != null)
        {
            Collider2D[] snowflakes = Physics2D.OverlapBoxAll(
                collidedPlatform.bounds.center,
                collidedPlatform.bounds.size,
                0f,
                LayerMask.GetMask("Snowflake")
            );

            if (snowflakes.Length == 0)
            {
                collidedPlatform.sharedMaterial = normalMaterial;
                Debug.Log($"[Platform] {collidedPlatform.name} trở lại bình thường.");
            }
        }
    }
}
