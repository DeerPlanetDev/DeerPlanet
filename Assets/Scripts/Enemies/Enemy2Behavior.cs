using System.Collections;
using UnityEngine;

public class Enemy2Behavior : Enemy
{
    // Public parameters for movement and attack
    public float moveSpeed = 2f;
    public float limitOfYPos = 2.5f;
    public float limitOfYNeg = -2.5f;
    public float limitOfXPos = 3.0f;
    public float limitOfXNeg = -5.5f;
    public string typeOfMovement; // "Vertical" or "Horizontal"

    // References
    public GameObject projectile;
    public GameObject player;

    // Private control variables
    private int control = -1;
    private float maxVision = 3f; // Maximum distance for detecting the player

    public override void Start()
    {
        base.Start();  // Call the base class Start() for initialization
        health = 100; // Set specific health for Enemy2 (or adjust in Inspector)
          // Set specific damage

        if (typeOfMovement == "Vertical")
        {
            StartCoroutine(verticalMove());
        }
        else if (typeOfMovement == "Horizontal")
        {
            StartCoroutine(horizontalMove());
        }
    }

    public override void Move()
    {
        // No need for separate verticalMove() or horizontalMove() here.
        // The coroutines handle the movement.
    }

    IEnumerator verticalMove()
    {
        while (true)
        {
            float step = moveSpeed * Time.deltaTime;
            float distanceFromWall = 0f;

            // Movement logic (UP or DOWN)
            if (control == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYPos), step);
                distanceFromWall = limitOfYPos - transform.position.y;
                animator.SetFloat("Vertical", 1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if (transform.position.y == limitOfYPos)
                {
                    control = -1;
                }
            }
            else if (control == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYNeg), step);
                distanceFromWall = transform.position.y - limitOfYNeg;
                animator.SetFloat("Vertical", -1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if (transform.position.y == limitOfYNeg)
                {
                    control = 1;
                }
            }

            // Check for attack opportunity
            float distanceFromPlayer = (player.transform.position.y - transform.position.y) * control;
            float currentMaxVision = (distanceFromWall < maxVision) ? distanceFromWall : maxVision;

            if ((player.transform.position.x == transform.position.x) && (distanceFromPlayer < currentMaxVision && distanceFromPlayer > 0))
            {
                animator.SetBool("Attacking", true);
                yield return new WaitForSeconds(2f); // Animation delay
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<ProjectileBehavior>().direction = new Vector3(0, control, 0);
                yield return new WaitForSeconds(1.167f); // Attack cooldown
                animator.SetBool("Attacking", false);
            }

            yield return null;
        }
    }

    IEnumerator horizontalMove()
{
    while (true)
    {
        // Movement logic (RIGHT or LEFT)
        float step = moveSpeed * Time.deltaTime;
        float distanceFromWall = 0f;

        if (control == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXPos, transform.position.y), step);
            distanceFromWall = limitOfXPos - transform.position.x;
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if (transform.position.x == limitOfXPos)
            {
                control = -1;
            }
        }
        else if (control == -1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXNeg, transform.position.y), step);
            distanceFromWall = transform.position.x - limitOfXNeg; 
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if (transform.position.x == limitOfXNeg)
            {
                control = 1;
            }
        }

        // Check for attack opportunity
        float distanceFromPlayer = (player.transform.position.x - transform.position.x) * control; 
        float currentMaxVision = (distanceFromWall < maxVision) ? distanceFromWall : maxVision;

        if ((player.transform.position.y == transform.position.y) && (distanceFromPlayer < currentMaxVision && distanceFromPlayer > 0))
        {
            animator.SetBool("Attacking", true);
            yield return new WaitForSeconds(2f); // Animation delay
            GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
            newProjectile.GetComponent<ProjectileBehavior>().direction = new Vector3(control, 0, 0); // Horizontal projectile
            yield return new WaitForSeconds(1.167f); // Attack cooldown
            animator.SetBool("Attacking", false);
        }
        yield return null;
    }
}

protected override void Die()
    {
        // Stop any active coroutines
        StopAllCoroutines();

        // Call the base class Die() to handle death animation and destruction
        base.Die();
    }

    // Damage player on trigger enter
    private void OnTriggerEnter2D(Collider2D other)
    {
          // Call the base class's DamagePlayer method
    }
}