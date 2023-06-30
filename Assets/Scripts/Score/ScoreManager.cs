using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] float timeLeft = 5;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;


    public static ScoreManager instance;
    private int score = 0;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        UpdateTime();
        UpdateScore();

    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score.ToString();
    }


    void UpdateTime()
    {
        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, 1000);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void IncreaseScore(int value)
    {
        score += value;
    }





}
