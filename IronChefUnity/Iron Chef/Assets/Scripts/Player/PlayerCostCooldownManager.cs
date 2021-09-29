using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostCooldownManager : MonoBehaviour
{
    public float JumpCost = 30f;
    public float RollCost = 30f;
    public float SprintCostPerSecond = 10f;
    [Space]
    public float EatCD;
    [HideInInspector]
    public bool EatOnCD = false;
    float currentEatCD;
    public float FryingPanCooldown = 5f;
    [HideInInspector]
    public bool FryingPanOnCooldown = false;
    float currentFryingPanCooldown = 0;


    public float PowerCooldown;
    float currentPowerCooldown = 0;
    public bool PowerOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        currentFryingPanCooldown = 0;
        currentPowerCooldown = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownAll();
    }

    void cooldownAll()
    {
        if(currentFryingPanCooldown > 0)
        {
            currentFryingPanCooldown -= Time.deltaTime;
        }
        FryingPanOnCooldown = currentFryingPanCooldown > 0;

        if(currentPowerCooldown > 0)
        {
            currentPowerCooldown -= Time.deltaTime;
        }
        PowerOnCooldown = currentPowerCooldown > 0;

        if(currentEatCD > 0)
        {
            currentEatCD -= Time.deltaTime;
        }
        EatOnCD = currentEatCD > 0;
    }

    public void SetFryingPanCooldown()
    {
        currentFryingPanCooldown = FryingPanCooldown;
    }

    public void SetPowerCooldown()
    {
        currentPowerCooldown = PowerCooldown;
    }

    public void SetEatCD()
    {
        currentEatCD = EatCD;
    }
}
