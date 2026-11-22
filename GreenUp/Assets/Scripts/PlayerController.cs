using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip leftFootstepSound;
    [SerializeField] private AudioClip rightFootstepSound;
    [SerializeField] private float footstepInterval = 0.20f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;
    private float footstepTimer;
    private bool nextFootIsLeft = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        // Initialize required components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        // Handle player movement, jumping and footsteps
        HandleMovement();
        HandleJump();
        HandleFootsteps();
    }

    private void HandleMovement()
    {
        // Read horizontal movement input
        moveInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(
            moveInput * moveSpeed,
            rb.velocity.y
        );

        animator.SetFloat(
            "Speed",
            Mathf.Abs(moveInput)
        );
    }

    private void HandleJump()
    {
        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );

        animator.SetBool(
            "IsJumping",
            !isGrounded
        );

        // Accessible alternative input:
        // Space or W can be used to jump
        bool jumpPressed =
            Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.W);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(
                rb.velocity.x,
                jumpForce
            );

            if (audioSource != null && jumpSound != null)
            {
                audioSource.PlayOneShot(jumpSound);
            }
        }
    }

    private void HandleFootsteps()
    {
        if (!isGrounded || Mathf.Abs(moveInput) < 0.1f)
        {
            footstepTimer = 0f;
            return;
        }

        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0f)
        {
            AudioClip footstep = nextFootIsLeft
                ? leftFootstepSound
                : rightFootstepSound;

            if (audioSource != null && footstep != null)
            {
                audioSource.PlayOneShot(
                    footstep,
                    1.5f
                );
            }

            nextFootIsLeft = !nextFootIsLeft;
            footstepTimer = footstepInterval;
        }
    }
}