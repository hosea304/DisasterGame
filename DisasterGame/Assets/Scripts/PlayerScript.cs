using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer; // Set this to the layer your platforms are on

    [Header("Physics Settings")]
    [SerializeField] private float gravityMultiplier = 2.5f;
    [SerializeField] private float fallMultiplier = 2.5f;

    private Rigidbody rb;
    private bool canJump = true;
    private bool isGrounded;
    private float horizontalInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

        // Set higher gravity
        Physics.gravity = new Vector3(0, -9.81f * gravityMultiplier, 0);
    }

    void Update()
    {
        // Get input
        horizontalInput = 0f;
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Ground check
        CheckGrounded();
    }

    void FixedUpdate()
    {
        // Apply movement
        Vector3 targetVelocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = targetVelocity;

        // Apply additional gravity when falling
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void CheckGrounded()
    {
        // Cast a ray downward to check if we're grounded
        RaycastHit hit;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f; // Slightly above the collider

        if (Physics.Raycast(rayStart, Vector3.down, out hit, groundCheckDistance + 0.1f, groundLayer))
        {
            isGrounded = true;
            canJump = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Jump()
    {
        if (canJump)
        {
            // Reset Y velocity before applying jump force
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    void OnDrawGizmos()
    {
        // Visualize ground check ray
        Gizmos.color = Color.green;
        Vector3 rayStart = transform.position + Vector3.up * 0.1f;
        Gizmos.DrawLine(rayStart, rayStart + Vector3.down * (groundCheckDistance + 0.1f));
    }
}