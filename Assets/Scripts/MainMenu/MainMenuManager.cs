using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] PostProcessProfile postProcessingProfile;
    [SerializeField] PostProcessLayer postProcessingLayer;

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
    [SerializeField] Slider brightnessSlider;
    [Header("Settings Text Counters")]
    [SerializeField] TextMeshProUGUI musicCounter;
    [SerializeField] TextMeshProUGUI sfxCounter;
    [SerializeField] TextMeshProUGUI brightnessCounter;

    void Awake()
    {
        musicSlider.value = GameSettings.musicVolume;
        sfxSlider.value = GameSettings.sfxVolume;
        brightnessSlider.value = GameSettings.brightness;
        musicCounter.text = ((int)(GameSettings.musicVolume * 100)).ToString();
        sfxCounter.text = ((int)(GameSettings.sfxVolume * 100)).ToString();
        brightnessCounter.text = GetUIBrightnessText(GameSettings.brightness);
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
                // Por alguna razón cada objeto de este nivel cambia su valor de z a -4500, lo recoloque a 0.
                RectTransform rt = levelUi.GetComponent<RectTransform>();
                levelUi.GetComponent<RectTransform>().position = new Vector3(rt.position.x, rt.position.y, 100);

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
        sfxCounter.text = ((int)(value * 100)).ToString();
    }

    public void UpdateMusicVolume(float value)
    {
        GameSettings.musicVolume = value;
        musicCounter.text = ((int)(value * 100)).ToString();
    }

    public void UpdateBrightness(float value)
    {
        GameSettings.brightness = value;
        postProcessingProfile.TryGetSettings(out AutoExposure exposure);
        exposure.keyValue.value = value;
        brightnessCounter.text = GetUIBrightnessText(value);
    }

    void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    string GetUIBrightnessText(float value)
    {
        float normal = 
            Mathf.InverseLerp(brightnessSlider.minValue, brightnessSlider.maxValue, value);
        return ((int)Mathf.Lerp(0f, 100f, normal)).ToString();   
    }

}
