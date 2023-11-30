using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Behavior : MonoBehaviour
{
    // Start is called before the first frame update
    private float step_size = 0.5f;
    public float vel = 1.0f;
    public GameObject player, movePoint;
    private RaycastHit2D hit_up, hit_down, hit_left, hit_right;
    public Animator self;
    private Vector3 lastKnownPos;
    private bool moving = false;

    void Start()
    {
        Debug.Log(step_size);
    }

    // Update is called once per frame
    void Update()
    {
        // Rayo hacia arriba
        hit_up = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.7f), Vector2.up);
        // Rayo hacia abajo
        hit_down = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.7f), Vector2.down);
        // Rayo hacia izquierda
        hit_left = Physics2D.Raycast(new Vector2(transform.position.x - 0.8f, transform.position.y), Vector2.left);
        // Rayo hacia derecha
        hit_right = Physics2D.Raycast(new Vector2(transform.position.x + 0.7f, transform.position.y), Vector2.right);

        // if ((transform.position - player.transform.position).magnitude > 0.1) {
        if (moving)
        {
            Debug.Log("Moviendo");
            moveP();
        }
        else if (hit_up.collider != null && hit_up.collider.CompareTag("Player"))
        {
            // Play animation and move
            Debug.Log("Arriba");
            lastKnownPos = player.transform.position;
            move(0);
        }
        else if (hit_down.collider != null && hit_down.collider.CompareTag("Player"))
        {
            // Play animation and move
            Debug.Log("Abajo");
            lastKnownPos = player.transform.position;
            move(1);
        }
        else if (hit_left.collider != null && hit_left.collider.CompareTag("Player"))
        {
            // Play animation and move
            Debug.Log("Izquierda");
            lastKnownPos = player.transform.position;
            move(3);
        }
        else if (hit_right.collider != null && hit_right.collider.CompareTag("Player"))
        {
            // Play animation and move
            Debug.Log("Derecha");
            lastKnownPos = player.transform.position;
            move(2);
        }
        else
        {
            self.SetBool("Moving", false);
            self.SetInteger("Vertical", 0);
            self.SetInteger("Horizontal", 0);
        }
        // }
    }

    private void move(int dir)
    {
        self.SetBool("Moving", true);
        moving = true;
        switch (dir)
        {
            case 0:
                if (!Physics2D.OverlapCircle(transform.position + new Vector3(0, 1f), 0.5f, 8))
                {
                    movePoint.transform.position += new Vector3(0, 1f);
                    self.SetInteger("Vertical", 1);
                    self.SetInteger("Horizontal", 0);
                }
                break;

            case 1:
                if (!Physics2D.OverlapCircle(transform.position - new Vector3(0, 1f), 0.5f, 8))
                {
                    movePoint.transform.position -= new Vector3(0, 1f);
                    self.SetInteger("Vertical", -1);
                    self.SetInteger("Horizontal", 0);
                }
                break;

            case 2:
                if (!Physics2D.OverlapCircle(transform.position + new Vector3(1f, 0), 0.5f, 8))
                {
                    movePoint.transform.position += new Vector3(1f, 0);
                    self.SetInteger("Horizontal", 1);
                    self.SetInteger("Vertical", 0);
                }
                break;

            case 3:
                if (!Physics2D.OverlapCircle(transform.position - new Vector3(1f, 0), 0.5f, 8))
                {
                    movePoint.transform.position -= new Vector3(1f, 0);
                    self.SetInteger("Horizontal", -1);
                    self.SetInteger("Vertical", 0);
                }
                break;
        }
    }

    private void moveP()
    {
        if ((transform.position - movePoint.transform.position).magnitude > 0.1)
        {
            Vector3 dis = movePoint.transform.position - transform.position;
            transform.position += dis * Time.deltaTime * vel;
        }
        else
        {
            moving = false;
        }
    }

    // private void move(int dir)
    // {
    //     self.SetBool("Moving", true);
    //     switch(dir) {
    //         case 0:
    //         if(!Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0, 0.5f), 0.5f, 8))
    //         {
    //             transform.position += new Vector3(0, vel * Time.deltaTime);
    //             self.SetInteger("Vertical", 1);
    //             self.SetInteger("Horizontal", 0);
    //         }
    //         break;

    //         case 1:
    //         if(!Physics2D.OverlapCircle(movePoint.transform.position - new Vector3(0, 0.5f), 0.5f, 8))
    //         {
    //             transform.position -= new Vector3(0, vel * Time.deltaTime);
    //             self.SetInteger("Vertical", -1);
    //             self.SetInteger("Horizontal", 0);
    //         }
    //         break;

    //         case 2:
    //         if(!Physics2D.OverlapCircle(movePoint.transform.position + new Vector3(0.5f, 0), 0.5f, 8))
    //         {
    //             transform.position += new Vector3(vel * Time.deltaTime, 0);
    //             self.SetInteger("Horizontal", 1);
    //             self.SetInteger("Vertical", 0);
    //         }
    //         break;

    //         case 3:
    //         if(!Physics2D.OverlapCircle(movePoint.transform.position - new Vector3(0.5f, 0), 0.5f, 8))
    //         {
    //             transform.position -= new Vector3(vel * Time.deltaTime, 0);
    //             self.SetInteger("Horizontal", -1);
    //             self.SetInteger("Vertical", 0);
    //         }
    //         break;
    //     }
    // }
}
