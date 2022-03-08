using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppliancePowerPageManager : MonoBehaviour
{
    public List<AppliancePowerPage> pages;
    int currentIndex;
    public Button rightButton;
    public Button leftButton;

    public bool autoSelect = true;

    bool firstOn = true;

    private void OnEnable()
    {
        AppliancePowerSelection.CheckUnlocks();
    }
    private void Start()
    {
        
    }

    public void StartThings()
    {
        if (firstOn)
        {
            currentIndex = -1;
            LoadNextPage(false);
            autoSelect = true;
            firstOn = false;
        }
    }


    public void LoadNextPage()
    {
        LoadNextPage(true);
    }

    public void LoadNextPage(bool setPref)
    {
        ForwardIndex();
        TurnOffAllPages();
        TurnOnCurrentPage(setPref);
        SetNav();

    }

    public void LoadPrevPage()
    {
        LoadPrevPage(true);
    }
    public void LoadPrevPage(bool setPref)
    {
        BackwardIndex();
        TurnOffAllPages();
        TurnOnCurrentPage(setPref);
        SetNav();
        
    }

    public void TurnOffAllPages()
    {
        foreach (var p in pages)
            p.gameObject.SetActive(false);
    }

    public void TurnOnCurrentPage(bool setPref)
    {
        pages[currentIndex].gameObject.SetActive(true);
        if (autoSelect)
        {
            pages[currentIndex].lastButton.GetComponent<ApplianceButton>()?.AwakeSelf();
            pages[currentIndex].lastButton.GetComponent<ApplianceButton>()?.SelectAppliance(setPref);
            pages[currentIndex].lastButton.GetComponent<PowerButton>()?.AwakeSelf();
            pages[currentIndex].lastButton.GetComponent<PowerButton>()?.SelectPower(setPref);
        }
    }

    public void TurnOnCurrentPage()
    {
        TurnOnCurrentPage(true);
    }

    void ForwardIndex()
    {
        currentIndex++;
        if (currentIndex >= pages.Count)
            currentIndex = 0;
    }
    void BackwardIndex()
    {
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = pages.Count - 1;
    }

    public string LastPowerButtonName()
    {
        string ret = "";

        if (pages[currentIndex].lastButton.GetComponent<PowerButton>() != null)
            ret = pages[currentIndex].lastButton.GetComponent<PowerButton>().power.powerName.ToString();


        return ret;
    }

    public string LastApplianceButtonName()
    {
        string ret = "";

        if (pages[currentIndex].lastButton.GetComponent<ApplianceButton>() != null)
            ret = pages[currentIndex].lastButton.GetComponent<ApplianceButton>().appliance.applianceName.ToString();


        return ret;
    }

    void SetNav()
    {
        /*
        var nav = rightButton.navigation;
        nav.selectOnUp = pages[currentIndex].lastButton;
        rightButton.navigation = nav;
        nav = leftButton.navigation;
        nav.selectOnUp = pages[currentIndex].lastButton;
        leftButton.navigation = nav;
        */

    }

    public void DEBUG_UnlockAllPowersAndApplianes()
    {
        foreach(var t in FindObjectsOfType<AppliancePowerPageManager>())
        {

            foreach (var p in t.pages)
                p.gameObject.SetActive(true);
            foreach (var b in FindObjectsOfType<PowerButton>())
            {
                UnlocksManager.UnlockPower(b.power.powerName.ToString());
                b.CheckIfUnlocked();
            }
            foreach (var b in FindObjectsOfType<ApplianceButton>())
            {
                UnlocksManager.UnlockAppliance(b.appliance.applianceName.ToString());
                b.CheckIfUnlocked();
            }

            t.TurnOffAllPages();
            t.TurnOnCurrentPage();
        }
    }
}
