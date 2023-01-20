using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestWin : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            print("space key was pressed");
            animator.SetBool("Win", true);
        }
    }
}
