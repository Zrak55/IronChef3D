using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnlocksManager
{
    static string PowerUnlockName = "PowerUnlocks";
    static string ApplianceUnlockName = "ApplianceUnlocks";
    static string ChapterUnlockName = "ChapterUnlocks";
    static string LevelUnlockName = "LevelUnlocks";

    static string PowerPrefix = "P_";
    static string AppliancePrefix = "A_";
    static string ChapterPrefix = "C_";
    static string LevelPrefix = "L_";

    public static void UnlockPower(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(PowerUnlockName, "");
        if(currentUnlocks.Contains(PowerPrefix + n) == false)
        {
            currentUnlocks += (PowerPrefix + n);
            PlayerPrefs.SetString(PowerUnlockName, currentUnlocks);
            PlayerPrefs.Save();


            AppliancePowerSelection.CheckUnlocks();
        }
    }
    public static void UnlockAppliance(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(ApplianceUnlockName, "");
        if (currentUnlocks.Contains(AppliancePrefix + n) == false)
        {
            currentUnlocks += (AppliancePrefix + n);
            PlayerPrefs.SetString(ApplianceUnlockName, currentUnlocks);
            PlayerPrefs.Save();

            AppliancePowerSelection.CheckUnlocks();
        }
    }
    public static void UnlockChapter(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(ChapterUnlockName, "");
        if (currentUnlocks.Contains(ChapterPrefix + n) == false)
        {
            currentUnlocks += (ChapterPrefix + n);
            PlayerPrefs.SetString(ChapterUnlockName, currentUnlocks);
            PlayerPrefs.Save();


        }
    }
    public static void UnlockLevel(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(LevelUnlockName, "");
        if (currentUnlocks.Contains(LevelPrefix + n) == false)
        {
            currentUnlocks += (LevelPrefix + n);
            PlayerPrefs.SetString(LevelUnlockName, currentUnlocks);
            PlayerPrefs.Save();
        }
    }


    public static bool HasPower(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(PowerUnlockName, "");
        return (currentUnlocks.Contains(PowerPrefix + n));
    }
    public static bool HasAppliance(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(ApplianceUnlockName, "");
        return (currentUnlocks.Contains(AppliancePrefix + n));
    }
    public static bool HasChapter(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(ChapterUnlockName, "");
        return (currentUnlocks.Contains(ChapterPrefix + n));
    }
    public static bool HasLevel(string n)
    {
        string currentUnlocks = PlayerPrefs.GetString(LevelUnlockName, "");
        return (currentUnlocks.Contains(LevelPrefix + n));
    }
}


