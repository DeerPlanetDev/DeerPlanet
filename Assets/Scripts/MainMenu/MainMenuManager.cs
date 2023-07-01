using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;



public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;

    [Header("Audio")]
    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource sfxAudio;

    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip backAudio;




    [Header("Levels Ui")]
    [SerializeField] GameObject levelTemplate;
    [SerializeField] GameObject levels;


    [Header("Settings Ui")]
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;


    void Awake()
    {
        musicSlider.value = GameSettings.musicVolume;
        sfxSlider.value = GameSettings.sfxVolume;

    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevels();

    }

    // Update is called once per frame
    void Update()
    {
        if (musicAudio.volume != GameSettings.musicVolume)
            musicAudio.volume = GameSettings.musicVolume;

        if (sfxAudio.volume != GameSettings.sfxVolume)
            sfxAudio.volume = GameSettings.sfxVolume;

    }


    public void OpenUi(int uiIndex)
    {
        for (int i = 0; i < canvas.transform.childCount; i++)
        {
            GameObject ui = canvas.transform.GetChild(i).transform.gameObject;
            if (i == uiIndex)
                ui.SetActive(true);
            else
                ui.SetActive(false);
        }
    }

    void GenerateLevels()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            int sceneIndex = i;
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(sceneIndex));

            if (sceneName.Contains("evel"))
            {
                GameObject levelUi = Instantiate(levelTemplate);
                levelUi.transform.SetParent(levels.transform);
                levelUi.name = sceneName;
                levelUi.transform.localScale = new Vector3(1, 1, 1);
                levelUi.SetActive(true);
                levelUi.transform.GetChild(0).GetComponent<TMP_Text>().text = sceneName;
                levelUi.GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneIndex));
            }
        }

    }

    public void PlaySFX(int a)
    {
        AudioClip audioToPlay = a == 1 ? clickAudio : backAudio;
        sfxAudio.PlayOneShot(audioToPlay);
    }


    public void UpdateSFXVolume(float value)
    {
        GameSettings.sfxVolume = value;

    }

    public void UpdateMusicVolume(float value)
    {
        GameSettings.musicVolume = value;
    }



    void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }


}
