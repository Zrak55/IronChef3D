using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
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


    public void SetMainVolume(float v)
    {
        Settings.MainVolume = v;
    }
    public void SetMusicVolume(float v)
    {
        Settings.MusicVolume = v;
    }
    public void SetSoundFXVolume(float v)
    {
        Settings.SoundFXVolume = v;
    }
    public void SetSensitivity(float v)
    {
        Settings.Sensitivity = v;
    }
}
