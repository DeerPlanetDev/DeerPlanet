using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*----------------------------------------------------------

Este script solo se encarga de perseguir al jugador, no sigue la "cuadricula"

--------------------------------------------------------------*/

public class EnemyFollow : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float stoppingDistance = 3f;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        
    }
}
