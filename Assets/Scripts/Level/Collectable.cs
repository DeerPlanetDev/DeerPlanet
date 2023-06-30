using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int score = 1;
    [SerializeField] int heal = 30;



    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            LevelManager.instance.IncreaseScore(score);
            PlayerHealth.instance.ModifyHP(heal);
        }
    }
}
