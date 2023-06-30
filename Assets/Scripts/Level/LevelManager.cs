using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{

    [SerializeField] float timeLeft = 30;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject levelCompletedUi;
    [SerializeField] GameObject outOfTimeUi;
    [SerializeField] GameObject deathUi;

    public static LevelManager instance;
    private int score = 0;
    private bool levelEnded = false;

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
        if (GameObject.FindGameObjectsWithTag("Collectable").Length == 0)
            EndLevel(1);

    }

    void HideAllUi()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            foreach (Transform child in canvas.transform)
                child.gameObject.SetActive(false);
        }
    }


    void UpdateTime()
    {
        timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, 1000);
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timeText.text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");

        if (timeLeft == 0)
            EndLevel(2);
    }


    public void EndLevel(int type)
    {
        //type: 1 = win, 2 = out of time, 3 = death
        if (!levelEnded)
        {
            levelEnded = true;
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
            GameObject uiToShow = type == 1 ? levelCompletedUi : (type == 2 ? outOfTimeUi : deathUi);
            HideAllUi();
            uiToShow.SetActive(true);
        }
    }

    public void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextSceneIndex);
        else
            SceneManager.LoadScene(0);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void IncreaseScore(int value)
    {
        score += value;
    }







}
