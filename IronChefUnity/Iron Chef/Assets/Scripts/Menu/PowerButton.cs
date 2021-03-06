using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PowerButton : MonoBehaviour
{
    public PlayerPowerScriptable power;
    Text text;

    public bool isLocked;

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
        {
            text.text = "Power locked!";
        }
        else
        {


           // if (EventSystem.current.currentSelectedGameObject == this.gameObject)
           // {
           //     text.text = power.briefDescription;
           // }
          //  else
          //  {
          //      text.text = power.DisplayName;
          //  }
        }
    }

    public void AwakeSelf()
    {
        if (power.powerName == PlayerPowerScriptable.PowerName.Molapeno)
            UnlocksManager.UnlockPower(power.powerName.ToString());
        CheckIfUnlocked();

        GetComponent<Image>().sprite = PlayerHUDManager.PlayerHud.GetPowerImage(power.powerName);
    }

    public void SelectPower()
    {
        SelectPower(true);
    }

    public void SelectPower(bool setPref)
    {
        if(isLocked)
        {

        }
        else
        {
            GetComponentInParent<AppliancePowerSelection>().SelectPower(power, setPref);

        }
    }

    public void CheckIfUnlocked()
    {
        isLocked = !UnlocksManager.HasPower(power.powerName.ToString());
    }
}
