using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerButton : MonoBehaviour
{
    public PlayerPowerScriptable power;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == this.gameObject)
        {
            text.text = power.briefDescription;
        }
        else
        {
            text.text = power.DisplayName;
        }
    }

    public void SelectPower()
    {
        GetComponentInParent<AppliancePowerSelection>().SelectPower(power);
    }
}
