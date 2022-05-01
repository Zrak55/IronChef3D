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

    public Image fadeToBlack;

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
            if (go.gameObject.name == "Dessert")
            {
                if (!UnlocksManager.HasLevel(chapterNum + "-3"))
                {
                    go.GetComponentInChildren<Text>().text = "Locked!";
                }
                else
                {
                    go.GetComponentInChildren<Text>().text = "Dessert";
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
            StartCoroutine(FadeAndLoad(start));
        }
    }


    IEnumerator FadeAndLoad(int start)
    {
        foreach (var b in FindObjectsOfType<Button>())
            b.enabled = false;
        while (fadeToBlack.color.a != 1)
        {
            fadeToBlack.color = new Color(0, 0, 0, Mathf.Clamp(fadeToBlack.color.a + Time.deltaTime, 0, 1));
            yield return null;
        }
        if (ChapterManager.destroyOnStartEnemies != null)
            ChapterManager.destroyOnStartEnemies.Clear();
        ChapterManager.LevelToStartAt = start;
        ChapterManager.deathsThisLevel = 0;
        FindObjectOfType<MenuController>().PlayGame(levelName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
