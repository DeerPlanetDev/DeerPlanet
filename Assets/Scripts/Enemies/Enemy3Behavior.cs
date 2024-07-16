using System.Collections;
using UnityEngine;

public class Enemy3Behavior : Enemy
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private int detectionRange = 100;
    [SerializeField] private LayerMask playerLayer;

    private Animator animator;
    private Vector2 playerDirection = Vector2.zero;
    private bool isMoving = false;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        AlignToNearestTile();

        // Initialize health and damage (you can adjust these values)
        health = 80; // Example value
    }

    // Align to nearest tile
    private void AlignToNearestTile()
    {
        float nearestMultipleX = Mathf.FloorToInt(transform.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(transform.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, transform.position.z);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    // Override the Move method from the base Enemy class
    public override void Move()
    {
        if (!isMoving)
        {
            // Detect player within range
            foreach (Vector2 dir in new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right })
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, detectionRange, playerLayer);
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    playerDirection = dir;
                    isMoving = true; // Start moving after detection
                    StartCoroutine(MoveAfterAnim());
                    break;
                }
            }
        }

        animator.SetInteger("Vertical", (int)playerDirection.y);
        animator.SetInteger("Horizontal", (int)playerDirection.x);
    }

    // Coroutine for movement animation
    private IEnumerator MoveAfterAnim()
    {
        yield return new WaitForSeconds(0.5f); // Wait for animation

        while (isMoving) // Continue moving until stopped
        {
            transform.position = Vector2.MoveTowards(transform.position, transform.position + (Vector3)playerDirection, Time.deltaTime * speed);
            yield return null;
        }

        // Reset playerDirection and isMoving after stopping
        playerDirection = Vector2.zero;
        isMoving = false;
    }

    // Trigger to damage player
    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.CompareTag("Wall"))
        {
            isMoving = false; // Stop when hitting a wall
        }
    }

    // Override Die method
    protected override void Die()
    {
        // Stop movement when the enemy dies
        isMoving = false;
        StopAllCoroutines();

        // Call the base class Die() to handle death animation and destruction
        base.Die();
    }
}