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
        IronChefUtils.ShowMouse();
        player.GetComponent<CharacterMover>().enabled = false;
        playerCam.GetComponent<PlayerCameraSetup>().CanMoveCam = false;
        player.GetComponent<PlayerAttackController>().canAct = false;
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
        IronChefUtils.HideMouse();
        player.GetComponent<CharacterMover>().MouseOff = true;

        //TODO: Update with proper models/anims/etc
        door.SetActive(false);
        turnin.SetActive(true);

        player.GetComponent<CharacterMover>().enabled = true;
        playerCam.GetComponent<PlayerCameraSetup>().CanMoveCam = true;
        player.GetComponent<PlayerAttackController>().canAct = true;


        gameObject.SetActive(false);

        
    }
}
