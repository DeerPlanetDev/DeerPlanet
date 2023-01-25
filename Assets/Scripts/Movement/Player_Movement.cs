using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player_Movement : MonoBehaviour
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

    public playerDamage playerDamage;
    public bool lookingUp = false, lookingDown = false, lookingRight = false, lookingLeft = false;
    public Vector3 lastPosition;
    public CircleCollider2D circleCollider;
    private float nextDamage = 0f;
    [SerializeField] float damageCD =  4.0f;

    // Start is called before the first frame update
    void Start()
    {
        movePoint.parent = null;
        //sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        playerDamage = GetComponent<playerDamage>();
        lookingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerDamage.isHurt == false)
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
                if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == 1f)
                {
                    // If not colliding
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), 0f, 0f), .5f, stopsMove))
                    {
                        lastPosition= movePoint.position;
                        Debug.Log("last position: " + lastPosition);
                        movePoint.position += new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * 1f, 0f, 0f);
                        // Debug.Log(movePoint.position);
                        animator.SetFloat("Horizontal", Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")));
                        animator.SetFloat("Speed", MovementOfDeer.sqrMagnitude);
                        if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == 1)
                        {
                            memoryH = 1;
                            memoryV = 0;
                        }
                        if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")) == -1)
                        {
                            memoryH = -1;
                            memoryV = 0;
                        }
                    }
                }
                // Moving up and down
                else if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) == 1f)
                {
                    // If not colliding
                    if (!Physics2D.OverlapCircle(movePoint.position + new Vector3(0f, CrossPlatformInputManager.GetAxis("Vertical"), 0f), .5f, stopsMove))
                    {
                        lastPosition = movePoint.position;
                        Debug.Log("last position: " + lastPosition);
                        movePoint.position += new Vector3(0f, CrossPlatformInputManager.GetAxis("Vertical") * 1f, 0f);
                        // Debug.Log(movePoint.position);
                        animator.SetFloat("Vertical", CrossPlatformInputManager.GetAxis("Vertical"));
                        animator.SetFloat("Speed", MovementOfDeer.sqrMagnitude);
                        if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical")) == 1)
                        {
                            memoryV = 1;
                            memoryH = 0;
                        }
                        if (CrossPlatformInputManager.GetAxis("Vertical") == -1)
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
        if(movePoint.transform.position.x > transform.position.x)
        {
            lookingRight= true;
            lookingLeft= false;
            lookingDown= false;
            lookingUp= false;
        }else if(movePoint.transform.position.x < transform.position.x)
        {
            lookingRight = false;
            lookingLeft = true;
            lookingDown = false;
            lookingUp = false;
        }
        else if(movePoint.transform.position.y > transform.position.y)
        {
            lookingRight = false;
            lookingLeft = false;
            lookingDown = false;
            lookingUp = true;
        }
        else if(movePoint.transform.position.y < transform.position.y)
        {
            lookingRight = false;
            lookingLeft = false;
            lookingDown = true;
            lookingUp = false;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        Vector2 contactPoint = c.ClosestPoint(transform.position);
        float angleHitted = GetCollisionAngle(c.transform,circleCollider,contactPoint);
        if(playerDamage.isHurt == false /*&& Time.time >= nextDamage*/)                    //Damage Cooldown needs fix
        {
            if (c.tag == "Enemy1")
            {
                if (angleHitted >= 225 && angleHitted <= 315) //hurt right
                {
                    Debug.Log("TESTING RIGHT");
                    playerDamage.startDamage(0);
                }
                else if (angleHitted >= 45 && angleHitted <= 135) //hurt left 
                {
                    Debug.Log("TESTING LEFT");
                    playerDamage.startDamage(1);
                }
                else if (angleHitted >= 315 || angleHitted <= 45) //hurt up
                {
                    Debug.Log("TESTING UP");
                    playerDamage.startDamage(2);
                }
                else if (angleHitted >= 135 && angleHitted <= 225) //hurt down
                {
                    Debug.Log("TESTING DOWN");
                    playerDamage.startDamage(3);
                }
                Score.instance.RemovePoint(5);
                VidaJugador.instance.TakeDamage(2);
            }
            //nextDamage = Time.time + damageCD; 
        }
    }

    public float GetCollisionAngle(Transform hitobjectTransform, CircleCollider2D collider, Vector2 contactPoint)
    {
        Vector2 collidertWorldPosition = new Vector2(hitobjectTransform.position.x, hitobjectTransform.position.y);
        Vector3 pointB = contactPoint - collidertWorldPosition;

        float theta = Mathf.Atan2(pointB.x, pointB.y);
        float angle = (360 - ((theta * 180) / Mathf.PI)) % 360;
        return angle;
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
