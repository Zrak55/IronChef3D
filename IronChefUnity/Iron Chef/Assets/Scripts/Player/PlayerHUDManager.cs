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
    float targetHPValue;

    [Header("Food Bars")]
    public Slider food1bar;
    public Slider food2bar;
    public Slider food3bar;
    public Slider food4bar;
    public Slider food5bar;
    public Slider badfoodbar;
    [Space]
    public Image food1requiredbar;
    public Image food2requiredbar;
    public Image food3requiredbar;
    public Image food4requiredbar;
    public Image food5requiredbar;
    [Space]
    public Text food1ingredient;
    public Text food2ingredient;
    public Text food3ingredient;
    public Text food4ingredient;
    public Text food5ingredient;

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
        food5ingredient.text = foodInfo.ingredientFiveType.ToString();
        if (food5bar.maxValue == 0)
        {
            food5bar.gameObject.SetActive(false);
            food5requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food5bar.transform.position;
        }


        food4bar.maxValue = foodInfo.ingredientFourMaximum;
        food4bar.value = foodInfo.ingredientFourRequired;
        food4requiredbar.transform.position = food4bar.handleRect.transform.position;
        food4ingredient.text = foodInfo.ingredientFourType.ToString();
        if (food4bar.maxValue == 0)
        {
            food4bar.gameObject.SetActive(false);
            food4requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food4bar.transform.position;
        }
        food3bar.maxValue = foodInfo.ingredientThreeMaximum;
        food3bar.value = foodInfo.ingredientThreeRequired;
        food3requiredbar.transform.position = food3bar.handleRect.transform.position;
        food3ingredient.text = foodInfo.ingredientThreeType.ToString();
        if (food3bar.maxValue == 0)
        {
            food3bar.gameObject.SetActive(false);
            food3requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food3bar.transform.position;
        }
        food2bar.maxValue = foodInfo.ingredientTwoMaximum;
        food2bar.value = foodInfo.ingredientTwoRequired;
        food2requiredbar.transform.position = food2bar.handleRect.transform.position;
        food2ingredient.text = foodInfo.ingredientTwoType.ToString();
        if (food2bar.maxValue == 0)
        {
            food2bar.gameObject.SetActive(false);
            food2requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food2bar.transform.position;
        }
        food1bar.maxValue = foodInfo.ingredientOneMaximum;
        food1bar.value = foodInfo.ingredientOneRequired;
        food1requiredbar.transform.position = food1bar.handleRect.transform.position;
        food1ingredient.text = foodInfo.ingredientOneType.ToString();
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
        moveABar(hpBar, stats.CurrentHP / stats.MaximumHP);
    }
    private void UpdateStaminaBar()
    {
        moveABar(staminaBar, stats.CurrentStamina / stats.MaximumStamina);
    }

    private void UpdateFoodBars()
    {
        Color targetcolor;

        moveABar(food1bar, foodInfo.ingredientOneCurrent);
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


        moveABar(food2bar, foodInfo.ingredientTwoCurrent);
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

        moveABar(food3bar, foodInfo.ingredientThreeCurrent);
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

        moveABar(food4bar, foodInfo.ingredientFourCurrent);
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

        moveABar(food5bar, foodInfo.ingredientFiveCurrent);
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

        moveABar(badfoodbar, foodInfo.badIngredientsCurrent);
    }

    public void slowMoveSlider(Slider s, float target)
    {
        StartCoroutine(moveSlider(s, s.value, target));
    }
    
    IEnumerator moveSlider(Slider s, float init, float target)
    {
        float distance = target - init;
        float tickRatePerSecond = distance / 0.5f;
        float t = 0;
        while (t < 0.5f)
        {
            if(target > init)
                s.value = Mathf.Clamp(s.value + (tickRatePerSecond * Time.deltaTime), s.minValue, target);
            else
                s.value = Mathf.Clamp(s.value + (tickRatePerSecond * Time.deltaTime), target, s.maxValue);

            t += Time.deltaTime;
            yield return null;
        }
        s.value = target;
    }


    void moveABar(Slider s, float target)
    {
        if (s.value != target)
        {
            float tickRate = Mathf.Clamp(Mathf.Abs(s.value - target) * Time.deltaTime * 0.5f * (s.maxValue - s.minValue), Time.deltaTime * 2, Mathf.Infinity);
            if (Mathf.Abs(target - s.value) < tickRate)
                s.value = target;
            else
            {
                if (target < s.value)
                    tickRate *= -1;
                s.value = s.value + tickRate;
            }
        }
    }

}
