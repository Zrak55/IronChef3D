using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ApplianceButton : MonoBehaviour
{
    public PlayerApplianceScriptable appliance;
    Text text;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            text.text = appliance.briefDescription;
        }
        else
        {
            text.text = appliance.DisplayName;
        }
    }

    public void SelectAppliance()
    {
        GetComponentInParent<AppliancePowerSelection>().SelectAppliance(appliance);
    }

    
    
}
