using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    public PreLevelWindow preLevelWindow;
    public PreLevelScriptable chapterInfo;


    public void ShowPreLevel()
    {
        
        preLevelWindow.gameObject.SetActive(true);
        preLevelWindow.levelName = chapterInfo.LevelName;
        EventSystem.current.SetSelectedGameObject(preLevelWindow.GetComponentInChildren<Button>().gameObject);
        
        

        //FindObjectOfType<MenuController>().PlayGame(levelInfo.LevelName);



        //EventSystem.current.SetSelectedGameObject(preLevelWindow.GetComponentInChildren<Button>().gameObject);
    }




}
