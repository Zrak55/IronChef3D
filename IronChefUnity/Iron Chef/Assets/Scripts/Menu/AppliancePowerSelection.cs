using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppliancePowerSelection : MonoBehaviour
{
    public GameObject player;
    public GameObject playerCam;

    public Text PowerName;
    public Text ApplianceName;
    public Text PowerDesc;
    public Text ApplianceDesc;

    [Space]
    public GameObject firstSelectButton;
    public PowerButton firstPowerButton;
    public ApplianceButton firstApplianceButton;

    bool firstOn = true;

    const string PowerPref = "SelectedPower";
    const string AppliancePref = "SelectedAppliance";

    public AppliancePowerPageManager powerPages;
    public AppliancePowerPageManager appliancePages;

    public GameObject PlayerHud;

    PlayerHUDManager hudScript;


    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectButton);

    }
    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectButton);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMover>().gameObject;
        playerCam = FindObjectOfType<PlayerCameraSetup>().gameObject;
        IronChefUtils.TurnOffCharacter();

        hudScript = PlayerHud.GetComponent<PlayerHUDManager>();

        if(firstOn)
        {
            //firstPowerButton.SelectPower();
            //firstApplianceButton.SelectAppliance();

            string power = PlayerPrefs.GetString(PowerPref, "");
            string appliance = PlayerPrefs.GetString(AppliancePref, "");

            if (power == "")
                power = "Molapeno";
            if (appliance == "")
                appliance = "Fridge";

            powerPages.StartThings();
            string p = "";
            while(p != power)
            {
                powerPages.LoadNextPage(false);
                p = powerPages.LastPowerButtonName();
            }

            appliancePages.StartThings();
            string a = "";
            while (a != appliance)
            {
               appliancePages.LoadNextPage(false);
               a = appliancePages.LastApplianceButtonName();
            }

            firstOn = false;
        }



    }

    // Update is called once per frame
    void Update()
    {
        PlayerHud.SetActive(false);
        PowerName.text = player.GetComponent<PlayerPower>().powerInformation.DisplayName;
        ApplianceName.text = player.GetComponent<Appliance>().applianceScriptable.DisplayName;
        PowerDesc.text = player.GetComponent<PlayerPower>().powerInformation.description;
        ApplianceDesc.text = player.GetComponent<Appliance>().applianceScriptable.description;

    }

    public void SelectPower()
    {

    }
    public void SelectPower(PlayerPowerScriptable powerScriptable, bool setPref = true)
    {

        PlayerPower power = null;

        if(setPref)
            PlayerPrefs.SetString(PowerPref, powerScriptable.powerName.ToString());

        switch(powerScriptable.powerName)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                power = player.AddComponent<Molapeno>();
                break;
            case PlayerPowerScriptable.PowerName.CarbUp:
                power = player.AddComponent<CarbUp>();
                break;
            case PlayerPowerScriptable.PowerName.SpearOfDesticheese:
                power = player.AddComponent<SpearOfDesticheese>();
                break;
            case PlayerPowerScriptable.PowerName.PortableLunch:
                power = player.AddComponent<PortableLunch>();
                break;
            case PlayerPowerScriptable.PowerName.Ham_mer:
                power = player.AddComponent<Hammer>();
                break;
            case PlayerPowerScriptable.PowerName._50CheeseStrike:
                power = player.AddComponent<_50CheeseStrike>();
                break;
            case PlayerPowerScriptable.PowerName.Catapasta:
                power = player.AddComponent<Catapasta>();
                break;
            case PlayerPowerScriptable.PowerName.IceTray:
                power = player.AddComponent<IceTray>();
                break;
            case PlayerPowerScriptable.PowerName.SugarRush:
                power = player.AddComponent<SugarRush>();
                break;
            case PlayerPowerScriptable.PowerName.Glockamole:
                power = player.AddComponent<Glockamole>();
                break;
            case PlayerPowerScriptable.PowerName.Mortater:
                power = player.AddComponent<Mortater>();
                break;
            case PlayerPowerScriptable.PowerName.Carrot50Cal:
                power = player.AddComponent<Carrot50Cal>();
                break;
            case PlayerPowerScriptable.PowerName.M1BrownieMachineGun:
                power = player.AddComponent<M1BrownieMachineGun>();
                break;
            default:
                break;
        }

        if(power!= null)
        {
            power.SetScriptableData(powerScriptable);
        }

        hudScript.SetPowerImage(powerScriptable.powerName);

        foreach (var p in player.GetComponents<PlayerPower>())
            if (p != power)
            {
                p.DoRemovalThings();
                Destroy(p);
            }
    }

    public void SelectAppliance(PlayerApplianceScriptable applianceScriptable, bool setPref = true)
    {

        Appliance appliance = null;

        if(setPref)
            PlayerPrefs.SetString(AppliancePref, applianceScriptable.applianceName.ToString());

        switch (applianceScriptable.applianceName)
        {
            case PlayerApplianceScriptable.ApplianceName.Fridge:
                appliance = player.AddComponent<Fridge>();
                break;
            case PlayerApplianceScriptable.ApplianceName.CookingOil:
                appliance = player.AddComponent<CookingOil>();
                break;
            case PlayerApplianceScriptable.ApplianceName.CheeseGrater:
                appliance = player.AddComponent<CheeseGrater>();
                break;
            case PlayerApplianceScriptable.ApplianceName.Stove:
                appliance = player.AddComponent<Stove>();
                break;
            case PlayerApplianceScriptable.ApplianceName.Grill:
                appliance = player.AddComponent<Grill>();
                break;
            case PlayerApplianceScriptable.ApplianceName.Blender:
                appliance = player.AddComponent<Blender>();
                break;
            case PlayerApplianceScriptable.ApplianceName.Freezer:
                appliance = player.AddComponent<Freezer>();
                break;
            case PlayerApplianceScriptable.ApplianceName.WardenGamseysHarshInstructions:
                appliance = player.AddComponent<WardenGamseysHarshInstructions>();
                break;
            case PlayerApplianceScriptable.ApplianceName.Microwave:
                appliance = player.AddComponent<Microwave>();
                break;
            default:
                break;
        }

        if (appliance != null)
        {
            appliance.SetScriptableData(applianceScriptable);
        }

        foreach (var p in player.GetComponents<Appliance>())
            if (p != appliance)
            {
                p.RemoveEffects();
                Destroy(p);
            }
    }

    public void Confirm()
    {
        IronChefUtils.TurnOnCharacter();

        PlayerHud.SetActive(true);

        LevelProgressManager.levelProgressManager.DisplayDish();



        gameObject.SetActive(false);

        

        
    }


    public static void CheckUnlocks()
    {
        /*
        foreach (var t in FindObjectsOfType<AppliancePowerPageManager>())
        {

            foreach (var p in t.pages)
                p.gameObject.SetActive(true);
            foreach (var b in t.GetComponentsInChildren<PowerButton>())
            {
                b.CheckIfUnlocked();
            }
            foreach (var b in t.GetComponentsInChildren<ApplianceButton>())
            {
                b.CheckIfUnlocked();
            }

            t.TurnOffAllPages();
            t.TurnOnCurrentPage();
        }
        */

    }
}


