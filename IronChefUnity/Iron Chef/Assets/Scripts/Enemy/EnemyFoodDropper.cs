using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoodDropper : MonoBehaviour
{
    private LevelProgressManager levelProgress;
    public FoodType food;


    // Start is called before the first frame update
    void Start()
    {
        levelProgress = FindObjectOfType<LevelProgressManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GiveFood()
    {
        levelProgress.AwardFood(food);
    }





    public enum FoodType
    {
        None,
        Bread,
        Cheese,
        Tomato,
        Egg,
        Grits,
        Bacon,
        Meat,
        Beans,
        Potato,
        Chips
    }
}
