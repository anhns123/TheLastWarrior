using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 12f;
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Attack")]
    public GameObject fireballPrefab;
    public Transform firePoint;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject gameOverUI;

    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;

        if (gameOverUI != null)
            gameOverUI.SetActive(false);
    }

    void Update()
    {
        GroundCheck();

        if (IsDead()) return;

        Move();

        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
            Jump();

        if (Keyboard.current.jKey.wasPressedThisFrame)
            Attack();

        if (Keyboard.current.kKey.wasPressedThisFrame)
            Dash();

        if (Keyboard.current.lKey.wasPressedThisFrame)
            UseSkill();
    }

    void Move()
    {
        moveInput = Keyboard.current.aKey.isPressed ? -1 :
                    Keyboard.current.dKey.isPressed ? 1 : 0;

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Set animation
        anim.SetFloat("Speed", Mathf.Abs(moveInput));

        // Quay mặt theo hướng
        if (moveInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);
        }
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        anim.SetBool("isJumping", true);
    }

    void Attack()
    {
        anim.SetTrigger("Attack");

        if (fireballPrefab != null && firePoint != null)
        {
            GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

            Fireball fb = fireball.GetComponent<Fireball>();
            if (fb != null)
            {
                fb.SetDirection(transform.localScale.x);
            }
            else
            {
                Debug.LogError("Fireball prefab is missing Fireball script!");
            }
        }
        else
        {
            Debug.LogError("FireballPrefab or FirePoint is not assigned!");
        }
    }

    void Dash()
    {
        anim.SetTrigger("Dash");
        rb.linearVelocity = new Vector2(transform.localScale.x * 12f, rb.linearVelocity.y);
    }

    void UseSkill()
    {
        anim.SetTrigger("Skill");
        // Gọi hiệu ứng, skill animation ở đây
    }

    void GroundCheck()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        anim.SetBool("isGrounded", isGrounded);

        // Nếu đang grounded thì không còn nhảy nữa
        if (isGrounded)
            anim.SetBool("isJumping", false);
    }

    public void TakeDamage(int damage)
    {
        if (IsDead()) return;

        currentHealth -= damage;
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            anim.SetTrigger("Death");

            if (gameOverUI != null)
                gameOverUI.SetActive(true);
        }
    }

    bool IsDead()
    {
        return currentHealth <= 0;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
}
