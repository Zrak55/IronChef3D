using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChapterManager : MonoBehaviour
{
    public static int LevelToStartAt = 0;
    public static List<int> destroyOnStartEnemies;
    public static int deathsThisLevel;

    [Tooltip("For Unlock purposes")]
    public int ChapterNumber;

    float totalScore = 0;


    [SerializeField]
    public LevelSpecifics[] LevelSpecificThings;
    [SerializeField]
    private GameObject[] Gates;


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

    [Header("Level to Level score")]
    public Text scoreText;
    public Image scoreBackground;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = LevelToStartAt;
        progressManager = FindObjectOfType<LevelProgressManager>();
        selector = FindObjectOfType<AppliancePowerSelection>();
        StartLevel(LevelToStartAt);


        StartCoroutine(destroyRestartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destroyRestartDelay()
    {
        yield return new WaitForSeconds(1);
        DestroyRestartedEnemies();
    }
    public void DestroyRestartedEnemies()
    {
        if (destroyOnStartEnemies != null)
        {
            foreach (var enumber in destroyOnStartEnemies)
            {
                var e = LevelSpecificThings[currentLevel].things[enumber];
                if (e != null)
                {
                    var hp = e.GetComponent<EnemyHitpoints>();
                    if (hp != null)
                    {
                        hp.Die(false);
                    }
                }
            }
        }
    }

    public void StartLevel(int i)
    {
        FindObjectOfType<PlayerHitpoints>().RestoreHP(FindObjectOfType<PlayerHitpoints>().GetMax());
        FindObjectOfType<PlayerFoodEater>().resetEat();
        for(int j = 0; j < LevelSpecificThings.Length; j++)
        {
            bool activate = i == j;
            foreach (var go in LevelSpecificThings[j].things)
            {
                if(go != null)
                    go.SetActive(activate);
            }
            Gates[j].SetActive(!activate);
        }

        progressManager.DoAllSetup(levels[i]);
        if(selector != null)
        {
            selector.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selector.firstSelectButton);
        }
    }

    public void GoToNextLevel(float score)
    {
        currentLevel++;
        totalScore += score;
        if(destroyOnStartEnemies != null)
        {
            destroyOnStartEnemies.Clear();
        }
        deathsThisLevel = 0;

        if(currentLevel >= levels.Length) 
        {
            totalScore /= (levels.Length - LevelToStartAt);
            UnlocksManager.UnlockChapter("Chapter" + (ChapterNumber + 1).ToString());
            UnlocksManager.UnlockLevel((ChapterNumber+1).ToString() + "-0");
            ShowWinScreen(score);
        }
        else
        {
            UnlocksManager.UnlockLevel(ChapterNumber.ToString() + "-" + currentLevel.ToString());
            DisplayLevelScore(score);
            StartLevel(currentLevel);
            PlayerHitpoints.CombatCount = 0;
        }
    }

    public void DisplayLevelScore(float score)
    {
        StartCoroutine(fadeScoreIn(score));
    }

    IEnumerator fadeScoreIn(float score)
    {
        scoreText.text = score.ToString("0.00") + "/100";
        while (scoreBackground.color.a != 1)
        {
            scoreBackground.color = new Color(scoreBackground.color.r, scoreBackground.color.g, scoreBackground.color.b, Mathf.Min(scoreBackground.color.a + (2f * Time.deltaTime), 1));
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, scoreBackground.color.a);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (scoreBackground.color.a != 0)
        {
            scoreBackground.color = new Color(scoreBackground.color.r, scoreBackground.color.g, scoreBackground.color.b, Mathf.Max(scoreBackground.color.a - (2f * Time.deltaTime), 0));
            scoreText.color = new Color(scoreText.color.r, scoreText.color.g, scoreText.color.b, scoreBackground.color.a);
            yield return null;
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
        if(destroyOnStartEnemies == null)
        {
            destroyOnStartEnemies = new List<int>();
        }
        destroyOnStartEnemies.Clear();
        for( int i = 0; i < LevelSpecificThings[currentLevel].things.Length; i++)
        {
            var thing = LevelSpecificThings[currentLevel].things[i];
            if (thing == null)
            {
                destroyOnStartEnemies.Add(i);
            }
        }

        LevelToStartAt = currentLevel;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void RestartChapter()
    {
        deathsThisLevel = 0;
        if(destroyOnStartEnemies != null)
        {
            destroyOnStartEnemies.Clear();
        }
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
            firstWinSelectButton.gameObject.AddComponent<DeselectColorReset>();
        }

        ScoreText.text = "Your score: " + ((int)score).ToString() + "/100";
        int stars = 0;
        if (score < 50)
        {
            WinStatusText.text = "Your dish needs work...\nSlay more monsters to increase your score!";
            //continueButton.gameObject.SetActive(false);
        }
        else if (score < 75)
        {
            WinStatusText.text = "On the right track...";
            stars = 1;
        }
        else if (score < 100)
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
