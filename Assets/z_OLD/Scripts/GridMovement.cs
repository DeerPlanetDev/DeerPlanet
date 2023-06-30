using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GridMovement : MonoBehaviour
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
            if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == 1f)
            {
                // If not colliding
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, 0f), .5f, stopsMove))
                {
                    movePoint.position += new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * 1f, 0f, 0f);
                    // Debug.Log(movePoint.position);
                    animator.SetFloat("speed_h", Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")));
                }
            }
            // Moving up and down
            else if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) == 1f)
            {
                // If not colliding
                if(!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, CrossPlatformInputManager.GetAxis("Vertical"), 0f), .5f, stopsMove))
                {
                    movePoint.position += new Vector3(0f, CrossPlatformInputManager.GetAxis("Vertical") * 1f, 0f);
                    // Debug.Log(movePoint.position);
                    animator.SetFloat("speed_v", CrossPlatformInputManager.GetAxis("Vertical"));
                }
            }
            else
            {
                animator.SetFloat("speed_h", 0f);
                animator.SetFloat("speed_v", 0f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.tag == "Enemy1")
        {
            Score.instance.RemovePoint(5);
            VidaJugador.instance.TakeDamage(20);
        }
    }
        //StartCoroutine(MovementSpeedReduction());
   

    IEnumerator MovementSpeedReduction()
    {
        float speedReduction = 2.5f;
        moveSpeed = moveSpeed - speedReduction;
        yield return new WaitForSeconds(2);
        moveSpeed = moveSpeed + speedReduction;
    }   
}
