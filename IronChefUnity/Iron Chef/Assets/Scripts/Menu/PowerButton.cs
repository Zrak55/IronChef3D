using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerButton : MonoBehaviour
{
    public PlayerPowerScriptable power;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPower()
    {
        GetComponentInParent<AppliancePowerSelection>().SelectPower(power);
    }
}
