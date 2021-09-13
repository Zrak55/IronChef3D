using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject[] Menus;

    [Space]
    public AudioMixer audioM;

    public Slider mainVolSlider;
    public Slider musicVolSlider;
    public Slider fxVolSlider;
    public Slider sensitivitySlider;
    bool allowedToSet = false;

    private void Awake()
    {
        

    }

    // Start is called before the first frame update
    void Start()
    {
        Settings.LoadAllSettings(audioM);
        LoadSettingsSliderValues();
    }

    // Update is called once per frame
    void Update()
    {

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

        allowedToSet = true;
    }

    

    public void GoToMenu(int i)
    {
        foreach (var g in Menus)
        {
            g.SetActive(false);
        }
        Menus[i].SetActive(true);
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

}
