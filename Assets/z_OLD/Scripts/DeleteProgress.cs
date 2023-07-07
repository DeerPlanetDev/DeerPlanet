using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteProgress : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete()
    {
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetInt("Lv" + i,0);
        }
    }
}
