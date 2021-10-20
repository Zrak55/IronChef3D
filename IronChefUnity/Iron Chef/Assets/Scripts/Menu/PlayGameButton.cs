using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    public PreLevelWindow preLevelWindow;
    public PreLevelScriptable levelInfo;


    public void ShowPreLevel()
    {
        preLevelWindow.gameObject.SetActive(true);
        preLevelWindow.levelName = levelInfo.LevelName;
        preLevelWindow.descText.text = levelInfo.dishName + "\n";
        for(int i = 0; i < levelInfo.ingredients.Count; i++)
        {
            preLevelWindow.descText.text += "Ingredient " + (i + 1) + ": " + levelInfo.ingredients[i] + "\n";
        }

        EventSystem.current.SetSelectedGameObject(preLevelWindow.GetComponentInChildren<Button>().gameObject);
    }




}
