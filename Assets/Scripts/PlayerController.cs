using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;

    [Header("Rotation Settings")]
    public float rotationSpeed = 120f; // Increased from 30 for more responsive turning
    public float detectionRadius = 15f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float moveInput = -1 * Input.GetAxis("Vertical");     // Up/Down arrow keys
        float rotateInput = Input.GetAxis("Horizontal");      // Left/Right arrow keys

        // Movement
        Vector3 moveDirection = transform.forward * moveInput * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + moveDirection);

        // Rotation
        float rotation = rotateInput * rotationSpeed * Time.deltaTime;

        // Optional: Invert rotation when moving forward (for tank-style controls)
        if (moveInput > 0)
        {
            rotation = -rotation;
        }

        // Apply rotation
        Quaternion turnOffset = Quaternion.Euler(0f, rotation, 0f);
        rb.MoveRotation(rb.rotation * turnOffset);
    }

    // Optional: Visualize detection radius in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
