using System.Collections;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 direction;
    public float movementSpeed = 2.5f;
    public Animator animator;
    [SerializeField] AudioClip damageSfx;
    [SerializeField] int damage = -15;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private float lifetime = 5f; 

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }

    IEnumerator TrackAndMoveProjectile()
    {
        float timeElapsed = 0f;
        while (timeElapsed < lifetime)
        {
            // Ensure the direction is set
            if (direction != Vector3.zero) 
            {
                // Move the projectile in the specified direction
                transform.position += direction * movementSpeed * Time.deltaTime;
            }

            // Check for collisions with the explosion layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, movementSpeed * Time.deltaTime, explosionLayerMask);
            if (hit.collider != null)
            {
                Explode();
                yield break;
            }

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Explode after the lifetime expires
        Explode(); 
    }

    private void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // (Optional) Play explosion sound effect
        // ...

        Destroy(gameObject);
    }

    IEnumerator destroyProjectile(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth.instance.ModifyHP(damage);
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx);
            Debug.Log("Impacted the player");

            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Debug.Log("Impacted a wall");
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(damageSfx);

            animator.SetBool("crashed", true);
            yield return new WaitForSeconds(0.3f);
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(destroyProjectile(other));
    }

    void Start()
    {
        StartCoroutine(TrackAndMoveProjectile());
    }
}