using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LevelProgressManager foodInfo;

    public Slider hpBar;
    public Slider staminaBar;

    [Space]
    public Slider food1bar;
    public Slider food2bar;
    public Slider food3bar;
    public Slider food4bar;
    public Slider food5bar;
    public Slider badfoodbar;

    public Image food1requiredbar;
    public Image food2requiredbar;
    public Image food3requiredbar;
    public Image food4requiredbar;
    public Image food5requiredbar;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
        foodInfo = FindObjectOfType<LevelProgressManager>();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();
    }

    public void SetFoodBars()
    {

        food5bar.maxValue = foodInfo.ingredientFiveMaximum;
        food5bar.value = foodInfo.ingredientFiveRequired;
        food5requiredbar.transform.position = food5bar.handleRect.transform.position;
        if (food5bar.maxValue == 0)
        {
            food5bar.gameObject.SetActive(false);
            food5requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food5bar.transform.position;
        }


        food4bar.maxValue = foodInfo.ingredientFourMaximum;
        food4bar.value = foodInfo.ingredientFourRequired;
        food4requiredbar.transform.position = food4bar.handleRect.transform.position; 
        if (food4bar.maxValue == 0)
        {
            food4bar.gameObject.SetActive(false);
            food4requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food4bar.transform.position;
        }
        food3bar.maxValue = foodInfo.ingredientThreeMaximum;
        food3bar.value = foodInfo.ingredientThreeRequired;
        food3requiredbar.transform.position = food3bar.handleRect.transform.position;
        if (food3bar.maxValue == 0)
        {
            food3bar.gameObject.SetActive(false);
            food3requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food3bar.transform.position;
        }
        food2bar.maxValue = foodInfo.ingredientTwoMaximum;
        food2bar.value = foodInfo.ingredientTwoRequired;
        food2requiredbar.transform.position = food2bar.handleRect.transform.position;
        if (food2bar.maxValue == 0)
        {
            food2bar.gameObject.SetActive(false);
            food2requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food2bar.transform.position;
        }
        food1bar.maxValue = foodInfo.ingredientOneMaximum;
        food1bar.value = foodInfo.ingredientOneRequired;
        food1requiredbar.transform.position = food1bar.handleRect.transform.position;
        if (food1bar.maxValue == 0)
        {
            food1bar.gameObject.SetActive(false);
            food1requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food1bar.transform.position;
        }
        badfoodbar.maxValue = foodInfo.badIngredientsMaximum;
    }

    private void UpdateBars()
    {
        UpdateHPBar();
        UpdateStaminaBar();
        UpdateFoodBars();
    }

    private void UpdateHPBar()
    {
        hpBar.value = stats.CurrentHP / stats.MaximumHP;
    }
    private void UpdateStaminaBar()
    {
        staminaBar.value = stats.CurrentStamina / stats.MaximumStamina;
    }

    private void UpdateFoodBars()
    {
        Color targetcolor;

        food1bar.value = foodInfo.ingredientOneCurrent;
        if(foodInfo.ingredientOneCurrent > foodInfo.ingredientOneRequired)
        {
            targetcolor = Color.red;
            
        }
        else if (foodInfo.ingredientOneCurrent == foodInfo.ingredientOneRequired)
        {
            targetcolor = Color.green;
        }
        else
        {
            targetcolor = Color.blue;
        }
        foreach (var i in food1bar.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "Fill")
                i.color = targetcolor;
        }


        food2bar.value = foodInfo.ingredientTwoCurrent;
        if (foodInfo.ingredientTwoCurrent > foodInfo.ingredientTwoRequired)
        {
            targetcolor = Color.red;

        }
        else if (foodInfo.ingredientTwoCurrent == foodInfo.ingredientTwoRequired)
        {
            targetcolor = Color.green;
        }
        else
        {
            targetcolor = Color.blue;
        }
        foreach (var i in food2bar.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "Fill")
                i.color = targetcolor;
        }

        food3bar.value = foodInfo.ingredientThreeCurrent;
        if (foodInfo.ingredientThreeCurrent > foodInfo.ingredientThreeRequired)
        {
            targetcolor = Color.red;

        }
        else if (foodInfo.ingredientThreeCurrent == foodInfo.ingredientThreeRequired)
        {
            targetcolor = Color.green;
        }
        else
        {
            targetcolor = Color.blue;
        }
        foreach (var i in food3bar.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "Fill")
                i.color = targetcolor;
        }

        food4bar.value = foodInfo.ingredientFourCurrent;
        if (foodInfo.ingredientFourCurrent > foodInfo.ingredientFourRequired)
        {
            targetcolor = Color.red;

        }
        else if (foodInfo.ingredientFourCurrent == foodInfo.ingredientFourRequired)
        {
            targetcolor = Color.green;
        }
        else
        {
            targetcolor = Color.blue;
        }
        foreach (var i in food4bar.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "Fill")
                i.color = targetcolor;
        }

        food5bar.value = foodInfo.ingredientFiveCurrent;
        if (foodInfo.ingredientFiveCurrent > foodInfo.ingredientFiveRequired)
        {
            targetcolor = Color.red;

        }
        else if (foodInfo.ingredientFiveCurrent == foodInfo.ingredientFiveRequired)
        {
            targetcolor = Color.green;
        }
        else
        {
            targetcolor = Color.blue;
        }
        foreach (var i in food5bar.GetComponentsInChildren<Image>())
        {
            if (i.gameObject.name == "Fill")
                i.color = targetcolor;
        }

    }

}
