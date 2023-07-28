using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------------------------------------------

Este es el script que se esta usando con el enemigo 3------**

------------------------------------------------------------------------*/

public class Enemy3Behavior : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] int detectionRange = 100;
    [SerializeField] LayerMask playerLayer;


    Animator animator;
    bool detectedPlayer = false;
    Vector2 playerDirection = new Vector2(0, 0);

    //Para el daño
    //[SerializeField] AudioClip damageSfx; //Sonido ------------------------ AGREGAR SONIDO AL UNIR RAMA DE ENEMIGOS 1 Y 2
    [SerializeField] int damage = -30; //Daño realizado
    [SerializeField] int notScore = -1; //Puntaje a restar

    private void Start()
    {
        animator = GetComponent<Animator>();

        AlignToNearestTile();
    }


    //Alineamos con la posicion de la cuadricula
    void AlignToNearestTile()
    {
        float nearestMultipleX = Mathf.FloorToInt(transform.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(transform.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, transform.position.z);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
    }


    void FixedUpdate()
    {

        foreach (Vector2 dir in new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right })
        {
            if (Physics2D.Raycast(transform.position, dir, detectionRange, playerLayer).collider != null)
            {
                playerDirection = dir;
                break;
            }
            else
                playerDirection = Vector2.zero;
        }


        if (playerDirection.magnitude > 0 && GameObject.FindWithTag("Player"))
            StartCoroutine(MoveAfterAnim());
        else
            AlignToNearestTile();

        animator.SetInteger("Vertical", (int)playerDirection.y);
        animator.SetInteger("Horizontal", (int)playerDirection.x);
    }


    //El enemigo se mueve despues de unos segundos --- anim de salto
    IEnumerator MoveAfterAnim()
    {
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime * speed);
    }


    //Para que no atraviese las paredes
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Colliders"))
    //    {
    //        playerDirection = Vector2.zero;
    //    }
    //}

    //Reducir la vida del jugador y su puntaje cuando entre en contacto con el enemigo
    void OnTriggerEnter2D(Collider2D other)
    {
        //Comparamaos si entraron en contacto
        if (other.gameObject.CompareTag("Player"))
        {
            //Sonido de daño
            //other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx); ------------------ QUITAR COMENTARIO AL UNIR RAMA
            //Modiicamos la salud del jugador
            PlayerHealth.instance.ModifyHP(damage);
            //Modificamos el puntaje del jugador
            LevelManager.instance.IncreaseScore(notScore, 0); //Solo modificaremos el puntaje (primer parametro)
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            playerDirection = Vector2.zero;
        }
    }

}
