using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float MovementSpeed = 2;
    private SpriteRenderer sprite;
    public Animator animator;
    private Rigidbody2D body;
    public Joystick joystick;

    // Start is called before the first frame update
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        var movement_h = joystick.Horizontal * MovementSpeed;
        animator.SetFloat("speed_h", movement_h);
        if (movement_h < 0) 
        {
            sprite.flipX = true;
        }
        else if (movement_h > 0)
        {
            sprite.flipX = false;
        }

        var movement_v = joystick.Vertical * MovementSpeed;
        animator.SetFloat("speed_v", movement_v);

        transform.position += new Vector3(movement_h,movement_v,0) * Time.deltaTime * MovementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("coin")) {
            Destroy(other.gameObject);
        }
    }
}
