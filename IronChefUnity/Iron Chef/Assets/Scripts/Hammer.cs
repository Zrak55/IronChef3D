using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : PlayerPower
{
    float damage;

    float width;
    float depth;
    float height;

    float animTime;

    GameObject meatMallet;
    GameObject hitEffect;

    PlayerAttackController pattack;
    PlayerSpeedController speed;
    

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<PlayerSpeedController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();

        var hits = IronChefUtils.GetCastHits(width, height, depth, transform.position, GetComponent<CharacterMover>().model.transform.rotation);
        foreach(var h in hits)
        {
            if(h.GetComponentInParent<EnemyHitpoints>() != null)
            {
                h.GetComponentInParent<EnemyHitpoints>().TakeDamage(damage);
                Destroy(Instantiate(hitEffect, h.transform.position + (Vector3.up * 1.5f), Quaternion.identity), 3f);
                FindObjectOfType<PlayerCamControl>().ShakeCam(7, 0.75f);
            }
        }
        if (hits.Count > 0)
            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Hammer);

    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);

        pattack = GetComponent<PlayerAttackController>();

        damage = power.values[0];
        width = power.values[1];
        depth = power.values[2];
        height = power.values[3];
        animTime = power.values[4];

        hitEffect = power.prefabs[1];

        meatMallet = Instantiate(power.prefabs[0], pattack.throwPoint.parent);
        meatMallet.SetActive(false);
    }

    public override void PerformPower()
    {
        base.PerformPower();
        meatMallet.SetActive(true);
        pattack.DeactivateBasicWeapon();
        Invoke("MalletOff", animTime);
        IronChefUtils.AddSlow(speed, 100f, animTime, SpeedEffector.EffectorName.HammerRoot);
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.HammerBuild);

    }

    void MalletOff()
    {
        meatMallet.SetActive(false);
        pattack.ActivateBasicWeapon();

    }

    public override void DoRemovalThings()
    {
        base.DoRemovalThings();
        Destroy(meatMallet);
    }
}
