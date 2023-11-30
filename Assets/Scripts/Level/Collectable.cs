using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] int scoreValue = 1;
    [SerializeField] int bioplasticScoreValue = 2;
    [SerializeField] int heal = 30;

    [SerializeField] AudioClip collectSfx;


    //Esta parte alinea un objeto con la poscicion de una cuadricula
    void Start()
    {

        //align to tile 
        float nearestMultipleX = Mathf.FloorToInt(transform.position.x) + 0.5f;
        float nearestMultipleY = Mathf.FloorToInt(transform.position.y) + 0.5f;
        Vector3 newPosition = new Vector3(nearestMultipleX, nearestMultipleY, transform.position.z);
        transform.position = newPosition;

    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<AudioSource>().PlayOneShot(collectSfx);
            Destroy(gameObject);
            LevelManager.instance.IncreaseScore(scoreValue, bioplasticScoreValue);
            PlayerHealth.instance.ModifyHP(heal);
        }
    }
}
