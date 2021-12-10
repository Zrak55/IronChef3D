using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreLevelWindow : MonoBehaviour
{
    public string levelName;
    public int chapterNum;
    public Text descText;
    public GameObject PlayButton;

    private void Awake()
    {
        foreach(var go in GetComponentsInChildren<Button>())
        {
            UnlocksManager.UnlockLevel(chapterNum.ToString() + "-0");
            if(go.gameObject.name == "Breakfast")
            {
                if (!UnlocksManager.HasLevel(chapterNum + "-0"))
                {
                    go.GetComponentInChildren<Text>().text = "Locked!";
                }
                else
                {
                    go.GetComponentInChildren<Text>().text = "Breakfast";
                }
            }
            if (go.gameObject.name == "Lunch")
            {
                if (!UnlocksManager.HasLevel(chapterNum + "-1"))
                {
                    go.GetComponentInChildren<Text>().text = "Locked!";
                }
                else
                {
                    go.GetComponentInChildren<Text>().text = "Lunch";
                }
            }
            if (go.gameObject.name == "Dinner")
            {
                if (!UnlocksManager.HasLevel(chapterNum + "-2"))
                {
                    go.GetComponentInChildren<Text>().text = "Locked!";
                }
                else
                {
                    go.GetComponentInChildren<Text>().text = "Dinner";
                }
            }
            if (go.gameObject.name == "Desert")
            {
                if (!UnlocksManager.HasLevel(chapterNum + "-3"))
                {
                    go.GetComponentInChildren<Text>().text = "Locked!";
                }
                else
                {
                    go.GetComponentInChildren<Text>().text = "Desert";
                }
            }
        }

    }
    public void Hide()
    {
        gameObject.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(transform.parent.GetComponentInChildren<Button>().gameObject);
    }
    public void Play(int start)
    {
        if (UnlocksManager.HasLevel(chapterNum + "-" + start.ToString()))
        {
            ChapterManager.LevelToStartAt = start;
            FindObjectOfType<MenuController>().PlayGame(levelName);
        }
    }


    //IEnumerator FadeAndLoad()
    //{
    //   while()
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
