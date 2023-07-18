using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*-----------------------------------------------------
 
Revisar que objetos son los que faltan
 
------------------------------------------------------- */

public class Enemy3Behaviour : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public Animator animator;
    public int direccion;
    public float speed_walk;
    public float speed_run;
    public GameObject target;
    public bool atacar;

    public float rango_vision;
    public float rango_ataque;
    public GameObject rango; //Vision de vigilia del enemigo?
    public GameObject Hit; //Vision de ataque del enemigo?

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }


    public void Comportamientos()
    {
        if (Mathf.Abs(transform.position.x - target.transform.position.x)>rango_vision && !atacar)
        {
            animator.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if (cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }

            switch (rutina)
            {
                case 0:
                    animator.SetBool("walk", false);
                    break;

                case 1:
                    direccion = Random.Range(0, 2);
                    rutina++;
                    break;

                case 2:
                    switch (direccion)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0, 0, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;

                        case 1:
                            transform.rotation = Quaternion.Euler(0, 180, 0);
                            transform.Translate(Vector3.right * speed_walk * Time.deltaTime);
                            break;
                    }
                    animator.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rango_ataque && !atacar)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    animator.SetBool("attack", false);
                }
                else
                {
                    animator.SetBool("walk", false);
                    animator.SetBool("run", true);
                    transform.Translate(Vector3.right * speed_run * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                    animator.SetBool("attack", false);
                }
            }
            else
            {
                if (!atacar)
                {
                    if (transform.position.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0, 180, 0);
                    }
                    animator.SetBool("walk", false);
                    animator.SetBool("run", false);
                }
            }
        }
        
    }

    public void Final_Ani()
    {
        animator.SetBool("attack", false);
        atacar = false;
        rango.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void ColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    }
    public void ColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        Comportamientos();
    }
}
