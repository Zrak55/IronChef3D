using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerHUDManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private PlayerStats stats;
    [SerializeField] private LevelProgressManager foodInfo;
    [SerializeField] private PlayerFoodEater eatFood;
    [SerializeField] private PlayerCostCooldownManager cooldowns;


    private static PlayerHUDManager _playerHud;
    public static PlayerHUDManager PlayerHud
    {
        get
        {
            if (_playerHud == null)
                _playerHud = FindObjectOfType<PlayerHUDManager>();
            return _playerHud;
        }
        set
        {
            _playerHud = value;
        }
    }

    [Header("Bars")]
    public Slider hpBar;
    public Slider staminaBar;
    public Slider hungryMeter;
    public Image foodHighlight;
    public Slider KnifeDamageScalarBar;
    public Text KnifeScalarNumber;
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
    public List<Slider> spareSliders;


    [Header("FoodBarSprites")]
    public Sprite cheese;
    public Sprite egg;
    public Sprite tomato;
    public Sprite bacon;
    public Sprite bread;
    public Sprite potato;
    public Sprite meat;
    public Sprite ravioli;
    public Sprite icecream;
    public Sprite sprikle;
    public Sprite fudge;
    public Sprite gummy;
    public Sprite cookie;


    [Header("Location Name")]
    public Text locName;

    [Space]
    float unlockTimer = 0;
    public GameObject unlockNotification;
    public Text unlockText;

    float updateFoodBarTimer = 0;

    [Header("Control texts")]
    [SerializeField] private Text weaponSwap;
    [SerializeField] private Text melee;
    [SerializeField] private Text eat;
    [SerializeField] private Text fryingpan;
    [SerializeField] private Text power;

    private void Awake()
    {
        stats = FindObjectOfType<PlayerStats>();
        foodInfo = FindObjectOfType<LevelProgressManager>();
        eatFood = FindObjectOfType<PlayerFoodEater>();
        cooldowns = FindObjectOfType<PlayerCostCooldownManager>();

        SetHungryBar();

        InvokeRepeating("BossTipFade", 0, 0.05f);

        InvokeRepeating("UpdateControlTexts", 0, 1f);

        PlayerHud = this;

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateBars();

        if(unlockTimer > 0)
        {

            unlockTimer -= Time.deltaTime;
            if(unlockTimer <= 0)
            {
                unlockText.text = "";
                unlockNotification.SetActive(false);
            }

        }
    }

    private void UpdateControlTexts()
    {
        if(Gamepad.current != null)
        {
            weaponSwap.text = "Y";
            melee.text = "RT";
            eat.text = "X";
            fryingpan.text = "LT";
            power.text = "RB";
        }
        else
        {
            weaponSwap.text = "MW";
            melee.text = "LMB";
            eat.text = "E";
            fryingpan.text = "RMB";
            power.text = "Q";
        }
    }

    public void DisplayLocationName(string lName)
    {
        if(locName.text != lName)
        {
            locName.text = lName;
            StartCoroutine(FadeLocName());
        }
    }
    IEnumerator FadeLocName()
    {
        while(locName.color.a < 1)
        {
            locName.color = new Color(locName.color.r, locName.color.g, locName.color.b, Mathf.Min(1, locName.color.a + (Time.deltaTime / 2)));
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while (locName.color.a > 0)
        {
            locName.color = new Color(locName.color.r, locName.color.g, locName.color.b, Mathf.Max(0, locName.color.a - (Time.deltaTime / 2)));
            yield return null;
        }
    }


    public void SetHungryBar()
    {
        hungryMeter.value = 0;
        hungryMeter.maxValue = eatFood.maxAllowedEaten;
    }
    public void SetFoodBars()
    {
        float height = 110;
        updateFoodBarTimer = 3f;

        food1bar.gameObject.SetActive(true);
        food2bar.gameObject.SetActive(true);
        food3bar.gameObject.SetActive(true);
        food4bar.gameObject.SetActive(true);
        food5bar.gameObject.SetActive(true);
        food1requiredbar.gameObject.SetActive(true);
        food2requiredbar.gameObject.SetActive(true);
        food3requiredbar.gameObject.SetActive(true);
        food4requiredbar.gameObject.SetActive(true);
        food5requiredbar.gameObject.SetActive(true);


        food5bar.maxValue = foodInfo.ingredientFiveRequired;

        //food5bar.value = foodInfo.ingredientFiveRequired;
        food5bar.value = food5bar.maxValue;        

        food5requiredbar.transform.position = food5bar.handleRect.transform.position;
        food5ingredient.text = foodInfo.ingredientFiveType.ToString();
        SetFoodBarImage(food5barImage, foodInfo.ingredientFiveType, height);
        if (food5bar.maxValue == 0)
        {
            food5bar.gameObject.SetActive(false);
            food5requiredbar.gameObject.SetActive(false);
        }


        food4bar.maxValue = foodInfo.ingredientFourRequired;

        //food4bar.value = foodInfo.ingredientFourRequired;
        food4bar.value = food4bar.maxValue;

        food4requiredbar.transform.position = food4bar.handleRect.transform.position;
        food4ingredient.text = foodInfo.ingredientFourType.ToString();
        SetFoodBarImage(food4barImage, foodInfo.ingredientFourType, height);
        if (food4bar.maxValue == 0)
        {
            food4bar.gameObject.SetActive(false);
            food4requiredbar.gameObject.SetActive(false);
        }

        food3bar.maxValue = foodInfo.ingredientThreeRequired;

        // food3bar.value = foodInfo.ingredientThreeRequired;
        food3bar.value = food3bar.maxValue;

        food3requiredbar.transform.position = food3bar.handleRect.transform.position;
        food3ingredient.text = foodInfo.ingredientThreeType.ToString();
        SetFoodBarImage(food3barImage, foodInfo.ingredientThreeType, height);
        if (food3bar.maxValue == 0)
        {
            food3bar.gameObject.SetActive(false);
            food3requiredbar.gameObject.SetActive(false);
        }

        food2bar.maxValue = foodInfo.ingredientTwoRequired;

        //food2bar.value = foodInfo.ingredientTwoRequired;
        food2bar.value = food2bar.maxValue;

        food2requiredbar.transform.position = food2bar.handleRect.transform.position;
        food2ingredient.text = foodInfo.ingredientTwoType.ToString();
        SetFoodBarImage(food2barImage, foodInfo.ingredientTwoType, height);
        if (food2bar.maxValue == 0)
        {
            food2bar.gameObject.SetActive(false);
            food2requiredbar.gameObject.SetActive(false);
        }

        food1bar.maxValue = foodInfo.ingredientOneRequired;

        //food1bar.value = foodInfo.ingredientOneRequired;
        food1bar.value = food1bar.maxValue;

        food1requiredbar.transform.position = food1bar.handleRect.transform.position;
        food1ingredient.text = foodInfo.ingredientOneType.ToString();
        SetFoodBarImage(food1barImage, foodInfo.ingredientOneType, height);
        if (food1bar.maxValue == 0)
        {
            food1bar.gameObject.SetActive(false);
            food1requiredbar.gameObject.SetActive(false);
        }

        badfoodbar.maxValue = foodInfo.badIngredientsMaximum + (foodInfo.ingredientOneMaximum - foodInfo.ingredientOneRequired) + (foodInfo.ingredientTwoMaximum - foodInfo.ingredientTwoRequired) + (foodInfo.ingredientThreeMaximum - foodInfo.ingredientThreeRequired) + (foodInfo.ingredientFourMaximum - foodInfo.ingredientFourRequired) + (foodInfo.ingredientFiveMaximum - foodInfo.ingredientFiveRequired);
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
            case EnemyFoodDropper.FoodType.Ravioli:
                i.sprite = ravioli;
                break;
            case EnemyFoodDropper.FoodType.Gummy:
                i.sprite = gummy;
                break;
            case EnemyFoodDropper.FoodType.IceCream:
                i.sprite = icecream;
                break;
            case EnemyFoodDropper.FoodType.Sprinkles:
                i.sprite = sprikle;
                break;
            case EnemyFoodDropper.FoodType.Cookies:
                i.sprite = cookie;
                break;
            case EnemyFoodDropper.FoodType.Fudge:
                i.sprite = fudge;
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

    public void UpdateScalarBar(float percentValue)
    {
        KnifeDamageScalarBar.value = percentValue;
    }
    public void ShowScalarBar()
    {
        if(KnifeDamageScalarBar.gameObject.activeSelf == false)
            KnifeDamageScalarBar.gameObject.SetActive(true);
    }
    public void HideScalarBar()
    {
        if (KnifeDamageScalarBar.gameObject.activeSelf == true)
            KnifeDamageScalarBar.gameObject.SetActive(false);
    }

    public void FoodParticleHit()
    {
        updateFoodBarTimer = 1;
    }

    public void UpdateScalarText(float percent)
    {
        KnifeScalarNumber.text = percent.ToString("0.0") + "x";
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
        if(updateFoodBarTimer > 0)
        {
            Color targetcolor;

            IronChefUtils.moveABar(food1bar, foodInfo.ingredientOneCurrent);
            if (foodInfo.ingredientOneCurrent > foodInfo.ingredientOneRequired)
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
            targetcolor.a = (200f / 255f);
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
            targetcolor.a = (200f / 255f);
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
            targetcolor.a = (200f / 255f);
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
            targetcolor.a = (200f / 255f);
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
            targetcolor.a = (200f / 255f);
            foreach (var i in food5bar.GetComponentsInChildren<Image>())
            {
                if (i.gameObject.name == "Fill")
                    i.color = targetcolor;
            }

            IronChefUtils.moveABar(badfoodbar, foodInfo.badIngredientsCurrent);
        }

        updateFoodBarTimer -= Time.deltaTime;
        
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

    public Sprite GetPowerImage(PlayerPowerScriptable.PowerName power)
    {
        Sprite s = null;
        switch (power)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                s = MalapenoImage;
                break;
            case PlayerPowerScriptable.PowerName.BreadTrap:
                s = BreadTrapImage;
                break;
            case PlayerPowerScriptable.PowerName.SpearOfDesticheese:
                s = SpearOfDesticheeseImage;
                break;
            case PlayerPowerScriptable.PowerName.PortableLunch:
                s = PortableLunchImage;
                break;
            case PlayerPowerScriptable.PowerName.Ham_mer:
                s = HamMerImage;
                break;
            case PlayerPowerScriptable.PowerName._50CheeseStrike:
                s = _50CheeseImage;
                break;
            case PlayerPowerScriptable.PowerName.Catapasta:
                s = CatapastaImage;
                break;
            case PlayerPowerScriptable.PowerName.IceTray:
                s = IceTrayImage;
                break;
            case PlayerPowerScriptable.PowerName.SugarRush:
                s = SugarRushImage;
                break;
            case PlayerPowerScriptable.PowerName.Glockamole:
                s = GlockamoleImage;
                break;
        }
        return s;
    }

    public Sprite GetApplianceImage(PlayerApplianceScriptable.ApplianceName appliance)
    {
        Sprite s = null;

        //TODO: Get appliane images

        return s;
    }

    public void SetPowerImage(PlayerPowerScriptable.PowerName power)
    {
        powerImage.sprite = GetPowerImage(power);

        
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

    public void UnlockNotification(string name)
    {
        unlockNotification.SetActive(true);
        unlockTimer = 3f;
        unlockText.text += name + " unlocked!\n";
    }
}
