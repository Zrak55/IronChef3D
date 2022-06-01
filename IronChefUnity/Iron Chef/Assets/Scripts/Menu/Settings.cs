using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class Settings
{
    private static AudioMixer audioMixer;

    public static void SaveAllPrefs()
    {
        FBPP.Save();
    }
    public static void LoadAllSettings(AudioMixer mixer)
    {
        audioMixer = mixer;
        Sensitivity = FBPP.GetFloat("Sensitivity", 3);
        MainVolume = FBPP.GetFloat("MainVolume", 1);
        SoundFXVolume = FBPP.GetFloat("SoundFXVolume", 1);
        MusicVolume = FBPP.GetFloat("MusicVolume", 1);
        InvertVerticalCam = FBPP.GetInt("InvertVerticalCam", 1) == 1;
        


        audioMixer.SetFloat("MainVolume", Mathf.Log10(_MainVolume) * 20);
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(_SoundFXVolume) * 20);
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(_MusicVolume) * 20);



    }


    private static float _Sensitivity = 5;
    public static float Sensitivity
    {
        get
        {
            return _Sensitivity;
        }
        set
        {
            _Sensitivity = value;
            FBPP.SetFloat("Sensitivity", _Sensitivity);
            SaveAllPrefs();
        }
    }

    private static float _MainVolume = 1;
    public static float MainVolume
    {
        get
        {
            return _MainVolume;
        }
        set
        {
            _MainVolume = value;
            FBPP.SetFloat("MainVolume", _MainVolume);

            Debug.Log(audioMixer);

            audioMixer.SetFloat("MainVolume", Mathf.Log10(value) * 20);
            SaveAllPrefs();
        }
    }

    private static float _SoundFXVolume = 1;
    public static float SoundFXVolume
    {
        get
        {
            return _SoundFXVolume;
        }
        set
        {
            _SoundFXVolume = value;
            FBPP.SetFloat("SoundFXVolume", _SoundFXVolume);
            
            audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(value) * 20);
            SaveAllPrefs();
        }
    }

    private static float _MusicVolume = 1;
    public static float MusicVolume
    {
        get
        {
            return _MusicVolume;
        }
        set
        {
            _MusicVolume = value;
            FBPP.SetFloat("MusicVolume", _MusicVolume);

            audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
            SaveAllPrefs();
        }
    }

    private static bool _InvertVerticalCam;
    public static bool InvertVerticalCam
    {
        get
        {
            return _InvertVerticalCam;
        }
        set
        {
            _InvertVerticalCam = value;
            if (value == true)
                FBPP.SetInt("InvertVerticalCam", 1);
            else
                FBPP.SetInt("InvertVerticalCam", 0);

            SaveAllPrefs();
        }
       
    }

}
