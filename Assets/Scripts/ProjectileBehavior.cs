using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public Vector3 direction;
    public float movementSpeed = 2.5f;
    public Animator animator;

    void Start()
    {
        StartCoroutine(moveProjectile());
    }

    IEnumerator moveProjectile()
    {
        while(true)
        {
            while(direction == null)
            {
                yield return null;
            }
            transform.position += direction * movementSpeed * Time.deltaTime; 
            yield return null;
        }
    }

    IEnumerator destroyProjectile(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador.instance.TakeDamage(20);
            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject); //animar destrucción
        }
        else if (other.CompareTag("Wall"))
        {
            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject); //animar destrucción
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(destroyProjectile(other));
    }

    void Update()
    {

    }
}