using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Behavior : MonoBehaviour
{
    //Parameters
    public float moveSpeed = 2f;
    public float limitOfYPos = 2.5f;
    public float limitOfYNeg = -2.5f;
    public float limitOfXPos = 3.0f;
    public float limitOfXNeg = -5.5f;
    public string typeOfMovement;
    //Control variables 
    private int control = -1;
    //Animator
    public Animator animator;
    public GameObject projectile;
    //player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (typeOfMovement == "Vertical")
        {
            StartCoroutine(verticalMove());
        }

        if (typeOfMovement == "Horizontal")
        {
            StartCoroutine(horizontalMove());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator verticalMove()
    {
        while (true)
        {
            float step =  moveSpeed * Time.deltaTime;
            float distanceFromWall = 0f;
            //UP
            if (control == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYPos), step);
                distanceFromWall = limitOfYPos - transform.position.y;
                //animator.SetFloat("Vertical", transform.position.y);
                animator.SetFloat("Vertical", 1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if(transform.position.y == limitOfYPos)
                {
                    control = -1;
                    //animator.SetFloat("Vertical", -1);
                    //animator.SetFloat("Speed", transform.position.sqrMagnitude);
                }
            }
            //DOWN
            else if(control == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYNeg), step);
                distanceFromWall = transform.position.y - limitOfYNeg;
                animator.SetFloat("Vertical", -1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if(transform.position.y == limitOfYNeg)
                {
                    control = 1;
                    //animator.SetFloat("Vertical", 1);
                    //animator.SetFloat("Speed", transform.position.sqrMagnitude);
                }
            }
            
            //revisa si puede atacar
            float distanceFromPlayer = (player.transform.position.y - transform.position.y) * control;
            float maxVision = 3;
            if(distanceFromWall < 3)
            {
                maxVision = distanceFromWall;
            }
            //Debug.Log(distanceFromPlayer);
            if((player.transform.position.x == transform.position.x) && (distanceFromPlayer < maxVision && distanceFromPlayer > 0))
            {
                animator.SetBool("Attacking", true);
                yield return new WaitForSeconds(2f); //animación
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<ProjectileBehavior>().direction = new Vector3(0, control, 0);
                yield return new WaitForSeconds(1.167f);
                animator.SetBool("Attacking", false);
            }
            yield return null;
        }
    }

    /*
    void verticalMove()
    {

    }
    */

    IEnumerator horizontalMove()
    {
        while(true)
        {
            //movimiento
            float step =  moveSpeed * Time.deltaTime;
            float distanceFromWall = 0f;
            //RIGHT
            if (control == 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXPos, transform.position.y), step);
                distanceFromWall = limitOfXPos - transform.position.x;
                animator.SetFloat("Horizontal", 1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if(transform.position.x == limitOfXPos)
                {
                    control = -1;
                    //animator.SetFloat("Horizontal", -1);
                    //animator.SetFloat("Speed", transform.position.sqrMagnitude);
                }
            }
            //LEFT
            else if(control == -1)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXNeg, transform.position.y), step);
                distanceFromWall = transform.position.x - limitOfXNeg; 
                animator.SetFloat("Horizontal", -1);
                animator.SetFloat("Speed", transform.position.sqrMagnitude);

                if(transform.position.x == limitOfXNeg)
                {
                    control = 1;
                    //animator.SetFloat("Horizontal", 1);
                    //animator.SetFloat("Speed", transform.position.sqrMagnitude);
                }  
            }

            //revisa si puede atacar
            float distanceFromPlayer = (player.transform.position.x - transform.position.x) * control;
            float maxVision = 3;
            if(distanceFromWall < 3)
            {
                maxVision = distanceFromWall;
            }
            //Debug.Log(distanceFromPlayer);
            if((player.transform.position.y == transform.position.y) && (distanceFromPlayer < maxVision && distanceFromPlayer > 0))
            {
                animator.SetBool("Attacking", true);
                yield return new WaitForSeconds(2f); //animación
                GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
                newProjectile.GetComponent<ProjectileBehavior>().direction = new Vector3(control, 0, 0);
                yield return new WaitForSeconds(1.167f);
                animator.SetBool("Attacking", false);
            }
            yield return null;
        }
    }

    /*
    void horizontalMove()
    {

    }
    */
}
