using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------------------------

Este es el script que esta usando el enemigo final
 
 ----------------------------------------------------------------*/

public class FinalEnemyBehavior : MonoBehaviour
{

    [Header("Values")]
    [SerializeField] float speed = 1.0f;
    [SerializeField] int detectionRange = 10;
    [SerializeField] float obstacleDuration = 2f;
    //Consecuencias al tocar el enemigo
    [SerializeField] int damage = -50;
    [SerializeField] int notScore = -1;
    bool obstacleActive = false;

    [Header("References")]
    [SerializeField] LayerMask playerLayer; //Nos permitira detectar al jugador
    [SerializeField] LayerMask colliderLayer; //Para cortar el paso
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] AudioClip damageSfx;


    Vector2 playerDirection = new Vector2 (0, 0); //Guardara la ubicación del jugador

    

    //---------------------------------------------------------------------------------
    private void Start()
    {
        AlignToNearestTile();
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

        //Movimiento del enemigo
        if (playerDirection.magnitude > 0 && GameObject.FindWithTag("Player"))
        {
            StartCoroutine(MoveAfterTime());
            
        }
        else
        {
            AlignToNearestTile();
        }

        //Obstaculo
        if(playerDirection.magnitude != 0)
        {
            PlaceObstacle();
        }
    }


    //-----------------------------------------------------------   FUNCIONES PARA LOS OBSTACULOS  -----------------------------------

    void PlaceObstacle()
    {
        if (!obstacleActive)
        {
            // Se calcula la posición del obstáculo basándose en la posición actual del jugador y la dirección del jugador
            Vector3 obstaclePos = GameObject.FindWithTag("Player").transform.position + new Vector3(playerDirection.x, playerDirection.y, 0f);
            // Se verifica si hay otros objetos en las capas especificadas dentro de un radio de 0.1f centrado en ObstaclePos
            if (Physics2D.OverlapCircle(obstaclePos, 0.1f, playerLayer + colliderLayer) == null)
            {
                obstacleActive = true;
                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePos, Quaternion.identity); // Se crea la instancia del prefab
                AlignPointToGrid(obstacle.transform); // Alineamos el obstáculo con la cuadrícula
                                                      // Destruimos el objeto después del tiempo especificado
                Destroy(obstacle, obstacleDuration);
                Invoke("ResetSign", obstacleDuration);
            }
        }
    }

    void AlignPointToGrid(Transform point)
    {
        float nearestMultipleX = Mathf.FloorToInt(point.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(point.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, point.position.z);
        point.position = newPosition;
    }

    void ResetSign()
    {
        obstacleActive = false;
    }


    //---------------------------------------------------------     FUNCIONES PARA EL ENEMIGO   -------------------------------------------------------

    IEnumerator MoveAfterTime()
    {
        yield return new WaitForSeconds(0.3f);
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime * speed);
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


    //------------------------------------------    Efectos de tocar el enemigo     -------------------------------------------
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Entraron en contacto?
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx);
            PlayerHealth.instance.ModifyHP(damage); //Dañar a jugador
            LevelManager.instance.IncreaseScore(notScore, 0); //Reducir el puntaje
        }
    }

}
