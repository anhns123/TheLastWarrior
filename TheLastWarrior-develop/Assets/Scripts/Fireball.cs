using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed = 10f;
    private float direction = 1f;

    public void SetDirection(float dir)
    {
        direction = dir;
        Vector3 scale = transform.localScale;
        if (Mathf.Sign(scale.x) != Mathf.Sign(dir))
        {
            scale.x *= -1;
        }
        transform.localScale = scale;
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject); // tự hủy khi ra khỏi màn hình
    }
}
