using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Menu")]
    public GameObject[] Menus;

    [Space]
    public AudioMixer audioM;

    [Header("Settings")]
    public Slider mainVolSlider;
    public Slider musicVolSlider;
    public Slider fxVolSlider;
    public Slider sensitivitySlider;
    public Toggle invertVerticalCam;

    private void Awake()
    {    

    }

    // Start is called before the first frame update
    void Start()
    {
        Settings.LoadAllSettings(audioM);

        SetSettingsListeners();

        LoadSettingsSliderValues();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetSettingsListeners()
    {
        mainVolSlider.onValueChanged.AddListener(delegate
        {
            SetMainVolume();
        });

        musicVolSlider.onValueChanged.AddListener(delegate
        {
            SetMusicVolume();
        });

        fxVolSlider.onValueChanged.AddListener(delegate
        {
            SetSoundFXVolume();
        });

        sensitivitySlider.onValueChanged.AddListener(delegate
        {
            SetSensitivity();
        });

        invertVerticalCam.onValueChanged.AddListener(delegate
        {
            SetInvertVerticalCam();
        });
    }

    public void PlayGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    void LoadSettingsSliderValues()
    {
        mainVolSlider.value = Settings.MainVolume;
        musicVolSlider.value = Settings.MusicVolume;
        fxVolSlider.value = Settings.SoundFXVolume;
        sensitivitySlider.value = Settings.Sensitivity;
        invertVerticalCam.isOn = Settings.InvertVerticalCam;
    }

    

    public void GoToMenu(int i)
    {
        foreach (var g in Menus)
        {
            g.SetActive(false);
        }
        Menus[i].SetActive(true);
        EventSystem.current.SetSelectedGameObject(Menus[i].GetComponentInChildren<Button>().gameObject);
    }

    public void SetMainVolume()
    {
        Settings.MainVolume = mainVolSlider.value;
    }
    public void SetMusicVolume()
    {
        Settings.MusicVolume = musicVolSlider.value;
    }
    public void SetSoundFXVolume()
    {
        Settings.SoundFXVolume = fxVolSlider.value;
    }
    public void SetSensitivity()
    {
        Settings.Sensitivity = sensitivitySlider.value;
    }
    public void SetInvertVerticalCam()
    {
        Settings.InvertVerticalCam = invertVerticalCam.isOn;
    }

    public void DEBUG_ClearAllUnlocks()
    {
        PlayerPrefs.DeleteAll();
    }

    public void DEBUG_UnlockAllLevels()
    {
        for(int i = 0; i < 1000; i++)
        {
            UnlocksManager.UnlockChapter("Chapter" + i.ToString());
            for(int j = 0; j < 4; j++)
            {
                UnlocksManager.UnlockLevel(i.ToString() + "-" + j.ToString());
            }
        }
    }
}
