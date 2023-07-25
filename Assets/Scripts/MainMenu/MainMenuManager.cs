using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using System;
using UnityEngine.Audio;
using Unity.Profiling;
using UnityEditor;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject canvas;
    [SerializeField] PostProcessProfile postProcessingProfile;
    [SerializeField] PostProcessLayer postProcessingLayer;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] AudioSource musicAudio;
    [SerializeField] AudioSource buttonsAudio;

    [SerializeField] AudioClip clickAudio;
    [SerializeField] AudioClip backAudio;

    [Header("Levels Ui")]
    [SerializeField] GameObject levelTemplate;
    [SerializeField] GameObject levels;


    [Header("Settings Ui")]
    [SerializeField] Slider generalSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider buttonSlider;
    [SerializeField] Slider playerSlider;
    [SerializeField] Slider brightnessSlider;
    [Header("Settings Text Counters")]
    [SerializeField] TextMeshProUGUI generalCounter;
    [SerializeField] TextMeshProUGUI musicCounter;
    [SerializeField] TextMeshProUGUI sfxCounter;
    [SerializeField] TextMeshProUGUI buttonCounter;
    [SerializeField] TextMeshProUGUI playerCounter;
    [SerializeField] TextMeshProUGUI brightnessCounter;

    void Awake()
    {
        DefaultVolSlider(generalSlider, GameSettings.generalVolume, ref generalCounter);
        DefaultVolSlider(musicSlider, GameSettings.musicVolume, ref musicCounter);
        DefaultVolSlider(sfxSlider, GameSettings.sfxVolume, ref sfxCounter);
        DefaultVolSlider(buttonSlider, GameSettings.buttonsVolume, ref buttonCounter);
        DefaultVolSlider(playerSlider, GameSettings.playerVolume, ref playerCounter);

        UpdateBrightness(GameSettings.brightness);
        brightnessSlider.value = GameSettings.brightness;
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateLevels();
    }

    // Update is called once per frame
    void Update()
    {
        audioMixer.GetFloat("musicVol", out float volume);
        if (volume != GameSettings.musicVolume)
            audioMixer.SetFloat("musicVol", GameSettings.musicVolume);

        audioMixer.GetFloat("generalVol", out volume);
        if (volume != GameSettings.generalVolume)
            audioMixer.SetFloat("generalVol", GameSettings.generalVolume);

        audioMixer.GetFloat("sfxVol", out volume);
        if (volume != GameSettings.sfxVolume)
            audioMixer.SetFloat("sfxVol", GameSettings.sfxVolume);

        audioMixer.GetFloat("playerVol", out volume);
        if (volume != GameSettings.playerVolume)
            audioMixer.SetFloat("playerVol", GameSettings.playerVolume);

        audioMixer.GetFloat("buttonsVol", out volume);
        if (volume != GameSettings.buttonsVolume)
            audioMixer.SetFloat("buttonsVol", GameSettings.buttonsVolume);
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
                // Por alguna raz�n cada objeto de este nivel cambia su valor de z a -4500, lo recoloque a 0.
                RectTransform rt = levelUi.GetComponent<RectTransform>();
                levelUi.GetComponent<RectTransform>().position = new Vector3(rt.position.x, rt.position.y, 100);

                levelUi.transform.GetChild(0).GetComponent<TMP_Text>().text = sceneName;
                levelUi.GetComponent<Button>().onClick.AddListener(() => LoadScene(sceneIndex));
            }
        }


    }

    public void PlayButtons(int a)
    {
        AudioClip audioToPlay = a == 1 ? clickAudio : backAudio;
        buttonsAudio.PlayOneShot(audioToPlay);
    }

    public void UpdateGeneralVolume(float val)
    {
        AdjustAudioMixerGroup("generalVol",
               val, generalCounter,
               ref GameSettings.generalVolume);
    }

    public void UpdateSFXVolume(float val)
    {
        AdjustAudioMixerGroup("sfxVol",
            val, sfxCounter,
            ref GameSettings.sfxVolume);
    }

    public void UpdateButtonsVolume(float val)
    {
        AdjustAudioMixerGroup("buttonsVol",
            val, buttonCounter,
            ref GameSettings.buttonsVolume);
    }

    public void UpdatePlayerVolume(float val)
    {
        AdjustAudioMixerGroup("playerVol",
            val, playerCounter,
            ref GameSettings.playerVolume);
    }
    public void UpdateMusicVolume(float val)
    {
        AdjustAudioMixerGroup("musicVol",
            val, musicCounter,
            ref GameSettings.musicVolume);
    }

    // Value must vary between -25f and 5f in each slider.
    private void AdjustAudioMixerGroup(string id, float val,
        TextMeshProUGUI counter, ref float gameSettingVal)
    {

        if (val <= -25f)
        {
            gameSettingVal = -80f;
            counter.text = 0.ToString();
        }
        else if (audioMixer.SetFloat(id, val))
        {
            gameSettingVal = val;
            float textual = Mathf.InverseLerp(-25f, 5f, val);
            textual = Mathf.Lerp(0f, 100f, textual);
            counter.text = ((int)textual).ToString();
        }
    }

    public void UpdateBrightness(float val)
    {
        GameSettings.brightness = val;
        postProcessingProfile.TryGetSettings(out AutoExposure exposure);
        exposure.keyValue.value = val;
        brightnessCounter.text = GetUIBrightnessText(val);
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

    private void DefaultVolSlider(Slider slider, float value, ref TextMeshProUGUI text)
    {
        float adjust = value < -25f ? -25f : value;
        slider.value = adjust;

        adjust = Mathf.InverseLerp(-25f, 5f, adjust);
        adjust = Mathf.Lerp(0f, 100f, adjust);
        text.text = ((int)adjust).ToString();
    }

}