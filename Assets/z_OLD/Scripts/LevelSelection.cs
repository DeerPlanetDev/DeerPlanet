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
    public Sprite starSprite, emptyStarSprite; //Filled star sprite
    public int star_num = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        else //Para el test de borrar Progreso
        {
            if(PreviousLevelNum != 0)
                unlocked = false;
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
        if (PlayerPrefs.GetInt("Lv" + int.Parse(gameObject.name)) == 0)   //Para el test de borrar Progreso
        {
            for(int i = 0; i < stars.Length; i++)
                stars[i].gameObject.GetComponent<Image>().sprite = emptyStarSprite;
        }
    }

    public void PressSelection(string _levelName) //Seleccionando niveles
    {
        if(unlocked)
        {
            SceneManager.LoadScene(_levelName);
        }
    }
}
