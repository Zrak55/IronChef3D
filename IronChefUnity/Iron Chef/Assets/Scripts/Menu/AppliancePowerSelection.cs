using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppliancePowerSelection : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public GameObject turnin;
    public GameObject playerCam;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMover>().gameObject;
        playerCam = FindObjectOfType<PlayerCameraSetup>().gameObject;
        IronChefUtils.TurnOffCharacter();
        FindObjectOfType<PowerButton>().SelectPower();
        FindObjectOfType<ApplianceButton>().SelectAppliance();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPower()
    {

    }
    public void SelectPower(PlayerPowerScriptable powerScriptable)
    {
        if(player.GetComponent<PlayerPower>() != null)
        {
            Destroy(player.GetComponent<PlayerPower>());
        }

        PlayerPower power = null;

        switch(powerScriptable.powerName)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                power = player.AddComponent<Molapeno>();
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
            Destroy(player.GetComponent<Appliance>());
        }

        Appliance appliance = null;

        switch (applianceScriptable.applianceName)
        {
            case PlayerApplianceScriptable.ApplianceName.Fridge:
                appliance = player.AddComponent<Fridge>();
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


}
