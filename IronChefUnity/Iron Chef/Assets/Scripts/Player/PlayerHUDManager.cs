using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LevelProgressManager foodInfo;
    [SerializeField] private PlayerFoodEater eatFood;
    [SerializeField] private PlayerCostCooldownManager cooldowns;

    [Header("Bars")]
    public Slider hpBar;
    public Slider staminaBar;
    public Slider hungryMeter;
    public Image foodHighlight;
    float targetHPValue;

    [Header("Food Bars")]
    public Slider food1bar;
    public Slider food2bar;
    public Slider food3bar;
    public Slider food4bar;
    public Slider food5bar;
    public Slider badfoodbar;
    public Image food1barImage;
    public Image food2barImage;
    public Image food3barImage;
    public Image food4barImage;
    public Image food5barImage;
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
    [Header("Weapon/Cooldowns")]
    public Image WeaponImage;
    public Sprite[] weaponImages;
    public Image powerImage;
    [Space]
    public Sprite MalapenoImage;
    public Sprite BreadTrapImage;
    public Sprite SpearOfDesticheeseImage;
    public Sprite PortableLunchImage;
    public Sprite HamMerImage;
    public Sprite _50CheeseImage;
    public Sprite CatapastaImage;
    public Sprite IceTrayImage;
    public Sprite SugarRushImage;
    public Sprite GlockamoleImage;
    [Space]
    public Slider FryingPanCD;
    public Slider PowerCD;
    [Header("Boss")]
    public Slider BossHP;
    public Text BossName;
    public Text BossTip;
    public Image BossTipBackground;
    EnemyHitpoints LevelBossHP;
    bool fadeDelay = false;


    [Header("FoodBarSprites")]
    public Sprite cheese;
    public Sprite egg;
    public Sprite tomato;
    public Sprite bacon;
    public Sprite bread;
    public Sprite potato;
    public Sprite meat;


    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
        foodInfo = FindObjectOfType<LevelProgressManager>();
        eatFood = FindObjectOfType<PlayerFoodEater>();
        cooldowns = FindObjectOfType<PlayerCostCooldownManager>();

        SetHungryBar();

        InvokeRepeating("BossTipFade", 0, 0.05f);
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();
    }
    public void SetHungryBar()
    {
        hungryMeter.value = 0;
        hungryMeter.maxValue = eatFood.maxAllowedEaten;
    }
    public void SetFoodBars()
    {
        float height = 110;

        food5bar.maxValue = foodInfo.ingredientFiveMaximum;
        food5bar.value = foodInfo.ingredientFiveRequired;
        food5requiredbar.transform.position = food5bar.handleRect.transform.position;
        food5ingredient.text = foodInfo.ingredientFiveType.ToString();
        SetFoodBarImage(food5barImage, foodInfo.ingredientFiveType, height);
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
        SetFoodBarImage(food4barImage, foodInfo.ingredientFourType, height);
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
        SetFoodBarImage(food3barImage, foodInfo.ingredientThreeType, height);
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
        SetFoodBarImage(food2barImage, foodInfo.ingredientTwoType, height);
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
        SetFoodBarImage(food1barImage, foodInfo.ingredientOneType, height);
        if (food1bar.maxValue == 0)
        {
            food1bar.gameObject.SetActive(false);
            food1requiredbar.gameObject.SetActive(false);
            badfoodbar.transform.position = food1bar.transform.position;
        }
        badfoodbar.maxValue = foodInfo.badIngredientsMaximum;
    }


    void SetFoodBarImage(Image i, EnemyFoodDropper.FoodType food, float targetHeight)
    {
        switch(food)
        {
            case EnemyFoodDropper.FoodType.Bacon:
                i.sprite = bacon;
                break;
            case EnemyFoodDropper.FoodType.Bread:
                i.sprite = bread;
                break;
            case EnemyFoodDropper.FoodType.Cheese:
                i.sprite = cheese;
                break;
            case EnemyFoodDropper.FoodType.Egg:
                i.sprite = egg;
                break;
            case EnemyFoodDropper.FoodType.Tomato:
                i.sprite = tomato;
                break;
            case EnemyFoodDropper.FoodType.Potato:
                i.sprite = potato;
                break;
            case EnemyFoodDropper.FoodType.Meat:
                i.sprite = meat;
                break;
            default:
                break;
        }

        i.SetNativeSize();
        float ratio = targetHeight / i.rectTransform.rect.height;
        i.rectTransform.sizeDelta *= ratio;
    }
    private void UpdateBars()
    {
        UpdateHPBar();
        UpdateStaminaBar();
        UpdateFoodBars();
        UpdateHungryBar();
        UpdateCDBars();
        UpdateBossHPBar();
    }

    private void UpdateBossHPBar()
    {
        if(LevelBossHP != null)
        {
            IronChefUtils.moveABar(BossHP, LevelBossHP.GetPercentHP());
        }
    }

    private void UpdateCDBars()
    {
        IronChefUtils.moveABar(FryingPanCD, 1 - cooldowns.GetFryingPanCDPercent());
        IronChefUtils.moveABar(PowerCD, 1 - cooldowns.GetPowerCDPercent());

    }

    private void UpdateHungryBar()
    {
        IronChefUtils.moveABar(hungryMeter, eatFood.amountEaten);
        Vector3 targetPos = foodHighlight.transform.position;
        if(eatFood.currentEatIndex == 1)
        {
            targetPos = food1bar.transform.position;
        }
        else if (eatFood.currentEatIndex == 2)
        {
            targetPos = food2bar.transform.position;
        }
        else if (eatFood.currentEatIndex == 3)
        {
            targetPos = food3bar.transform.position;
        }
        else if (eatFood.currentEatIndex == 4)
        {
            targetPos = food4bar.transform.position;
        }
        else if (eatFood.currentEatIndex == 5)
        {
            targetPos = food5bar.transform.position;
        }
        else if (eatFood.currentEatIndex == 6)
        {
            targetPos = badfoodbar.transform.position;
        }
        foodHighlight.transform.position = targetPos;
    }
    private void UpdateHPBar()
    {
        IronChefUtils.moveABar(hpBar, stats.CurrentHP / stats.MaximumHP);
    }
    private void UpdateStaminaBar()
    {
        IronChefUtils.moveABar(staminaBar, stats.CurrentStamina / stats.MaximumStamina);
    }

    private void UpdateFoodBars()
    {
        Color targetcolor;

        IronChefUtils.moveABar(food1bar, foodInfo.ingredientOneCurrent);
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


        IronChefUtils.moveABar(food2bar, foodInfo.ingredientTwoCurrent);
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

        IronChefUtils.moveABar(food3bar, foodInfo.ingredientThreeCurrent);
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

        IronChefUtils.moveABar(food4bar, foodInfo.ingredientFourCurrent);
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

        IronChefUtils.moveABar(food5bar, foodInfo.ingredientFiveCurrent);
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

        IronChefUtils.moveABar(badfoodbar, foodInfo.badIngredientsCurrent);
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


    

    public void SetWeaponImage(int i)
    {
        WeaponImage.sprite = weaponImages[i];
    }

    public void SetPowerImage(PlayerPowerScriptable.PowerName power)
    {
        switch(power)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                powerImage.sprite = MalapenoImage;
                break;
            case PlayerPowerScriptable.PowerName.BreadTrap:
                powerImage.sprite = BreadTrapImage;
                break;
            case PlayerPowerScriptable.PowerName.SpearOfDesticheese:
                powerImage.sprite = SpearOfDesticheeseImage;
                break;
            case PlayerPowerScriptable.PowerName.PortableLunch:
                powerImage.sprite = PortableLunchImage;
                break;
            case PlayerPowerScriptable.PowerName.Ham_mer:
                powerImage.sprite = HamMerImage;
                break;
            case PlayerPowerScriptable.PowerName._50CheeseStrike:
                powerImage.sprite = _50CheeseImage;
                break;
            case PlayerPowerScriptable.PowerName.Catapasta:
                powerImage.sprite = CatapastaImage;
                break;
            case PlayerPowerScriptable.PowerName.IceTray:
                powerImage.sprite = IceTrayImage;
                break;
            case PlayerPowerScriptable.PowerName.SugarRush:
                powerImage.sprite = SugarRushImage;
                break;
            case PlayerPowerScriptable.PowerName.Glockamole:
                powerImage.sprite = GlockamoleImage;
                break;
        }
    }


    public void BossInfoOn(string bossName, EnemyHitpoints bossHP, string bossTip)
    {
        BossName.gameObject.SetActive(true);
        BossTip.gameObject.SetActive(true);
        BossHP.gameObject.SetActive(true);
        BossTipBackground.gameObject.SetActive(true);

        BossName.text = bossName;
        LevelBossHP = bossHP;
        BossTip.text = bossTip;
        if(BossTip.text == "")
        {
            BossTip.color = new Color(BossTip.color.r, BossTip.color.g, BossTip.color.b, 0);
            BossTipBackground.color = new Color(BossTipBackground.color.r, BossTipBackground.color.g, BossTipBackground.color.b, 0);
        }
    }

    public void SetBossTip(string s)
    {
        BossTip.color = new Color(BossTip.color.r, BossTip.color.g, BossTip.color.b, 1);
        BossTip.text = s;
        fadeDelay = true;
        Invoke("UndoFadeDelay", 4f);
    }

    void BossTipFade()
    {
        if(!fadeDelay)
            BossTip.color = new Color(BossTip.color.r, BossTip.color.g, BossTip.color.b, Mathf.Max(BossTip.color.a - 0.05f, 0));
        BossTipBackground.color = new Color(BossTipBackground.color.r, BossTipBackground.color.g, BossTipBackground.color.b, Mathf.Max(BossTip.color.a - 0.05f, 0));

    }
    void UndoFadeDelay()
    {
        fadeDelay = false;
    }

    public void BossOver()
    {
        BossName.gameObject.SetActive(false);
        BossTip.gameObject.SetActive(false);
        BossHP.gameObject.SetActive(false);
    }

    public void PlayFullAnim()
    {
        hungryMeter.GetComponent<Animator>().SetTrigger("Full");
        
    }
}
