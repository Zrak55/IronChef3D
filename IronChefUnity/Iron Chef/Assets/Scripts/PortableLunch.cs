using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortableLunch : PlayerPower
{
    float healing;
    PlayerHitpoints hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<PlayerHitpoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        hp.RestoreHP(healing);
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        healing = power.values[0];
    }
}
