using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppliancePowerSelection : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public GameObject turnin;
    public GameObject playerCam;

    public Text PowerName;
    public Text ApplianceName;
    public Text PowerDesc;
    public Text ApplianceDesc;

    [Space]
    public GameObject firstSelectButton;
    public PowerButton firstPowerButton;
    public ApplianceButton firstApplianceButton;

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

        firstPowerButton.SelectPower();
        firstApplianceButton.SelectAppliance();

    }

    // Update is called once per frame
    void Update()
    {
        PowerName.text = player.GetComponent<PlayerPower>().powerInformation.DisplayName;
        ApplianceName.text = player.GetComponent<Appliance>().applianceScriptable.DisplayName;
        PowerDesc.text = player.GetComponent<PlayerPower>().powerInformation.description;
        ApplianceDesc.text = player.GetComponent<Appliance>().applianceScriptable.description;
    }

    public void SelectPower()
    {

    }
    public void SelectPower(PlayerPowerScriptable powerScriptable)
    {
        if(player.GetComponent<PlayerPower>() != null)
        {
            player.GetComponent<PlayerPower>().DoRemovalThings();
            Destroy(player.GetComponent<PlayerPower>());
        }

        PlayerPower power = null;

        switch(powerScriptable.powerName)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                power = player.AddComponent<Molapeno>();
                break;
            case PlayerPowerScriptable.PowerName.BreadTrap:
                power = player.AddComponent<BreadTrap>();
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
            default:
                break;
        }

        if(power!= null)
        {
            power.SetScriptableData(powerScriptable);
        }

        FindObjectOfType<PlayerHUDManager>().SetPowerImage(powerScriptable.powerName);
    }

    public void SelectAppliance(PlayerApplianceScriptable applianceScriptable)
    {
        if (player.GetComponent<Appliance>() != null)
        {
            player.GetComponent<Appliance>().RemoveEffects();
            Destroy(player.GetComponent<Appliance>());
        }

        Appliance appliance = null;

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
    }

    public void Confirm()
    {
        IronChefUtils.TurnOnCharacter();

        //TODO: Update with proper models/anims/etc
        door.SetActive(false);
        turnin.SetActive(true);



        gameObject.SetActive(false);

        
    }


    public static void CheckUnlocks()
    {
        foreach (var t in FindObjectsOfType<AppliancePowerPageManager>())
        {

            foreach (var p in t.pages)
                p.gameObject.SetActive(true);
            foreach (var b in FindObjectsOfType<PowerButton>())
            {
                b.CheckIfUnlocked();
            }
            foreach (var b in FindObjectsOfType<ApplianceButton>())
            {
                b.CheckIfUnlocked();
            }

            t.TurnOffAllPages();
            t.TurnOnCurrentPage();
        }

    }
}


