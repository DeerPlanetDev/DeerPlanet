using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class VolumenEfectos : MonoBehaviour
{
    public Slider slider2;
    public GameObject valor2, audioPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay = GameObject.Find("EfectosAudio");
        slider2.value = audioPlay.GetComponent<AudioSource>().volume * 100;
        int volumenEfectos = ((int)slider2.value);
        valor2.transform.GetComponent<TextMeshProUGUI>().text = volumenEfectos.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CambiarVolumenEfectos()
    {
        int volumenEfectos = ((int)slider2.value);
        valor2.transform.GetComponent<TextMeshProUGUI>().text = volumenEfectos.ToString();
        audioPlay.GetComponent<AudioSource>().volume = slider2.value / (float)100;
    }
}