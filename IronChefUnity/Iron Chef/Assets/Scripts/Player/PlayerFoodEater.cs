using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFoodEater : MonoBehaviour
{
    public int currentEatIndex;
    LevelProgressManager food;
    PlayerCostCooldownManager cooldown;
    PlayerHitpoints health;
    PlayerHUDManager hud;
    float eatCD = 0;
    public int amountEaten = 0;
    public int maxAllowedEaten;
    float eatDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
        food = FindObjectOfType<LevelProgressManager>();
        cooldown = GetComponent<PlayerCostCooldownManager>();
        health = GetComponent<PlayerHitpoints>();
        hud = FindObjectOfType<PlayerHUDManager>();
        currentEatIndex = 1;
    }

    // Update is called once per frame
    void Update()
    {
        TryChangeEatTarget();
        TryEat();

    }

    void TryEat()
    {
        if (InputControls.controls.Gameplay.Eat.triggered && cooldown.EatOnCD == false)
        {
            bool ate = false;
            bool full = false;
            if(currentEatIndex == 1)
            {
                if(food.ingredientOneCurrent > 0 && (maxAllowedEaten - amountEaten) >= 1)
                {
                    food.ingredientOneCurrent--;
                    amountEaten++;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 1)
                {
                    full = true;
                }
            }
            if (currentEatIndex == 2)
            {
                if (food.ingredientTwoCurrent > 0 && (maxAllowedEaten - amountEaten) >= 1)
                {
                    food.ingredientTwoCurrent--;
                    amountEaten++;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 1)
                {
                    full = true;
                }
            }
            if (currentEatIndex == 3)
            {
                if (food.ingredientThreeCurrent > 0 && (maxAllowedEaten - amountEaten) >= 1)
                {
                    food.ingredientThreeCurrent--;
                    amountEaten++;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 1)
                {
                    full = true;
                }
            }
            if (currentEatIndex == 4)
            {
                if (food.ingredientFourCurrent > 0 && (maxAllowedEaten - amountEaten) >= 1)
                {
                    food.ingredientFourCurrent--;
                    amountEaten++;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 1)
                {
                    full = true;
                }
            }
            if (currentEatIndex == 5)
            {
                if (food.ingredientFiveCurrent > 0 && (maxAllowedEaten - amountEaten) >= 1)
                {
                    food.ingredientFiveCurrent--;
                    amountEaten++;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 1)
                {
                    full = true;
                }
            }
            if (currentEatIndex == 6)
            {
                if (food.badIngredientsCurrent > 0 && (maxAllowedEaten - amountEaten) >= 2)
                {
                    food.badIngredientsCurrent--;
                    amountEaten += 2;
                    ate = true;
                }

                else if ((maxAllowedEaten - amountEaten) < 2)
                {
                    full = true;
                }
            }

            if (ate)
            {
                RestoreHealth();
                cooldown.SetEatCD();
            }
            else if(full)
            {
                hud.PlayFullAnim();
            }
        }
    }

    void RestoreHealth()
    {
        health.RestoreHP(20);
    }

    void TryChangeEatTarget()
    {
        eatDelay -= Time.deltaTime;

        if(InputControls.controls.Gameplay.ChangeEatTrigger.triggered)
        {
            float value = InputControls.controls.Gameplay.ChangeEat.ReadValue<float>();

            if (value != 0)
            {
                if (value > 0)
                {
                    currentEatIndex++;
                }
                else if (value < 0)
                {
                    currentEatIndex--;
                }
            }
            bool swapped = true;
            while (swapped)
            {
                int old = currentEatIndex;
                if (currentEatIndex == 2 && food.ingredientTwoRequired == 0)
                {
                    currentEatIndex += (int)value;
                }
                if (currentEatIndex == 3 && food.ingredientThreeRequired == 0)
                {
                    currentEatIndex += (int)value;
                }
                if (currentEatIndex == 4 && food.ingredientFourRequired == 0)
                {
                    currentEatIndex += (int)value;
                }
                if (currentEatIndex == 5 && food.ingredientFiveRequired == 0)
                {
                    currentEatIndex += (int)value;
                }
                swapped = old != currentEatIndex;
            }
            

            if (currentEatIndex == 7)
            {
                currentEatIndex = 1;
            }
            else if (currentEatIndex == 0)
            {
                currentEatIndex = 6;
            }

        }
        
    }

    public void resetEat()
    {
        amountEaten = 0;
    }
}
