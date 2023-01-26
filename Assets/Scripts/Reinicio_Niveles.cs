using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Reinicio_Niveles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("Reinició el juego");
            Reinicio();
            SceneManager.LoadScene("LevelSelection");
        }
    }

    public void Reinicio()
    {
        for(int i = 0; i < 7; i++){
            PlayerPrefs.SetInt("Lv" + i, 0);
        }
    }
}
