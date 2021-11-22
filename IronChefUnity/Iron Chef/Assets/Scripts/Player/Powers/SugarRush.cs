using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : PlayerPower
{
    GameObject particles;

    float moveSpeedStim;
    float attackSpeedStim;
    float duration;

    GameObject currentParticles;
    Transform particlePoint;
    PlayerSpeedController speed;
    PlayerAttackController pattacks;
    // Start is called before the first frame update
    void Start()
    {
        particlePoint = GetComponent<CharacterMover>().model.transform;
        speed = GetComponent<PlayerSpeedController>();
        pattacks = GetComponent<PlayerAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);

        particles = power.prefabs[0];

        moveSpeedStim = power.values[0];
        attackSpeedStim = power.values[1];
        duration = power.values[2];
    
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();

        currentParticles = Instantiate(particles, particlePoint);
        IronChefUtils.AddSpeedUp(speed, moveSpeedStim, duration, SpeedEffector.EffectorName.SugarRush);
        pattacks.AddAttackSpeed(attackSpeedStim);

        FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.SugarRush);

        Invoke("UnPower", duration);
    }

    void UnPower()
    {
        pattacks.RemoveAttackSpeed(attackSpeedStim);
        Destroy(currentParticles);
    }
}
