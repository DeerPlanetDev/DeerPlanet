using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IADisparo : MonoBehaviour
{
  
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float fireRate = 1f;

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            ShootProjectile();
            nextFireTime = Time.time + 1f / fireRate; 
        }
    }

    void ShootProjectile()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Vector3 direction = (player.transform.position - spawnPoint.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

            ProjectileBehavior projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
            if (projectileBehavior != null)
            {
                projectileBehavior.SetDirection(direction); 
            }
        }
    }
}