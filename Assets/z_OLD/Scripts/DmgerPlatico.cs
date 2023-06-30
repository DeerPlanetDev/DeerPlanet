using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgerPlatico : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            VidaJugador.instance.TakeDamage(20);
            Destroy(gameObject);
        }
    }
}
