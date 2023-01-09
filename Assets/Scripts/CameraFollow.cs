using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 pos;
    float x,y;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = GameObject.Find("Venado").transform.position;
        x = pos.x;
        y = pos.y;
        transform.position = new Vector3(x,y,-1);
    }
}
