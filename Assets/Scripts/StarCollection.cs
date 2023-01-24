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
        Total_Stars = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Total_Stars++;
    }
}
