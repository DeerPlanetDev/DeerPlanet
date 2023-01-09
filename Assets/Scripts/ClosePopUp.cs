using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClosePopUp : MonoBehaviour
{
    public Button cont;
    public RectTransform msg;
    // Start is called before the first frame update
    void Start()
    {
        msg = GameObject.Find("Panel").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close () {
        
    }
}
