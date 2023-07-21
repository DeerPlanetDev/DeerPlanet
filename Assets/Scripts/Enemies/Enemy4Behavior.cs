using System.Collections;
using System.Collections.Generic;
using UnityEditor.AppleTV;
using UnityEngine;

public class Enemy4Behavior : MonoBehaviour
{
    //Referencias a nuestros puntos
    public GameObject pointA;
    public GameObject pointB;
    public GameObject pointC;
    public GameObject pointD;

    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed = 3.0f;

    //Para el daño
    //[SerializeField] AudioClip damageSfx; //Sonido ------------------------ AGREGAR SONIDO AL UNIR RAMA DE ENEMIGOS 1 Y 2
    [SerializeField] int damage = -30; //Daño realizado
    [SerializeField] int notScore = -1; //Puntaje a restar


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform; //Usamos este punto para que empieze a moverse
    }

    // Update is called once per frame
    void Update()
    {
        //Direccion del movimiento entre el objeto y currentpoint
        Vector2 point = currentPoint.position - transform.position;
        
        //Movimiento
        if(currentPoint == pointB.transform)
        {
            //Derecha
            rb.velocity = new Vector2(speed, 0);
        }
        else if(currentPoint == pointC.transform)
        {
            //Abajo
            rb.velocity = new Vector2(0,-speed);
        }
        else if(currentPoint == pointD.transform)
        {
            //Izquierda
            rb.velocity = new Vector2(-speed, 0);
        }
        else if(currentPoint == pointA.transform)
        {
            //Arriba
            rb.velocity = new Vector2(0, speed);
        }


        //Al llegar a un punto vamos al siguiente
        if(Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == pointB.transform)
        {
            //Cambiamos el punto
            currentPoint = pointC.transform;
        }
        
        //Repetimos este proceso con todos los puntos

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == pointC.transform)
        {
            currentPoint = pointD.transform;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == pointD.transform)
        {
            currentPoint = pointA.transform;
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.1f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }

    //Dibujos para identificar la ruta
    private void OnDrawGizmos()
    {
        //Puntos
        Gizmos.DrawWireSphere(pointA.transform.position, 0.15f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.15f);
        Gizmos.DrawWireSphere(pointC.transform.position, 0.15f);
        Gizmos.DrawWireSphere(pointD.transform.position, 0.15f);

        //Trayectoria
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
        Gizmos.DrawLine(pointB.transform.position, pointC.transform.position);
        Gizmos.DrawLine(pointC.transform.position, pointD.transform.position);
        Gizmos.DrawLine(pointD.transform.position, pointA.transform.position);
    }


    //Reducir la vida del jugador y su puntaje cuando entre en contacto con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        //Comparamaos si entraron en contacto
        if (other.gameObject.CompareTag("Player"))
        {
            //Hacemos que suene el sonido de daño
            //other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx); ----------- QUITAR COMENTARIO AL UNIR RAMA
            //Modiicamos la salud del jugador
            PlayerHealth.instance.ModifyHP(damage);
            //Modificamos el puntaje del jugador
            LevelManager.instance.IncreaseScore(notScore, 0); //Solo modificaremos el puntaje (primer parametro)
        }
    }
}
