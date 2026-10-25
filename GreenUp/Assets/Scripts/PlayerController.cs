using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize required components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Execute player movement and jump checks on every frame
        HandleMovement();
        HandleJump();
    }

    // Method to handle horizontal movement using A/D or Arrow keys
    private void HandleMovement()
    {
        // Read horizontal movement input
        float moveInput = Input.GetAxis("Horizontal");

        // Apply horizontal velocity while preserving vertical velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update the Animator speed parameter
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
    }

    // Method to handle jump mechanics and jumping animation
    private void HandleJump()
    {
        // Check if the player is touching the ground
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            0.2f,
            groundLayer
        );

        // Update the Animator jumping state
        animator.SetBool("IsJumping", !isGrounded);

        // Perform jump if Space key is pressed and character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}