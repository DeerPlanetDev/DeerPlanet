using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    public static Score instance;
    public Text scoreText;
    public GameObject h;
    public GameObject player;
    public GameObject collectibles;
    public GameObject collectibles2;
    int score = 0;
    int bioPlastics = 0;
    int total;

    //Timer values
    bool timerIsActive = false;
    public Text timeText;
    float currentTime;
    public int startMinutes;

    public GameObject text;
    public GameObject now;
    public GameObject timeOut;
    public int levelNum;
    private int control = 0;

    public Text BPText;

    public Animator animator;

    public CircleCollider2D collider_;

    public GameObject UI;

    //public Gameobject canvas;

    private void Awake()
    {
        instance = this;
        h.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE  " + score.ToString();
        BPText.text = "BIOPLASTICS  " + bioPlastics.ToString();
        //Debug.Log(collectibles.transform.childCount);
        //Debug.Log(collectibles2.transform.childCount);

        //total = collectibles.transform.childCount + collectibles2.transform.childCount; -> antes del cambio a bioplasticos
        total = collectibles2.transform.childCount;
        Debug.Log(total);

        //Timer
        //        currentTime = startMinutes * 60;
        //        timeText.text = currentTime.ToString("0") + " :TIME";
        //        startTimer();
    }

    void Update()
    {
        if (now.activeSelf == true && control == 0)
        {
            text.SetActive(true);
            control = 1;
            //Timer
            currentTime = startMinutes * 60;
            timeText.text = currentTime.ToString("0") + " :TIME";
            startTimer();
        }

        if (timerIsActive == true)
        {
            currentTime = currentTime - Time.deltaTime;
            if (currentTime <= 0)
            {
                stopTimer();
                Debug.Log("U lose");
                player.SetActive(false);
                timeOut.SetActive(true);
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.TotalSeconds.ToString("0") + " :TIME";
    }

    void startTimer()
    {
        timerIsActive = true;
    }

    void stopTimer()
    {
        timerIsActive = false;
    }

    public void AddPoint(int value)
    {
        score += value;
        scoreText.text = "SCORE  " + score.ToString();
        //total = collectibles.transform.childCount + collectibles2.transform.childCount; -> antes del cambio a bioplasticos
        total = collectibles2.transform.childCount;
        Debug.Log(total);
        //Debug.Log(collectibles.transform.childCount);
        //Debug.Log(collectibles2.transform.childCount);
        if (total == 1)
        {
            // Debug.Log("Hola");
            collider_.enabled = false;
            UI.SetActive(false);
            h.SetActive(true);
            //player.SetActive(false);
            animator.SetBool("Win", true);
            stopTimer();

            PlayerPrefs.SetInt("Lv" + levelNum, 3);
            Debug.Log(PlayerPrefs.GetInt("Lv" + levelNum));

            // PopUpSystem pop = GameObject.FindGameObjectWithTag("GameController").GetComponent<PopUpSystem>();
            // pop.PopUp(popUp);
        }
    }

    public void AddBioPlastics(int value)
    {
        bioPlastics += value;
        BPText.text = "BIOPLASTICS  " + bioPlastics.ToString();

        //Logica de coleccionables proximamente 
        /*if (total == 1)
        {
            h.SetActive(true);
            player.SetActive(false);
            stopTimer();

            PlayerPrefs.SetInt("Lv" + levelNum, 3);
            Debug.Log(PlayerPrefs.GetInt("Lv" + levelNum));

        }*/
    }


    public void RemovePoint(int value)
    {
        score -= value;
        if (score > 0)
        {
            scoreText.text = "SCORE  " + score.ToString();
            Debug.Log("To bad");
        }
        else
        {
            score = 0;
            scoreText.text = "SCORE  " + score.ToString();
            Debug.Log("Close enough");
        }

        //Debug.Log(collectibles.transform.childCount);
        //Debug.Log(collectibles2.transform.childCount);
    }
}
