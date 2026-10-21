using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize components and get the Rigidbody2D reference from the GameObject
        rb = GetComponent<Rigidbody2D>();
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
        // Read input axis for horizontal movement (-1 for left, 1 for right)
        float moveInput = Input.GetAxis("Horizontal");
        
        // Apply horizontal velocity while preserving current vertical velocity
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    // Method to handle simple jump mechanics when touching the ground
    private void HandleJump()
    {
        // Check if the groundCheck transform is overlapping with the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Perform jump if Space key is pressed and character is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
