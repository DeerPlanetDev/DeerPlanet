using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        target = gameObject.transform.parent.GetComponent<Transform>();
        //gameObject.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, -10);
    }
}
