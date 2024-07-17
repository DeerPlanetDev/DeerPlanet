using System.Collections;
using UnityEngine;

public class IAPatrulla : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed;
    [SerializeField] private SignPlaceIA signPlacer; // Reference to the sign placer

    private int currentWaypoint = 0;
    private bool isMoving = false;

    void Update()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveToNextWaypoint());
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);
            yield return null;
        }

        // Reached waypoint
        if (signPlacer != null)
        {
            signPlacer.OnWaypointReached();
        }

        yield return new WaitForSeconds(waitTime);
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length; 
        isMoving = false; 
    }

    // Method to check if the enemy is currently at a waypoint
    public bool IsAtWaypoint()
    {
        return Vector2.Distance(transform.position, waypoints[currentWaypoint].position) <= 0.1f;
    }
}