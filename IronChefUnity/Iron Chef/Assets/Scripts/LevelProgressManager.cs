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

    /*
     * Hud things required
     */


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
