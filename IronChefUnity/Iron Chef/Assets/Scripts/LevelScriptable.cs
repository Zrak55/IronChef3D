using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "Chapters/New Level", order = 1)]
public class LevelScriptable : ScriptableObject
{
    public string dish;
    [Space]
    public int ingredientOneRequired;
    public int ingredientTwoRequired;
    public int ingredientThreeRequired;
    public int ingredientFourRequired;
    public int ingredientFiveRequired;
    [Space]
    public EnemyFoodDropper.FoodType ingredientOneType = 0;
    public EnemyFoodDropper.FoodType ingredientTwoType = 0;
    public EnemyFoodDropper.FoodType ingredientThreeType = 0;
    public EnemyFoodDropper.FoodType ingredientFourType = 0;
    public EnemyFoodDropper.FoodType ingredientFiveType = 0;
    [Space]
    public PlayerPowerScriptable.PowerName completionPowerUnlock;
    public PlayerPowerScriptable.PowerName perfectionPowerUnlock;

    public PlayerApplianceScriptable.ApplianceName completionApplianceUnlock;
    public PlayerApplianceScriptable.ApplianceName perfectionApplianceUnlock;
    [Space]
    public bool IsTutorial = false;

}
