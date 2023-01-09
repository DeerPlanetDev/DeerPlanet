using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayer : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public Transform movePoint;
    public LayerMask stopsMove;
    public SpriteRenderer sprite;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 diff = transform.position - movePoint.position;
        // Checking direction the deer will take
        if (diff.x > 0)
        {
            sprite.flipX = true;
        }
        else if (diff.x < 0)
        {
            sprite.flipX = false;
        }
        // Moving the deer
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

        // Movement limit of the character
        if (Vector3.Distance(transform.position, movePoint.position) <= 0.5f)
        {
            // Moving sideways
            if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                // If not colliding
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), .5f, stopsMove))
                {
                    movePoint.position += new Vector3(Input.GetAxisRaw("Horizontal") * 1f, 0f, 0f);
                    // Debug.Log(movePoint.position);
                    animator.SetFloat("speed_h", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
                }
            }
            // Moving up and down
            else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                // If not colliding
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), .5f, stopsMove))
                {
                    movePoint.position += new Vector3(0f, Input.GetAxisRaw("Vertical") * 1f, 0f);
                    // Debug.Log(movePoint.position);
                    animator.SetFloat("speed_v", Input.GetAxisRaw("Vertical"));
                }
            }
            else
            {
                animator.SetFloat("speed_h", 0f);
                animator.SetFloat("speed_v", 0f);
            }
        }
    }
}