using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molapeno : PlayerPower
{
    [Header("Malapeno Things")]
    public float baseDamagePerSecond;
    public float baseDuration;
    public float baseRadius;
    public float launchAngle;
    public float launchForce;
    [Space]
    public GameObject MolapenoPrefab;
    
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
        var proj = Instantiate(MolapenoPrefab, GetComponent<PlayerAttackController>().throwPoint.position, GetComponent<CharacterMover>().model.transform.rotation).GetComponent<MolapenoProjectile>();

        //TOD: Get damage buffs/other buffs

        proj.SetData(baseDamagePerSecond, launchForce, launchAngle, baseDuration, baseRadius);

    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        baseDamagePerSecond = powerInformation.values[0];
        baseDuration = powerInformation.values[1];
        baseRadius = powerInformation.values[2];
        launchAngle = powerInformation.values[3];
        launchForce = powerInformation.values[4];
        MolapenoPrefab = powerInformation.prefabs[0];
    }
}
