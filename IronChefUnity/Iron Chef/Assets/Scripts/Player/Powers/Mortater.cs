using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortater : PlayerPower
{
    [Header("Mortater Things")]
    public float baseDamage;
    public float baseRadius;
    public float launchAngle;
    public float launchForce;
    [Space]
    public GameObject MortaterPrefab;

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
        var proj = Instantiate(MortaterPrefab, GetComponent<PlayerAttackController>().throwPoint.position, Quaternion.Euler(attkControl.SavedRangedAttackPoint)).GetComponent<MortaterProjectile>();

        //TOD: Get damage buffs/other buffs


        proj.SetData(baseDamage, launchForce, launchAngle, baseRadius);

    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        baseDamage = powerInformation.values[0];
        baseRadius = powerInformation.values[1];
        launchAngle = powerInformation.values[2];
        launchForce = powerInformation.values[3];
        MortaterPrefab = powerInformation.prefabs[0];
    }
}
