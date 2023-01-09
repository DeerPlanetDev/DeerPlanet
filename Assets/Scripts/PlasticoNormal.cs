using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticoNormal : MonoBehaviour
{
    public int value = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin grabbed");
            Score.instance.AddPoint(value);
        }
    }
}
