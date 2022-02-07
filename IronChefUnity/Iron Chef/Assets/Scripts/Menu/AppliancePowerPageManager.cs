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

    private void OnEnable()
    {
        AppliancePowerSelection.CheckUnlocks();
    }
    private void Start()
    {
        currentIndex = -1;
        LoadNextPage();
        autoSelect = true;
    }
    public void LoadNextPage()
    {
        ForwardIndex();
        TurnOffAllPages();
        TurnOnCurrentPage();
        SetNav();

    }
    public void LoadPrevPage()
    {
        BackwardIndex();
        TurnOffAllPages();
        TurnOnCurrentPage();
        SetNav();
        
    }

    public void TurnOffAllPages()
    {
        foreach (var p in pages)
            p.gameObject.SetActive(false);
    }
    public void TurnOnCurrentPage()
    {
        pages[currentIndex].gameObject.SetActive(true);
        if (autoSelect)
        {
            pages[currentIndex].lastButton.GetComponent<ApplianceButton>()?.AwakeSelf();
            pages[currentIndex].lastButton.GetComponent<ApplianceButton>()?.SelectAppliance();
            pages[currentIndex].lastButton.GetComponent<PowerButton>()?.AwakeSelf();
            pages[currentIndex].lastButton.GetComponent<PowerButton>()?.SelectPower();
        }
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

    void SetNav()
    {
        var nav = rightButton.navigation;
        nav.selectOnUp = pages[currentIndex].lastButton;
        rightButton.navigation = nav;
        nav = leftButton.navigation;
        nav.selectOnUp = pages[currentIndex].lastButton;
        leftButton.navigation = nav;

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
