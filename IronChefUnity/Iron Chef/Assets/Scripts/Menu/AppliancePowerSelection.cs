using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppliancePowerSelection : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterMover>().gameObject;
        IronChefUtils.ShowMouse();
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
        switch(powerScriptable.powerName)
        {
            case PlayerPowerScriptable.PowerName.Molapeno:
                player.AddComponent<Molapeno>();
                break;
            default:
                break;
        }

        var power = player.GetComponent<PlayerPower>();
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
        switch (applianceScriptable.applianceName)
        {
            case PlayerApplianceScriptable.ApplianceName.Fridge:
                player.AddComponent<Fridge>();
                break;
            default:
                break;
        }

        var appliance = player.GetComponent<Appliance>();
        if (appliance != null)
        {
            appliance.SetScriptableData(applianceScriptable);
        }
    }

    public void Confirm()
    {
        IronChefUtils.HideMouse();
        gameObject.SetActive(false);
    }
}
