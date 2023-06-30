using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MovementSpeed = 3;
    private SpriteRenderer sprite;
    public Animator animator;
    private Rigidbody2D body;
    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        // Horizontal Movement Animator
        var movement_h = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("speed_h", Mathf.Abs(movement_h));
        // Flip movement
        if (movement_h < 0) 
        {
            sprite.flipX = true;
        }
        else if (movement_h > 0)
        {
            sprite.flipX = false;
        }
        // Vertical Movement Animator
        var movement_v = Input.GetAxisRaw("Vertical");
        animator.SetFloat("speed_v", movement_v);
        // Moving
        transform.position += new Vector3(movement_h,movement_v,0) * Time.deltaTime * MovementSpeed;
    }
    // OnCollision Effects
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("plastico")) {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("bioplastico")) {
            Destroy(other.gameObject);
        }
    }
}
