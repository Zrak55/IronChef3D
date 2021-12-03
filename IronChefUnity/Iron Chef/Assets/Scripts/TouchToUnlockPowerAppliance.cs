using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchToUnlockPowerAppliance : MonoBehaviour
{
    public string thingName;
    public bool isAppliance;
    public bool isPower;


    // Start is called before the first frame update
    void Start()
    {
        if (isAppliance)
            if (UnlocksManager.HasAppliance(thingName))
                gameObject.SetActive(false);
        if (isPower)
            if (UnlocksManager.HasPower(thingName))
                gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

            FindObjectOfType<PlayerHUDManager>().UnlockNotification(thingName);

            if (isAppliance)
                UnlocksManager.UnlockAppliance(thingName);
            if (isPower)
                UnlocksManager.UnlockPower(thingName);
            gameObject.SetActive(false);

        }
    }
}
