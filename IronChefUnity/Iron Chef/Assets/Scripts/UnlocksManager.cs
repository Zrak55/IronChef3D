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
        string currentUnlocks = FBPP.GetString(PowerUnlockName, "");
        if(currentUnlocks.Contains(PowerPrefix + n) == false)
        {
            currentUnlocks += (PowerPrefix + n);
            FBPP.SetString(PowerUnlockName, currentUnlocks);
            FBPP.Save();


            AppliancePowerSelection.CheckUnlocks();
        }
    }
    public static void UnlockAppliance(string n)
    {
        string currentUnlocks = FBPP.GetString(ApplianceUnlockName, "");
        if (currentUnlocks.Contains(AppliancePrefix + n) == false)
        {
            currentUnlocks += (AppliancePrefix + n);
            FBPP.SetString(ApplianceUnlockName, currentUnlocks);
            FBPP.Save();

            AppliancePowerSelection.CheckUnlocks();
        }
    }
    public static void UnlockChapter(string n)
    {
        string currentUnlocks = FBPP.GetString(ChapterUnlockName, "");
        if (currentUnlocks.Contains(ChapterPrefix + n) == false)
        {
            currentUnlocks += (ChapterPrefix + n);
            FBPP.SetString(ChapterUnlockName, currentUnlocks);
            FBPP.Save();


        }
    }
    public static void UnlockLevel(string n)
    {
        string currentUnlocks = FBPP.GetString(LevelUnlockName, "");
        if (currentUnlocks.Contains(LevelPrefix + n) == false)
        {
            currentUnlocks += (LevelPrefix + n);
            FBPP.SetString(LevelUnlockName, currentUnlocks);
            FBPP.Save();
        }
    }


    public static bool HasPower(string n)
    {
        string currentUnlocks = FBPP.GetString(PowerUnlockName, "");
        return (currentUnlocks.Contains(PowerPrefix + n));
    }
    public static bool HasAppliance(string n)
    {
        string currentUnlocks = FBPP.GetString(ApplianceUnlockName, "");
        return (currentUnlocks.Contains(AppliancePrefix + n));
    }
    public static bool HasChapter(string n)
    {
        string currentUnlocks = FBPP.GetString(ChapterUnlockName, "");
        return (currentUnlocks.Contains(ChapterPrefix + n));
    }
    public static bool HasLevel(string n)
    {
        string currentUnlocks = FBPP.GetString(LevelUnlockName, "");
        return (currentUnlocks.Contains(LevelPrefix + n));
    }
}


