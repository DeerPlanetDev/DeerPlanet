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
    //Agregamos un efecto de sonido para indicar al jugador el daño recibido
    [SerializeField] AudioClip damageSfx;
    //Una variable para indicar el daño que se realizara al jugador
    [SerializeField] int damage = -30;

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
            //MoveTowards se encarga de mover un punto directamente a otro punto, primer parametro indica la posicion actual
            //Vector2 -- Indica que la nueva posicion deseada en X, mientras que Y se queda estatica
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXPos, transform.position.y), step);

            //Especificamos la animacion a realizar y la velocidad a la que se va ejectuar
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            //Control nos permitira cambiar el sentido del movimiento
            if(transform.position.x == limitOfXPos)
            {
                control = 1;
            }
        }
        //LEFT
        else if(control == 1)
        {
            //Usamos localPosition para mover el objeto con relacio a su objeto padre, no con el origen del mundo
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(limitOfXNeg, transform.position.y), step);

            //Indicamos la nueva animacion que se va a realizar
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Speed", transform.position.sqrMagnitude);

            if(transform.position.x == limitOfXNeg)
            {
                control = 0;
            }  
        }
    }


    //Reducir la vida del jugador cuando entre en contacto con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        //Comparamaos si entraron en contacto
        if (other.gameObject.CompareTag("Player"))
        {
            //Hacemos que suene el sonido de daño
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx);
            //Modiicamos la salud del jugador
            PlayerHealth.instance.ModifyHP(damage);
        }
    }
}

