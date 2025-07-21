using System;
using UnityEngine;
using UnityEngine.Windows;
using Input = UnityEngine.Input;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Key Bindings")]
    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode attackKey = KeyCode.F;
    [SerializeField] private KeyCode ultiKey = KeyCode.G;

    [Header("Cài đặt di chuyển")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float delayAttack = 0.4f;

    [Header("Kiểm tra mặt đất")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    [Header("Vũ khí")]
    [SerializeField] private WeaponController weapon;

    private Rigidbody2D rb;
    private Animator animator;

    private float velocityY;
    private float lastY;
    private Vector2 input;
    private bool isAttack = false;
    private bool isUltimate = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
    }

    private void Start()
    {
        lastY = transform.position.y;
    }

    private void Update()
    {
        //Input
        HandleInput();
        //Set Move
        if (!isAttack && !isUltimate)
        {
            HandleJump(input.y);
            MovePlayer(input.x);
            HandleFlip(input.x);
        }
        else HandleAttack();

        //Set animation
        UpdateAnimation(input);
    }

    private void HandleAttack()
    {

        if (isAttack)
        {
            //Attacked
            weapon.NormalAttack();
        }
        if (isUltimate)
        {
            //Attack by skill
            weapon.UltimateAttack();
        }
    }

    private void HandleInput()
    {
        input = Vector2.zero;

        if (Input.GetKey(leftKey))
            input.x = -1;
        if (Input.GetKey(rightKey))
            input.x = 1;
        if (Input.GetKey(upKey))
            input.y = 1;
        if (Input.GetKey(downKey))
            input.y = -1;
        //Attack , not move
        if (Input.GetKey(attackKey))
        {
            isAttack = true;
            Invoke(nameof(EndAttack), delayAttack);
        }
        if (Input.GetKey(ultiKey))
        {
            isUltimate = true;
            Invoke(nameof(EndAttack), delayAttack);
        }
    }
    void EndAttack()
    {
        isAttack = false;
        isUltimate = false;
    }
    private void MovePlayer(float horizontal)
    {
        if (horizontal != 0) rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void HandleJump(float vertical)
    {
        if (vertical > 0 && IsGrounded())
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
    private void HandleDef(float vertical)
    {
        if (vertical < 0 && IsGrounded())
        {
            //Defence
        }
    }
    private void HandleFlip(float horizontal)
    {
        if (horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    private void UpdateAnimation(Vector2 direction)
    {
        if (animator == null) return;

        // Tính tốc độ chạy
        animator.SetFloat("Speed", Mathf.Abs(direction.x));

        // Tính vận tốc trục Y để xử lý Jump/Fall
        float currentY = transform.position.y;
        velocityY = (currentY - lastY);
        lastY = currentY;
        animator.SetFloat("VelocityY", velocityY);

        // Gán trạng thái mặt đất
        animator.SetBool("IsGrounded", IsGrounded());

        //Attack
        animator.SetBool("isAttack", isAttack);

        //Attack by ultimate
        animator.SetBool("isUltimate", isUltimate);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
