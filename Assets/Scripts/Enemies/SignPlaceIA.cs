using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPlaceIA : MonoBehaviour
{
    [SerializeField] private GameObject signPrefab; 
    [SerializeField] private Transform signSpawnPoint;
    [SerializeField] private float minLifetime = 5f;
    [SerializeField] private float maxLifetime = 10f;
    [SerializeField] private float placementCooldown = 15f;

    [SerializeField] private IAPatrulla patrolScript;

    private float nextPlacementTime = 0f;
    private bool hasPlacedSignAtCurrentWaypoint = false; 

    void Start()
    {
        if (patrolScript == null)
        {
            patrolScript = GetComponent<IAPatrulla>(); 
            if (patrolScript == null)
            {
                Debug.LogError("IAPatrulla script not found on this GameObject!");
            }
        }
    }

    void Update()
    {
        if (patrolScript != null && !hasPlacedSignAtCurrentWaypoint && patrolScript.IsAtWaypoint())
        {
            if (Time.time > nextPlacementTime)
            {
                PlaceSign();
                nextPlacementTime = Time.time + placementCooldown;
                hasPlacedSignAtCurrentWaypoint = true; 
            }
        }
    }

    public void OnWaypointReached()
    {
        hasPlacedSignAtCurrentWaypoint = false;
    }

    void PlaceSign()
    {
        GameObject sign = Instantiate(signPrefab, signSpawnPoint.position, signSpawnPoint.rotation); 

        // Correctly pass the 'sign' reference to the coroutine
        float lifetime = Random.Range(minLifetime, maxLifetime);
        StartCoroutine(DestroySignAfterTime(sign, lifetime));  
    }

    IEnumerator DestroySignAfterTime(GameObject signToDestroy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(signToDestroy); // Use the passed reference to destroy the sign
    }
}