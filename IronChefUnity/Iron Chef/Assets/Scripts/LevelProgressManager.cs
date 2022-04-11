using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager levelProgressManager;


    public Image startBackground;
    public Text startText;


    float score;

    public LevelScriptable level;

    ChapterManager chapters;

    [HideInInspector]
    public int ingredientOneRequired;
    [HideInInspector]
    public int ingredientTwoRequired;
    [HideInInspector]
    public int ingredientThreeRequired;
    [HideInInspector]
    public int ingredientFourRequired;
    [HideInInspector]
    public int ingredientFiveRequired;
    [Space]
    [HideInInspector]
    public int ingredientOneMaximum;
    [HideInInspector]
    public int ingredientTwoMaximum;
    [HideInInspector]
    public int ingredientThreeMaximum;
    [HideInInspector]
    public int ingredientFourMaximum;
    [HideInInspector]
    public int ingredientFiveMaximum;
    [Space]
    public int ingredientOneCurrent = 0;
    public int ingredientTwoCurrent = 0;
    public int ingredientThreeCurrent = 0;
    public int ingredientFourCurrent = 0;
    public int ingredientFiveCurrent = 0;
    [Space]
    [HideInInspector]
    public EnemyFoodDropper.FoodType ingredientOneType = 0;
    [HideInInspector]
    public EnemyFoodDropper.FoodType ingredientTwoType = 0;
    [HideInInspector]
    public EnemyFoodDropper.FoodType ingredientThreeType = 0;
    [HideInInspector]
    public EnemyFoodDropper.FoodType ingredientFourType = 0;
    [HideInInspector]
    public EnemyFoodDropper.FoodType ingredientFiveType = 0;


    public int badIngredientsCurrent = 0;
    public int badIngredientsMaximum;

    

    [Space]
    public int currentLevel = 0;


    IEnumerator fading;

    // Start is called before the first frame update
    void Start()
    {
        chapters = FindObjectOfType<ChapterManager>();
        levelProgressManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoAllSetup(LevelScriptable newLevel)
    {
        level = newLevel;

        SetInformation();

        GetMaxes();



        //DisplayDish();

        CompleteSetup();
    }

    public void DisplayDish()
    {
        startBackground.color = new Color(startBackground.color.r, startBackground.color.g, startBackground.color.b, 0);
        startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, startBackground.color.a);
        if(fading != null)
        {
            StopCoroutine(fading);
            fading = null;
        }
        startText.text = "Your next dish..." + level.dish + ".";
        fading = fadeStartThings();
        StartCoroutine(fading);
    }
    IEnumerator fadeStartThings()
    {
        while(startBackground.color.a != 1)
        {
            startBackground.color = new Color(startBackground.color.r, startBackground.color.g, startBackground.color.b, Mathf.Min(startBackground.color.a + (2f * Time.deltaTime), 1));
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, startBackground.color.a);
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        while (startBackground.color.a != 0)
        {
            startBackground.color = new Color(startBackground.color.r, startBackground.color.g, startBackground.color.b, Mathf.Max(startBackground.color.a - (0.5f * Time.deltaTime), 0));
            startText.color = new Color(startText.color.r, startText.color.g, startText.color.b, startBackground.color.a);
            yield return null;
        }
    }

    private void SetInformation()
    {
        ingredientOneRequired = level.ingredientOneRequired;
        ingredientTwoRequired = level.ingredientTwoRequired;
        ingredientThreeRequired = level.ingredientThreeRequired;
        ingredientFourRequired = level.ingredientFourRequired;
        ingredientFiveRequired = level.ingredientFiveRequired;


        ingredientOneType = level.ingredientOneType;
        ingredientTwoType = level.ingredientTwoType;
        ingredientThreeType = level.ingredientThreeType;
        ingredientFourType = level.ingredientFourType;
        ingredientFiveType = level.ingredientFiveType;

        PlayerHitpoints.CombatCount = 0;
    }

    private void GetMaxes()
    {
        badIngredientsMaximum = 0;
        ingredientFiveMaximum = 0;
        ingredientFourMaximum = 0;
        ingredientThreeMaximum = 0;
        ingredientTwoMaximum = 0;
        ingredientOneMaximum = 0;
        foreach(var m in FindObjectsOfType<EnemyFoodDropper>())
        {
            if (m.food == ingredientOneType)
                ingredientOneMaximum++;
            else if (m.food == ingredientTwoType)
                ingredientTwoMaximum++;
            else if (m.food == ingredientThreeType)
                ingredientThreeMaximum++;
            else if (m.food == ingredientFourType)
                ingredientFourMaximum++;
            else if (m.food == ingredientFiveType)
                ingredientFiveMaximum++;
            else
                badIngredientsMaximum++;
        }
    }

    void CompleteSetup()
    {
        ingredientFiveCurrent = 0;
        ingredientFourCurrent = 0;
        ingredientThreeCurrent = 0;
        ingredientTwoCurrent = 0;
        ingredientOneCurrent = 0;
        badIngredientsCurrent = 0;

        FindObjectOfType<PlayerHUDManager>().SetFoodBars();
    }
    public void AwardFood(EnemyFoodDropper.FoodType type)
    {
        if(type == ingredientOneType)
        {
            if (ingredientOneCurrent == ingredientOneRequired)
                badIngredientsCurrent++;
            else
                ingredientOneCurrent++;
        }
        else if(type == ingredientTwoType)
        {
            if (ingredientTwoCurrent == ingredientTwoRequired)
                badIngredientsCurrent++;
            else
                ingredientTwoCurrent++;
        }
        else if (type == ingredientThreeType)
        {
            if (ingredientThreeCurrent == ingredientThreeRequired)
                badIngredientsCurrent++;
            else
                ingredientThreeCurrent++;
        }
        else if (type == ingredientFourType)
        {
            if (ingredientFourCurrent == ingredientFourRequired)
                badIngredientsCurrent++;
            else
                ingredientFourCurrent++;
        }
        else if (type == ingredientFiveType)
        {
            if (ingredientFiveCurrent == ingredientFiveRequired)
                badIngredientsCurrent++;
            else
                ingredientFiveCurrent++;
        }
        else
        {
            badIngredientsCurrent++;
        }

    }


    public void FinishLevel()
    {
        IronChefUtils.TurnOffCharacter();

        Debug.Log("Level Over!");
        score = 0;
        int numIngredients = 0;
        if(ingredientOneRequired > 0)
        {
            score += 1 - Mathf.Abs(1f - ((float)ingredientOneCurrent / ingredientOneRequired));
            numIngredients++;
        }
        if (ingredientTwoRequired > 0)
        {
            score += 1 - Mathf.Abs(1f - ((float)ingredientTwoCurrent / ingredientTwoRequired));
            numIngredients++;
        }
        if (ingredientThreeRequired > 0)
        {
            score += 1 - Mathf.Abs(1f - ((float)ingredientThreeCurrent / ingredientThreeRequired));
            numIngredients++;
        }
        if (ingredientFourRequired > 0)
        {
            score += 1 - Mathf.Abs(1f - ((float)ingredientFourCurrent / ingredientFourRequired));
            numIngredients++;
        }
        if (ingredientFiveRequired > 0)
        {
            score += 1 - Mathf.Abs(1f - ((float)ingredientFiveCurrent / ingredientFiveRequired));
            numIngredients++;
        }

        if(badIngredientsMaximum > 0)
        {
            //score = score * (1-(0.5f * (float)badIngredientsCurrent / badIngredientsMaximum));

        }

        if(score > 0 && numIngredients > 0)
        {
            score /= numIngredients;
            score *= 100;
            Debug.Log("Deaths: " + ChapterManager.deathsThisLevel);
            for(int i = 0; i < ChapterManager.deathsThisLevel; i++)
            {
                score *= 0.1f;
            }
        }

        Debug.Log("Score: " + score + "/100");

        if(!level.IsTutorial)
        {
            var hud = FindObjectOfType<PlayerHUDManager>();
            if (score >= 50)
            {
                UnlocksManager.UnlockAppliance(level.completionApplianceUnlock.ToString());
                UnlocksManager.UnlockPower(level.completionPowerUnlock.ToString());
                hud.UnlockNotification(level.completionApplianceUnlock.ToString());
                hud.UnlockNotification(level.completionPowerUnlock.ToString());
            }

            if (score >= 90)
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(FindObjectOfType<PlayerAttackController>().transform.position, SoundEffectSpawner.SoundEffect.LevelCom100);
                UnlocksManager.UnlockAppliance(level.perfectionApplianceUnlock.ToString());
                UnlocksManager.UnlockPower(level.perfectionPowerUnlock.ToString());
                hud.UnlockNotification(level.perfectionApplianceUnlock.ToString());
                hud.UnlockNotification(level.perfectionPowerUnlock.ToString());
            }
            else
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(FindObjectOfType<CharacterMover>().transform.position, SoundEffectSpawner.SoundEffect.LevelCom);
            }
        }
        
        
        continueOn();
        
    
    }

    public void continueOn()
    {
        chapters.GoToNextLevel(score);
    }
    


}
