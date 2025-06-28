using UnityEngine;


public class ZombiePatrol : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public float moveSpeed = 2f;

    private Vector3 targetPosition;
    private Animator animator;
    private bool isHurt = false;
    private bool movingToPosition2 = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        targetPosition = position2.position;
    }

    void Update()
    {
        if (isHurt) return;

        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // Flip direction and target
            movingToPosition2 = !movingToPosition2;
            targetPosition = movingToPosition2 ? position2.position : position1.position;

            // Rotate 180 degrees on Y-axis
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isHurt) return;

        if (collision.gameObject.CompareTag("bullet"))
        {
            isHurt = true;
            animator.SetBool("isHurt", true);
            Destroy(collision.gameObject); // optional: destroy bullet on hit
            StartCoroutine(WaitForHurtAnimation());
        }
    }

    System.Collections.IEnumerator WaitForHurtAnimation()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // Wait for current frame to update
        yield return null;

        // Wait for the hurt animation to finish
        while (!stateInfo.IsName("root|Zombie_dead") || stateInfo.normalizedTime < 1f)
        {
            stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject); // fallback if no parent
        }
    }
}
