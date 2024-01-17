using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCtrl : MonoBehaviour
{
    GameObject audio_;
    AudioSource player;
    Audio track;
    string previousName;
    private void Start() {
        audio_ = GameObject.Find("Audio");
        player = audio_.GetComponent<AudioSource>();
        track = audio_.GetComponent<Audio>();

    }
    public void LevelSelection()
    {
        player.clip = track.music[4];
        player.Play();
        SceneManager.LoadScene("LevelSelection");
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void AjustesMenu()
    {
        PlayerPrefs.SetString("previous", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Ajustes");
    }
    public void Level1()
    {
        
        player.clip = track.music[1];
        player.Play();
        SceneManager.LoadScene("FirstLevel");
    }
    public void Level2()
    {
        player.clip = track.music[1];
        player.Play();
        SceneManager.LoadScene("SecondLevel");
    }
    public void Level3()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("ThirdLevel");
    }
    public void Level4()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("FourthLevel");
    }
    public void Level5()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("FifthLevel");
    }
    public void Level6()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("SixthLevel");
    }
    public void Level7()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("SeventhLevel");
    }
    public void Level8()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("EightLevel");
    }
    public void Level9()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene("NinthLevel");
    }
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        player.clip = track.music[0];
        player.Play();
        SceneManager.LoadScene(PlayerPrefs.GetString("previous"));
    }
}