using System;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [Header("Base Enemy Settings")]
    public int health;

    // Event for notifying the EnemyManager when the enemy dies
    public event Action<Enemy> OnDeath;
  

    protected Animator animator;

    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        // Other common initialization logic if needed
        

    }

    public virtual void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }
    protected virtual void Die()
    {
        // Optional: Trigger death animation if you have one
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }

        // Notify the EnemyManager that this enemy has died
        OnDeath?.Invoke(this); 

        // Destroy this enemy after a short delay (if you have a death animation)
        Destroy(gameObject, 0.5f); 
    }

    public abstract void Move(); // Abstract method for movement logic (implemented in derived classes)
}