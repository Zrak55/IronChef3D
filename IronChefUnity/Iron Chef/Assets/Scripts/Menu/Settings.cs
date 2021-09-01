using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    public static void SaveAllPrefs()
    {
        PlayerPrefs.Save();
    }
    public static void LoadAllSettings()
    {
        _Sensitivity = PlayerPrefs.GetFloat("Sensitivity", 5);
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
            PlayerPrefs.SetFloat("Sensitivity", _Sensitivity);
            SaveAllPrefs();
        }
    }


}
