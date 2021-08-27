using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostCooldownManager : MonoBehaviour
{
    public float JumpCost = 30f;
    public float RollCost = 30f;
    public float SpringCostPerSecond = 10f;


    public float FryingPanCooldown = 5f;
    [HideInInspector]
    public bool FryingPanOnCooldown = false;
    float currentFryingPanCooldown = 0;


    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void SetFryingPanCooldown()
    {
        currentFryingPanCooldown = FryingPanCooldown;
    }
}
