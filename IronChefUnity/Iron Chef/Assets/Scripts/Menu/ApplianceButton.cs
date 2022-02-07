using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ApplianceButton : MonoBehaviour
{
    public PlayerApplianceScriptable appliance;
    Text text;
    bool isLocked;

    private void Awake()
    {
        AwakeSelf();
    }
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
            text.text = "Locked!";
        else
        {
            

           // if (EventSystem.current.currentSelectedGameObject == this.gameObject)
           // {
                //text.text = appliance.briefDescription;
            //}
           // else
          //  {
          //      text.text = appliance.DisplayName;
          //  }
            
        }
    }

    public void AwakeSelf()
    {

        if (appliance.applianceName == PlayerApplianceScriptable.ApplianceName.Fridge)
            UnlocksManager.UnlockAppliance(PlayerApplianceScriptable.ApplianceName.Fridge.ToString());
        CheckIfUnlocked();


        GetComponent<Image>().sprite = PlayerHUDManager.PlayerHud.GetApplianceImage(appliance.applianceName);
    }

    public void SelectAppliance()
    {
        if (isLocked)
        {

        }
        else
        {

            GetComponentInParent<AppliancePowerSelection>().SelectAppliance(appliance);
        }
    }

    public void CheckIfUnlocked()
    {
        isLocked = !UnlocksManager.HasAppliance(appliance.applianceName.ToString());
    }
}
