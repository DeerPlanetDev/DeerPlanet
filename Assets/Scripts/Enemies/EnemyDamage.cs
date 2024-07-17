using UnityEngine;

public class EnemyDamage : MonoBehaviour

{
    [Header("Damage Settings")]
    public int damageAmount = 10;
    public float damageCooldown = 1f;

    private float lastDamageTime = -1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Damaging object triggered by player!");

            if (Time.time - lastDamageTime >= damageCooldown)
            {
                Debug.Log("Applying damage to player...");
                PlayerHealth.instance.ModifyHP(-damageAmount);
                lastDamageTime = Time.time;
            }
            else
            {
                Debug.Log("Damage on cooldown. Player is temporarily invincible.");
            }
        }
    }
}