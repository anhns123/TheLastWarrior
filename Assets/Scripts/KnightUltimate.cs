using UnityEngine;

public class KnightUltimate : MonoBehaviour
{
    
    [SerializeField] private float delayTime = 0.8f; // Thời gian vận công
    [SerializeField] private float moveSpeed = 5f;   // Tốc độ bay
    private bool canMove = false;

    private SpriteRenderer spriteRenderer;
    private Collider2D c;
    private void Awake()
    {
        // ẩn hình và collider
        spriteRenderer = GetComponent<SpriteRenderer>();
        c = GetComponent<Collider2D>();
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (c != null)
            c.enabled = false;
    }
    void Start()
    {

        StartCoroutine(StartAfterDelay());
    }
     
    private System.Collections.IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);
        // Sau delay, hiện hình và cho bay
        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (c != null)
            c.enabled = true;

        canMove = true;
       
    }

    void Update()
    {

        if (canMove)
        {

            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
    }
}
