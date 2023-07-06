using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Behivour : MonoBehaviour
{
    //Parametros publicos que cuyos valores podemos modificar desde Unity
    public float moveSpeed = 3f;
    public float limitOfYPos = 2.5f;
    public float limitOfYNeg = -2.5f;
    public float limitOfXPos = 3.0f;
    public float limitOfXNeg = -5.5f;
    //Con esto indicaremos que tipo de movimiento usaremos.
    public string typeOfMovement;
    //Control variables 
    private int control = 0;
    //Se especifica el enemigo y su animator
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(typeOfMovement == "Vertical")
        {
            verticalMove();
        }

        if(typeOfMovement == "Horizontal")
        {
            horizontalMove();                        
        }

    }

    void verticalMove()
    {
        float step =  moveSpeed * Time.deltaTime;
        //UP
        if (control == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYPos), step);
            //animator.SetFloat("Vertical", transform.position.y);
            animator.SetFloat("Vertical", 1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);
            

            if(transform.position.y == limitOfYPos)
            {
                control = 1;
            }
        }
        //DOWN
        else if(control == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(transform.position.x, limitOfYNeg), step);
            animator.SetFloat("Vertical", -1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if(transform.position.y == limitOfYNeg)
            {
                control = 0;
            }  
        }
    }

    void horizontalMove()
    {
        float step =  moveSpeed * Time.deltaTime;
        //RIGHT
        if (control == 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXPos, transform.position.y), step);
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if(transform.position.x == limitOfXPos)
            {
                control = 1;
            }
        }
        //LEFT
        else if(control == 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXNeg, transform.position.y), step);
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if(transform.position.x == limitOfXNeg)
            {
                control = 0;
            }  
        }
    }
}

