using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    //Viene del script de Enemy2Behaviour, nos indica la direccion del proyectil
    public Vector3 direction;
    public float movementSpeed = 2.5f;
    public Animator animator;
    //Agregamos un efecto de sonido para indicar al jugador el daño recibido
    [SerializeField] AudioClip damageSfx;
    //Una variable para indicar el daño que se realizara al jugador
    [SerializeField] int damage = -15;

    void Start()
    {
        StartCoroutine(moveProjectile());
    }

    IEnumerator moveProjectile()
    {
        while (true)
        {
            while (direction == null)
            {
                yield return null;
            }
            //Se mueve el proyectil en la direccion especificada por el enemigo
            transform.position += direction * movementSpeed * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator destroyProjectile(Collider2D other)
    {
        //Comparamos si el proyectil ha colisionado con el jugador
        if (other.CompareTag("Player"))
        {
            //VidaJugador.instance.TakeDamage(20); -------------- NO SE ESTA USANDO ESTE SRIPT
            //Indicamos el daño que recibiar el jugador
            PlayerHealth.instance.ModifyHP(damage);
            //Hacemos que suene el sonido de daño
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx);

            //Esto es un test para ver que impacta
            Debug.Log("Impacte al jugador");

            //Se ejecuta la siguiente animacion
            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject); //animar destrucción
        }
        //El proyectil se destruye si choca con el muro
        else if (other.CompareTag("Wall"))
        {

            //Esto es un test para ver que impacta
            Debug.Log("Impacte una pared");

            //Hacemos que suene el sonido de daño
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx); //No eliminar audioSource


            //Se ejecuta la siguiente animacion
            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject); //animar destrucción
        }
    }
    //Accion a tomar cuando el proyectil colisione
    void OnTriggerEnter2D(Collider2D other)
    {

        StartCoroutine(destroyProjectile(other));
    }



    void Update()
    {

    }
}