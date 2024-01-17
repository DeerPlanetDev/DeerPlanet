using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    [SerializeField] private bool unlocked; //Default value is false
    public Image unlockImage; //Lock image
    public GameObject[] stars; //Star gameobject
    public Sprite starSprite; //Filled star sprite
    public int star_num = 0;
    GameObject audio_;
    AudioSource player;
    Audio track;

    // Start is called before the first frame update
    void Start()
    {
        audio_ = GameObject.Find("Audio");
        player = audio_.GetComponent<AudioSource>();
        track = audio_.GetComponent<Audio>();

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevelStatus();
        UpdateLevelImage();
    }

    private void UpdateLevelStatus() //Desbloquear niveles
    {
        int PreviousLevelNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + PreviousLevelNum) > 0)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage() //Actualizar estrellas por nivel y quitando candados
    {
        if (!unlocked)
        {
            unlockImage.gameObject.SetActive(true);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(false);
            }
        }
        else
        {
            unlockImage.gameObject.SetActive(false);
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].gameObject.SetActive(true);
            }
            // PlayerPrefs.SetInt("Lv" + int.Parse(gameObject.name), star_num);
            for (int i = 0; i < PlayerPrefs.GetInt("Lv" + int.Parse(gameObject.name)); i++)
            {
                stars[i].gameObject.GetComponent<Image>().sprite = starSprite;
            }
        }
    }

    public void PressSelection(string _levelName) //Seleccionando niveles
    {
        if(unlocked)
        {
            player.clip = track.music[1];
            player.Play();
            SceneManager.LoadScene(_levelName);
        }
    }
}
