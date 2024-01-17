using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement_DiagTest : MonoBehaviour
{
    public float moveSpeed = 3.5f;
    public Transform movePoint;
    public LayerMask stopsMove;
    public SpriteRenderer sprite;
    public Animator animator;
    private Vector3 MovementOfDeer;
    private float memoryH;
    private float memoryV;

    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        //sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
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
        MovementOfDeer = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);
        transform.position = MovementOfDeer;

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
                    animator.SetFloat("Horizontal", Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")));
                    animator.SetFloat("Speed", MovementOfDeer.sqrMagnitude);
                    if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == 1)
                    {
                        memoryH = 1;
                        memoryV = 0;
                    }
                    if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == -1)
                    {
                        memoryH = -1;
                        memoryV = 0;
                    }
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
                    animator.SetFloat("Vertical", CrossPlatformInputManager.GetAxis("Vertical"));
                    animator.SetFloat("Speed", MovementOfDeer.sqrMagnitude);
                    if(Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) == 1)
                    {
                        memoryV = 1;
                        memoryH = 0;
                    }
                    if(CrossPlatformInputManager.GetAxis("Vertical") == -1)
                    {
                        memoryV = -1;
                        memoryH = 0;
                    }
                }
            }
            else
            {
                animator.SetFloat("Horizontal", 0f);
                animator.SetFloat("Vertical", 0f);
                animator.SetFloat("Speed", 0f);
                
                animator.SetFloat("VMemory", memoryV);
                animator.SetFloat("HMemory", memoryH);
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
