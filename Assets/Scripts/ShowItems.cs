using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowItems : MonoBehaviour
{
    public List<GameObject> info;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in info)
        {
            g.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(int i)
    {
        DeactivateAll();
        info[i].SetActive(true);
    }
    void DeactivateAll()
    {
       foreach (GameObject g in info)
        {
            g.SetActive(false);
        } 
    }
}
