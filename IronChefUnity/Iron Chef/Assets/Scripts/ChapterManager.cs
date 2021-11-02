using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    public static int LevelToStartAt = 0;

    float totalScore = 0;


    [SerializeField]
    public LevelSpecifics[] LevelSpecificThings;


    public LevelScriptable[] levels;

    public LevelProgressManager progressManager;
    public AppliancePowerSelection selector;

    public int currentLevel = 0;

    [Header("Win/Lose Screen")]
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public Text WinStatusText;
    public Text ScoreText;
    public Button continueButton;
    public GameObject firstWinSelectButton;
    public GameObject firstLoseSelectButton;

    // Start is called before the first frame update
    void Start()
    {
        progressManager = FindObjectOfType<LevelProgressManager>();
        selector = FindObjectOfType<AppliancePowerSelection>();
        StartLevel(LevelToStartAt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartLevel(int i)
    {
        for(int j = 0; j < LevelSpecificThings.Length; j++)
        {
            bool activate = i == j;
            foreach (var go in LevelSpecificThings[i].things)
            {
                if(go != null)
                    go.SetActive(activate);
            }
        }

        progressManager.DoAllSetup(levels[i]);
        if(selector != null)
            selector.gameObject.SetActive(true);
    }

    public void GoToNextLevel(float score)
    {
        currentLevel++;
        totalScore += score;
        if(currentLevel >= levels.Length)
        {
            totalScore /= (levels.Length - LevelToStartAt); 
            ShowWinScreen(totalScore);
        }
        else
        {
            StartLevel(currentLevel);
        }
    }

    



    public void ContinueOn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartLevel()
    {
        LevelToStartAt = currentLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartChapter()
    {
        LevelToStartAt = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
        IronChefUtils.TurnOffCharacter();
        EventSystem.current.SetSelectedGameObject(firstLoseSelectButton);

        if (firstLoseSelectButton.GetComponent<Image>().color != firstLoseSelectButton.GetComponent<Button>().colors.selectedColor)
        {
            firstLoseSelectButton.GetComponent<Image>().color = firstLoseSelectButton.GetComponent<Button>().colors.selectedColor;
            firstLoseSelectButton.gameObject.AddComponent<DeselectColorReset>();
        }
    }
    public void ShowWinScreen(float score)
    {
        WinScreen.SetActive(true);

        EventSystem.current.SetSelectedGameObject(firstWinSelectButton);
        if (firstWinSelectButton.GetComponent<Image>().color != firstWinSelectButton.GetComponent<Button>().colors.selectedColor)
        {
            firstWinSelectButton.GetComponent<Image>().color = firstWinSelectButton.GetComponent<Button>().colors.selectedColor;
            firstWinSelectButton.gameObject.AddComponent<DeselectColorReset>();
        }

        ScoreText.text = "Your score: " + ((int)score).ToString() + "/100";
        int stars = 0;
        if (score < 50)
        {
            WinStatusText.text = "Your dish needs work...\nTry to avoid unnecessary ingredients!";
            //continueButton.gameObject.SetActive(false);
        }
        else if (score < 75)
        {
            WinStatusText.text = "On the right track...";
            stars = 1;
        }
        else if (score < 90)
        {
            WinStatusText.text = "Scrumptious!";
            stars = 2;
        }
        else
        {
            WinStatusText.text = "Perfection!";
            stars = 3;
        }
    }








    [System.Serializable]
    public struct LevelSpecifics
    {
        public GameObject[] things;
    }
}
