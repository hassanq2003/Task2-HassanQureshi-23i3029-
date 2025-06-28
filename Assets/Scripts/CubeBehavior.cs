using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeBehavior : MonoBehaviour
{
    public float throwForce = 19f;
    public float upwardForce = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Cube collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("bullet"))
        {
            Vector3 hitDirection = (transform.position - collision.transform.position).normalized;
            Vector3 force = hitDirection * throwForce + Vector3.up * upwardForce;

            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
