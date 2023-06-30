using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StarCollection : MonoBehaviour
{
    public Text Star_Number;
    public int Total_Stars;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ConteoDeEstrellas(Total_Stars);
        
    }

    public void ConteoDeEstrellas( int Total_Stars1)
    {
        Total_Stars1 = 0;
        for(int i = 0; i < 7; i++){
            Total_Stars1 += PlayerPrefs.GetInt("Lv" + i);
        }
        Debug.Log("Estrellas: " + Total_Stars1.ToString());
        Star_Number.text = (""+Total_Stars1.ToString());
    }
}
