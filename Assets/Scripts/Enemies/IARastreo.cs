using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IARastreo : MonoBehaviour
{
    private bool isFacingRight = true;
    [SerializeField] private Transform player;
    [SerializeField] private float minDistance;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position)< minDistance){
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else{
        }
        bool isPlayerRight = transform.position.x < player.transform.position.x;
        Flip(isPlayerRight);
    }

    private void Flip(bool isPlayerRight)
    {
        if((isFacingRight && !isPlayerRight) || (!isFacingRight && isPlayerRight))
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
