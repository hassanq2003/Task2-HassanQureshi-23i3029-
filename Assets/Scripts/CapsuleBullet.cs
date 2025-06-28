using UnityEngine;

public class CapsuleBullet : MonoBehaviour
{
    private Transform target;
    public float speed = 10f;
    private Rigidbody rb;
    private rotateCannon rotatecannon;
    private bool hasFired = false;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rotatecannon = FindAnyObjectByType<rotateCannon>();
        rb.useGravity = false;
        rb.velocity = (target.position - transform.position).normalized * speed;

        // Start timeout coroutine
        StartCoroutine(DestroyAfterDelay(5f));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rotatecannon != null && !hasFired)
            {
                rotatecannon.canFireAgain = true;
                hasFired = true;
            }

            Destroy(gameObject);
        }
    }

    System.Collections.IEnumerator DestroyAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        if (!hasFired && rotatecannon != null)
        {
            rotatecannon.canFireAgain = true;
        }

        Destroy(gameObject);
    }
}
