using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelProgressManager : MonoBehaviour
{
    public int ingredientOneRequired;
    public int ingredientTwoRequired;
    public int ingredientThreeRequired;
    public int ingredientFourRequired;
    public int ingredientFiveRequired;
    [Space]
    public int ingredientOneMaximum;
    public int ingredientTwoMaximum;
    public int ingredientThreeMaximum;
    public int ingredientFourMaximum;
    public int ingredientFiveMaximum;
    [Space]
    public int ingredientOneCurrent = 0;
    public int ingredientTwoCurrent = 0;
    public int ingredientThreeCurrent = 0;
    public int ingredientFourCurrent = 0;
    public int ingredientFiveCurrent = 0;
    [Space]
    public EnemyFoodDropper.FoodType ingredientOneType = 0;
    public EnemyFoodDropper.FoodType ingredientTwoType = 0;
    public EnemyFoodDropper.FoodType ingredientThreeType = 0;
    public EnemyFoodDropper.FoodType ingredientFourType = 0;
    public EnemyFoodDropper.FoodType ingredientFiveType = 0;


    public int badIngredientsCurrent = 0;
    public int badIngredientsMaximum;

    [Header("Win/Lose Screen")]
    public GameObject LoseScreen;
    public GameObject WinScreen;
    public Text WinStatusText;
    public Text ScoreText;
    public Button continueButton;


    // Start is called before the first frame update
    void Start()
    {
        GetMaxes();


        CompleteSetup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetMaxes()
    {
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
        FindObjectOfType<PlayerHUDManager>().SetFoodBars();
    }
    public void AwardFood(EnemyFoodDropper.FoodType type)
    {
        if(type == ingredientOneType)
        {
            ingredientOneCurrent++;
        }
        else if(type == ingredientTwoType)
        {
            ingredientTwoCurrent++;
        }
        else if (type == ingredientThreeType)
        {
            ingredientThreeCurrent++;
        }
        else if (type == ingredientFourType)
        {
            ingredientFourCurrent++;
        }
        else if (type == ingredientFiveType)
        {
            ingredientFiveCurrent++;
        }
        else
        {
            badIngredientsCurrent++;
        }

    }

    public void FinishLevel()
    {
        FindObjectOfType<CharacterMover>().enabled = false;
        FindObjectOfType<PlayerCameraSetup>().CanMoveCam = false;

        IronChefUtils.ShowMouse();

        Debug.Log("Level Over!");
        float score = 0;
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
            score = score * (0.5f * (float)badIngredientsCurrent / badIngredientsMaximum);

        }

        if(score > 0 && numIngredients > 0)
        {
            score /= numIngredients;
            score *= 100;
        }

        Debug.Log("Score: " + score + "/100");


        ShowWinScreen(score);
        
    
    }

    public void continueOn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowLoseScreen()
    {
        LoseScreen.SetActive(true);
    }
    public void ShowWinScreen(float score)
    {
        WinScreen.SetActive(true);

        ScoreText.text = "Your score: " + ((int)score).ToString() + "/100";
        int stars = 0;
        if (score < 50)
        {
            WinStatusText.text = "Your dish needs work...";
            continueButton.gameObject.SetActive(false);
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


}
