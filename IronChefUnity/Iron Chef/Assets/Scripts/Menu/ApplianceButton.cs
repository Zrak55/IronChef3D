using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplianceButton : MonoBehaviour
{
    public PlayerApplianceScriptable appliance;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectAppliance()
    {
        GetComponentInParent<AppliancePowerSelection>().SelectAppliance(appliance);
    }
}
