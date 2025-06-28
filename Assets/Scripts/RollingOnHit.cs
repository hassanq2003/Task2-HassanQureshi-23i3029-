using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RollingOnHit : MonoBehaviour
{
    public float rollForce = 10f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            // Calculate a horizontal push direction away from the bullet
            Vector3 pushDir = (transform.position - collision.transform.position).normalized;
            pushDir.y = 0f; // Keep the force horizontal

            rb.AddForce(pushDir * rollForce, ForceMode.Impulse);
        }
    }
}
