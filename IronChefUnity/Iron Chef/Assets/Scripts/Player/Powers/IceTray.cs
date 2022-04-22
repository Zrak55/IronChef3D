using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTray : PlayerPower
{
    GameObject projectile;
    float damagePerProjectile;
    int numProjectiles;
    float launchForce;
    float launchAngle;
    float spreadConeAngle;

    Transform launchPoint;

    // Start is called before the first frame update
    void Start()
    {
        launchPoint = GetComponent<PlayerAttackController>().throwPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        projectile = power.prefabs[0];
        damagePerProjectile = power.values[0];
        numProjectiles = (int)power.values[1];
        launchForce = power.values[2];
        launchAngle = power.values[3];
        spreadConeAngle = power.values[4];
    }
    public override void PlayerPowerPressed()
    {
        base.PlayerPowerPressed();
        attkControl.ToggleAiming(true);
    }
    public override void DoPowerEffects()
    {
        if (!internalCooldown)
        {
            base.DoPowerEffects();
            for (int i = 0; i < numProjectiles; i++)
            {
                var newProj = Instantiate(projectile, launchPoint.position, Quaternion.Euler(attkControl.SavedRangedAttackPoint));
                newProj.GetComponent<IceTrayProjectile>().damage = damagePerProjectile;
                newProj.transform.Rotate(new Vector3(0, Random.Range(-spreadConeAngle / 2, spreadConeAngle / 2), 0));
                newProj.GetComponent<ProjectileLaunch>().Launch(launchForce, newProj.transform.forward, launchAngle + Random.Range(-spreadConeAngle / 2, spreadConeAngle / 2));
            }
        }
    }
}
