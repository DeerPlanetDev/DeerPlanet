using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class playerDamage : MonoBehaviour
{
    public GameObject movePoint;
    public Animator self;
    public bool isHurt;
    public Player_Movement playerMovement;
    private RaycastHit2D hit_up, hit_down, hit_left, hit_right, lastHit_up, lastHit_down, lastHit_left, lastHit_right, pointHit_up, pointHit_down, pointHit_left, pointHit_right; 
    void Start()
    {
        playerMovement = GetComponent<Player_Movement>();
    }

    private void Update()
    {
        int layer_mask = LayerMask.GetMask("StopMovement");
        hit_up = Physics2D.Raycast(transform.position,Vector2.up,1,layer_mask);
        hit_down = Physics2D.Raycast(transform.position,Vector2.down,1,layer_mask);
        hit_left = Physics2D.Raycast(transform.position,Vector2.left,1,layer_mask);
        hit_right = Physics2D.Raycast(transform.position, Vector2.right,1,layer_mask);
        lastHit_up = Physics2D.Raycast(playerMovement.lastPosition, Vector2.right, 1, layer_mask);
        lastHit_down = Physics2D.Raycast(playerMovement.lastPosition, Vector2.down, 1, layer_mask);
        lastHit_left = Physics2D.Raycast(playerMovement.lastPosition, Vector2.left, 1, layer_mask);
        lastHit_right = Physics2D.Raycast(playerMovement.lastPosition, Vector2.right, 1, layer_mask);
        pointHit_up = Physics2D.Raycast(movePoint.transform.position, Vector2.up, 1, layer_mask);
        pointHit_down = Physics2D.Raycast(movePoint.transform.position, Vector2.down, 1, layer_mask);
        pointHit_left = Physics2D.Raycast(movePoint.transform.position, Vector2.left, 1, layer_mask);
        pointHit_right = Physics2D.Raycast(movePoint.transform.position, Vector2.right, 1, layer_mask);
    }
    
    public void startDamage(int dir)
    {
        isHurt = true;
        switch (dir)
        {
            case 0: //hurt right
                if(playerMovement.lookingRight== true)
                {
                    if (transform.position.x > movePoint.transform.position.x)
                    {
                        self.SetTrigger("damageRight");
                        if (lastHit_right.collider == null)
                        {
                            transform.position = playerMovement.lastPosition + Vector3.right * 1f;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else if (transform.position.x < movePoint.transform.position.x)
                    {
                        self.SetTrigger("damageRight");
                        if (lastHit_right.collider == null)
                        {
                            transform.position = movePoint.transform.position;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else
                    {
                        self.SetTrigger("damageRight");
                        if(hit_right.collider == null)
                            transform.position += Vector3.right * 1f;
                    }
                }
                else
                {
                    if (transform.position.x > movePoint.transform.position.x)
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageRight");
                        if (lastHit_right.collider == null)
                        {
                            transform.position = playerMovement.lastPosition + Vector3.right * 1f;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }   
                    }
                    else if (transform.position.x < movePoint.transform.position.x)
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageRight");
                        if (lastHit_right.collider == null)
                        {
                            transform.position = movePoint.transform.position;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageRight");
                        if(movePoint.transform.position.y != transform.position.y)
                        {
                            if(pointHit_right.collider == null)
                            {
                                transform.position = new Vector3(transform.position.x + 1f, movePoint.transform.position.y, transform.position.z);
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, movePoint.transform.position.y, transform.position.z);
                            }
                        }
                        else
                        {
                            if (hit_right.collider == null)
                                transform.position += Vector3.right * 1f;
                        }
                    }
                }
                break;
            case 1: // hurt left
                if (playerMovement.lookingLeft == true)
                {
                    if(transform.position.x < movePoint.transform.position.x)
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageLeft");
                        if (lastHit_left.collider == null)
                        {
                            transform.position = playerMovement.lastPosition + Vector3.left * 1f;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else if (transform.position.x > movePoint.transform.position.x)
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageLeft");
                        if(lastHit_left.collider == null)
                        {
                            transform.position = movePoint.transform.position;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else
                    {
                        playerMovement.sprite.flipX = false;
                        self.SetTrigger("damageLeft");
                        if(hit_left.collider == null)
                            transform.position += Vector3.left * 1f;
                    }
                }
                else
                {
                    if (transform.position.x < movePoint.transform.position.x)
                    {
                        self.SetTrigger("damageLeft");
                        if(lastHit_left.collider == null)
                        {
                            transform.position = playerMovement.lastPosition + Vector3.left * 1f;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                        
                    }
                    else if (transform.position.x > movePoint.transform.position.x)
                    {
                        self.SetTrigger("damageLeft");
                        if (lastHit_left.collider == null)
                        {
                            transform.position = movePoint.transform.position;
                        }
                        else
                        {
                            transform.position = playerMovement.lastPosition;
                        }
                    }
                    else
                    {
                        self.SetTrigger("damageLeft");
                        if (movePoint.transform.position.y != transform.position.y)
                        {
                            playerMovement.sprite.flipX = false;
                            if(pointHit_left.collider == null)
                            {
                                transform.position = new Vector3(transform.position.x - 1f, movePoint.transform.position.y, transform.position.z);
                            }
                            else
                            {
                                transform.position = new Vector3(transform.position.x, movePoint.transform.position.y, transform.position.z);
                            }
                        }
                        else
                        {
                            if(hit_left.collider == null)
                                transform.position += Vector3.left * 1f;
                        }
                    }
                }
                break;
            case 2: //hurt up
                if(transform.position.y > movePoint.transform.position.y)
                {
                    self.SetTrigger("damageUp");
                    if(lastHit_up.collider == null)
                    {
                        transform.position = playerMovement.lastPosition + Vector3.up * 1f;
                    }
                    else
                    {
                        transform.position = playerMovement.lastPosition;
                    }
                }
                else if(transform.position.y < movePoint.transform.position.y)
                {
                    self.SetTrigger("damageUp");
                    if(lastHit_up.collider == null)
                    {
                        transform.position = movePoint.transform.position;
                    }
                    else
                    {
                        transform.position = playerMovement.lastPosition;
                    }
                }
                else
                {
                    self.SetTrigger("damageUp");
                    if(movePoint.transform.position.x != transform.position.x)
                    {
                        if(pointHit_up.collider == null)
                        {
                            transform.position = new Vector3(movePoint.transform.position.x, transform.position.y + 1f, transform.position.z);
                        }
                        else
                        {
                            transform.position = new Vector3(movePoint.transform.position.x, transform.position.y, transform.position.z);
                        }
                    }
                    else
                    {
                        if(hit_up.collider == null)
                            transform.position += Vector3.up * 1f;
                    }
                }
                break;
            case 3: //hurt down
                if(transform.position.y < movePoint.transform.position.y)
                {
                    self.SetTrigger("damageDown");
                    if (lastHit_down.collider == null)
                    {
                        transform.position = playerMovement.lastPosition + Vector3.down * 1f;
                    }
                    else
                    {
                        transform.position = playerMovement.lastPosition;
                    }
                }
                else if (transform.position.y > movePoint.transform.position.y)
                {
                    self.SetTrigger("damageDown");
                    if(lastHit_down.collider == null)
                    {
                        transform.position = movePoint.transform.position;
                    }
                    else
                    {
                        transform.position = playerMovement.lastPosition;
                    }
                }
                else
                {
                    self.SetTrigger("damageDown");
                    if (movePoint.transform.position.x != transform.position.x)
                    {
                        if(pointHit_down.collider == null)
                        {
                            transform.position = new Vector3(movePoint.transform.position.x, transform.position.y - 1f, transform.position.z);
                        }
                        else
                        {
                            transform.position = new Vector3(movePoint.transform.position.x, transform.position.y, transform.position.z);
                        }
                    }
                    else
                    {
                        if(hit_down.collider == null)
                            transform.position += Vector3.down * 1f;
                    }
                }   
                break;
        }
    }

    public void damageFinished()
    {
        if(movePoint.transform.position != transform.position) 
            movePoint.transform.position = transform.position;
        isHurt = false;
    }
}
