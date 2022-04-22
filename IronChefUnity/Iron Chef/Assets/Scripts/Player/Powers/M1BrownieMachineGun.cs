using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M1BrownieMachineGun : PlayerPower
{
    GameObject spear;
    Transform shootPoint;
    Transform rotation;
    float damage;
    float speed;
    float distance;
    int NumShots;
    float Duration;


    PlayerAttackController pAttack;
    PlayerSpeedController pSpeed;

    public override void DoPowerEffects()
    {
        if (!internalCooldown)
        {
            base.DoPowerEffects();
        }


        //SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Glockamole);
    }

    IEnumerator RepeatPower()
    {
        int numShots = 0;
        float tickRate = Duration / NumShots;

        while (numShots < NumShots)
        {

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.M1BrownieFire);
            var fp = Instantiate(spear, shootPoint.position, rotation.rotation);
            fp.GetComponent<PlayerProjectile>().damage = damage;
            fp.GetComponent<PlayerProjectile>().speed = speed;
            fp.GetComponent<PlayerProjectile>().maxAllowedDistance = distance;
            fp.GetComponent<PlayerProjectile>().FireProjectile(fp.transform.position + IronChefUtils.GetSoftLockDirection(fp.transform.forward, fp.transform.position, 1 << LayerMask.NameToLayer("Enemy"), 20, true));


            Debug.Log("Shoot");

            yield return new WaitForSeconds(tickRate);
            numShots++;
        }
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
        NumShots = (int)powerInformation.values[3];
        Duration = powerInformation.values[4];


        shootPoint = GetComponent<PlayerAttackController>().throwPoint;
        rotation = GetComponent<CharacterMover>().model.transform;

    }

    public override void PerformPower()
    {
        base.PerformPower();
        StartCoroutine(RepeatPower());
        Invoke("TurnWeaponBackOn", Duration);
    }

    public void TurnWeaponBackOn()
    {
        pAttack.ActivateBasicWeapon();
    }
}
