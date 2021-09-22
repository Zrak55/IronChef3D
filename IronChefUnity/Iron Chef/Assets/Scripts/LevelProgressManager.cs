using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    /*
     * Hud things required
     */


    // Start is called before the first frame update
    void Start()
    {
        GetMaxes();
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

        /*
         * TODO: Update sliders
         */
    }

    public void FinishLevel()
    {

        Debug.Log("Level Over!");
        float score = 0;
        int numIngredients = 0;
        if(ingredientOneRequired > 0)
        {
            score += 1 - Mathf.Abs(1 - (ingredientOneCurrent / ingredientOneRequired));
            numIngredients++;
        }
        if (ingredientTwoRequired > 0)
        {
            score += 1 - Mathf.Abs(1 - (ingredientTwoCurrent / ingredientTwoRequired));
            numIngredients++;
        }
        if (ingredientThreeRequired > 0)
        {
            score += 1 - Mathf.Abs(1 - (ingredientThreeCurrent / ingredientThreeRequired));
            numIngredients++;
        }
        if (ingredientFourRequired > 0)
        {
            score += 1 - Mathf.Abs(1 - (ingredientFourCurrent / ingredientFourRequired));
            numIngredients++;
        }
        if (ingredientFiveRequired > 0)
        {
            score += 1 - Mathf.Abs(1 - (ingredientFiveCurrent / ingredientFiveRequired));
            numIngredients++;
        }

        if(badIngredientsMaximum > 0)
        {
            score = score * (0.5f * badIngredientsCurrent / badIngredientsMaximum);

        }

        if(score > 0 && numIngredients > 0)
        {
            score /= numIngredients;
            score *= 100;
        }

        Debug.Log("Score: " + score + "/100");


        //TODO: Bring up win/score screen
    }
}
