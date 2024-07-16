using UnityEngine;

public class Enemy1Behavior : Enemy
{
    // Public parameters for movement control
    public float moveSpeed = 3f;
    public float limitOfYPos = 2.5f;
    public float limitOfYNeg = -2.5f;
    public float limitOfXPos = 3.0f;
    public float limitOfXNeg = -5.5f;
    public string typeOfMovement;  // "Vertical" or "Horizontal"

    // Private control variable
    private int control = 0; 

    public override void Start()
    {
        base.Start();  
        health = 100; // Set specific health for Enemy1 (or adjust in Inspector)
    }

    // Implement the abstract Move method from the base Enemy class
    public override void Move()
    {
        // Determine movement based on typeOfMovement
        if (typeOfMovement == "Vertical")
        {
            verticalMove();
        }
        else if (typeOfMovement == "Horizontal")
        {
            horizontalMove();
        }
    }

    //Vertical Movement Logic
    private void verticalMove()
    {
        float step = moveSpeed * Time.deltaTime;

        // UP
        if (control == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYPos), step);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Speed", moveSpeed); // Use moveSpeed for animation speed

            if (transform.position.y >= limitOfYPos)
            {
                control = 1;
            }
        }
        // DOWN
        else if (control == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYNeg), step);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Speed", moveSpeed); // Use moveSpeed for animation speed

            if (transform.position.y <= limitOfYNeg)
            {
                control = 0;
            }
        }
    }

    //Horizontal Movement Logic
    private void horizontalMove()
    {
        float step = moveSpeed * Time.deltaTime;

        // RIGHT
        if (control == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXPos, transform.position.y), step);
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", moveSpeed);

            if (transform.position.x >= limitOfXPos)
            {
                control = 1;
            }
        }
        // LEFT
        else if (control == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXNeg, transform.position.y), step);
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", moveSpeed);

            if (transform.position.x <= limitOfXNeg)
            {
                control = 0;
            }
        }
    }

    // Damage player on trigger enter (assuming you have a PlayerHealth script)
    private void OnTriggerEnter2D(Collider2D other)
    {
         // Call the base class method to damage the player
    }
}