using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot50Cal : PlayerPower
{
    GameObject spear;
    Transform shootPoint;
    Transform rotation;
    float damage;
    float speed;
    float distance;

    float animTime;

    PlayerAttackController pAttack;
    PlayerSpeedController pSpeed;

    // Start is called before the first frame update
    void Start()
    {
        shootPoint = GetComponent<PlayerAttackController>().throwPoint;
        rotation = GetComponent<CharacterMover>().model.transform;
    }

    // Update is called once per frame
    void Update()
    {

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
            var fp = Instantiate(spear, shootPoint.position, Quaternion.Euler(attkControl.SavedRangedAttackPoint));
            fp.GetComponent<PlayerProjectile>().damage = damage;
            fp.GetComponent<PlayerProjectile>().speed = speed;
            fp.GetComponent<PlayerProjectile>().maxAllowedDistance = distance;
            fp.GetComponent<PlayerProjectile>().FireProjectile(fp.transform.position + IronChefUtils.GetSoftLockDirection(fp.transform.forward, fp.transform.position, 1 << LayerMask.NameToLayer("Enemy"), 20, true));
            Invoke("TurnWeaponBackOn", 1f);

            //SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Glockamole);
        }
    }

    public void TurnWeaponBackOn()
    {
        pAttack.ActivateBasicWeapon();
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        pAttack = GetComponent<PlayerAttackController>();
        pSpeed = GetComponent<PlayerSpeedController>();
        spear = powerInformation.prefabs[0];
        damage = powerInformation.values[0];
        speed = powerInformation.values[1];
        distance = powerInformation.values[2];
        animTime = powerInformation.values[3];

    }

    public override void PerformPower()
    {
        base.PerformPower();
        IronChefUtils.AddSlow(pSpeed, 100f, animTime, SpeedEffector.EffectorName.Carrot50CalRoot);
        pAttack.DeactivateBasicWeapon();
    }
}
      
