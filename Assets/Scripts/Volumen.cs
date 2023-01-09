using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Volumen : MonoBehaviour
{
    public Slider slider;
    public GameObject valor, audioPlay;
    // Start is called before the first frame update
    void Start()
    {
        audioPlay = GameObject.Find("Audio");
        slider.value =  audioPlay.GetComponent<AudioSource>().volume * 100;
        int Volumen = ((int)slider.value);
        valor.transform.GetComponent<TextMeshProUGUI>().text = Volumen.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CambiarVolumen()
    {
        int Volumen = ((int)slider.value);
        valor.transform.GetComponent<TextMeshProUGUI>().text = Volumen.ToString();
        audioPlay.GetComponent<AudioSource>().volume = slider.value / (float)100;
    }
}
