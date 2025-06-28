using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateCannon : MonoBehaviour
{
    PlayerController playerController;
    public GameObject capsulePrefab; // Assign in Inspector
    public Transform spawnPoint;     // Where the capsule spawns
    public Transform target;         // Target the capsule should move to

    public bool canFireAgain;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        canFireAgain = true;
    }

    // Update is called once per frame
    void Update()
    {
        target = null;
        RotateTowardsNearestEnemy();
        if (Input.GetMouseButtonDown(0) && canFireAgain) // Press Left Mouse Button to fireSpace to fire
        {
            SpawnCapsule();
        }
    }
    void SpawnCapsule()
{
        if (capsulePrefab != null && spawnPoint != null && target != null)
        {
            GameObject capsule = Instantiate(capsulePrefab, spawnPoint.position, spawnPoint.rotation);
            CapsuleBullet bulletScript = capsule.GetComponent<CapsuleBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(target);
            }
            canFireAgain = false; // Set the cooldown timer
    }
}
    void RotateTowardsNearestEnemy()
    {
        Collider[] nearbyObjects = Physics.OverlapSphere(transform.position, playerController.detectionRadius);
        Transform nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (Collider obj in nearbyObjects)
        {
            if (obj.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(transform.position, obj.transform.position);
                if (distance < shortestDistance)
                {
                    shortestDistance = distance;
                    nearestEnemy = obj.transform;
                }
            }
        }

        if (nearestEnemy != null)
        {   
            target=nearestEnemy.gameObject.transform;
            // Rotate towards the nearest enemy
            Vector3 direction = -1*(nearestEnemy.position - transform.position).normalized;
            direction.y = 0f;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerController.rotationSpeed * Time.deltaTime);
            }
        }
    }
}
