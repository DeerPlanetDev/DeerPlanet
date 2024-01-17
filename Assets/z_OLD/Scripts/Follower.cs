using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //bool flag = VidaJugador.life;
        bool flag = true;
        if (flag)
        {
            transform.position = new Vector3(target.position.x, target.position.y, -10);
        }
        else
        {
            Debug.Log(flag);
            Debug.Log("U ded m8");
        }
        
    }
}


