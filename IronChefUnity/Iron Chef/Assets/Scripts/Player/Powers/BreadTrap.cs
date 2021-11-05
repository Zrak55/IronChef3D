using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadTrap : PlayerPower
{
    [Header("Bread Trap Things")]
    public float BaseStunDuration;
    public float BaseTriggerRadius;
    [Space]
    public GameObject TrapPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        var trap = Instantiate(TrapPrefab, transform.position + transform.forward * 2, transform.rotation).GetComponent<BreadTrapTrigger>();
        trap.SetData(BaseStunDuration, BaseTriggerRadius);
    }
    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);

        BaseStunDuration = powerInformation.values[0];
        BaseTriggerRadius = powerInformation.values[1];
        TrapPrefab = powerInformation.prefabs[0];
    }
}
