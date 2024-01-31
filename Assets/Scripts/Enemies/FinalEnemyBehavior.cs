using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------------------------

Este es el script que esta usando el enemigo final
 
 ----------------------------------------------------------------*/

public class FinalEnemyBehavior : MonoBehaviour
{

    [SerializeField] float speed = 1.0f;
    [SerializeField] int detectionRange = 50;
    [SerializeField] LayerMask playerLayer; //Nos permitira detectar al jugador
    Vector2 playerDirection = new Vector2 (0, 0); //Guardara la ubicación del jugador

    //Consecuencias al tocar el enemigo
    [SerializeField] int damage = -50;
    [SerializeField] int notScore = -1;


    private void Start()
    {
        AlignToNearestTile();
    }


    //Alineamos al enemigo en la casilla en la que se encuentra
    void AlignToNearestTile()
    {
        //Redondeamos la ubicación acutal del objeto y ajustamos con la cuadricula
        float nearX = Mathf.FloorToInt(transform.position.x) + 0.5f;
        float nearY = Mathf.FloorToInt(transform.position.y) + 0.5f;
        Vector2 newPosition = new Vector2(nearX, nearY);
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime);
    }

    void FixedUpdate()
    {
        foreach (Vector2 dir in new Vector2[] { Vector2.down, Vector2.up, Vector2.left, Vector2.right }) //El arreglo de vectores que nos permitra mover nuestro objeto
        {
            if (Physics2D.Raycast(transform.position, dir, detectionRange, playerLayer).collider != null)
            {
                playerDirection = dir;
                break;
            }
            else
            {
                playerDirection = Vector2.zero;
            }
        }

        if (playerDirection.magnitude > 0 && GameObject.FindWithTag("Player"))
        {
            StartCoroutine(MoveAfterTime());
            
        }
        else
        {
            AlignToNearestTile();
        }
    }

    IEnumerator MoveAfterTime()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime * speed);
    }


    //Efectos de tocar el enemigo
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Entraron en contacto?
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerHealth.instance.ModifyHP(damage); //Dañar a jugador
            LevelManager.instance.IncreaseScore(notScore, 0); //Reducir el puntaje
        }
    }

}
