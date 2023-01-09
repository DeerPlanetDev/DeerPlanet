using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plastico : MonoBehaviour
{
    public int coinValue = -1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin grabbed");
            Score.instance.AddPoint(coinValue);
        }
    }
}
