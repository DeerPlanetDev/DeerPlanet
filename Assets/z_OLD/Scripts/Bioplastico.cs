using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bioplastico : MonoBehaviour
{
    //public int coinValue = 1;
    int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin grabbed");
            Score.instance.AddBioPlastics(coinValue);
            VidaJugador.instance.Curar(20);
        }
    }
}
