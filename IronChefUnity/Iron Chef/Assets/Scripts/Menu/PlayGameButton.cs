using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayGameButton : MonoBehaviour
{
    public PreLevelWindow preLevelWindow;
    public PreLevelScriptable chapterInfo;

    private void Awake()
    {

        UnlocksManager.UnlockChapter("Tutorial");
        UnlocksManager.UnlockChapter("Chapter1");
        if (UnlocksManager.HasChapter(chapterInfo.LevelName) == false)
            GetComponentInChildren<Text>().text = "Locked!";
    }
    public void ShowPreLevel()
    {
        if(UnlocksManager.HasChapter(chapterInfo.LevelName))
        {

            preLevelWindow.levelName = chapterInfo.LevelName;
            preLevelWindow.chapterNum = chapterInfo.ChapterNumber;
            preLevelWindow.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(preLevelWindow.GetComponentInChildren<Button>().gameObject);
        }
        
        

        //FindObjectOfType<MenuController>().PlayGame(levelInfo.LevelName);



        //EventSystem.current.SetSelectedGameObject(preLevelWindow.GetComponentInChildren<Button>().gameObject);
    }

    public void PlayTutorial()
    {
        preLevelWindow.levelName = chapterInfo.LevelName;
        preLevelWindow.chapterNum = chapterInfo.ChapterNumber;
        preLevelWindow.gameObject.SetActive(true);
        foreach (var i in preLevelWindow.GetComponentsInChildren<Image>())
            i.enabled = false;
        foreach (var i in preLevelWindow.GetComponentsInChildren<Text>())
            i.enabled = false;
        preLevelWindow.Play(0);
    }




}
