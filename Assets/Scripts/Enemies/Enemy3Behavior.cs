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
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindWithTag("Player").transform.position, Time.deltaTime * speed);
        else
            AlignToNearestTile();

        animator.SetInteger("Vertical", (int)playerDirection.y);
        animator.SetInteger("Horizontal", (int)playerDirection.x);
    }
}
