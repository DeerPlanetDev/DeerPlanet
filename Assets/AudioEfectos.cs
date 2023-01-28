using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEfectos : MonoBehaviour
{
    public List<AudioClip> music;
    private void Awake()
    {
        if (GameObject.Find("EfectosAudio"))
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            this.name = "EfectosAudio";
        }
    }

    public void ChangeVolume(int newVolume)
    {
        this.gameObject.GetComponent<AudioSource>().volume = (float)newVolume / (float)100;
    }
}
