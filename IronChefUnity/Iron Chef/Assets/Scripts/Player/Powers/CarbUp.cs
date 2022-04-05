using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarbUp : PlayerPower
{
    float speedUp;
    float duration;
    GameObject particles;
    GameObject currentParticles;
    PlayerSpeedController speed;
    Transform particlePoint;

    // Start is called before the first frame update
    void Start()
    {
        speed = GetComponent<PlayerSpeedController>();
        particlePoint = GetComponent<CharacterMover>().model.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();

        currentParticles = Instantiate(particles, particlePoint);
        IronChefUtils.AddSpeedUp(speed, speedUp, duration, SpeedEffector.EffectorName.CarbUp);

        Invoke("UnPower", duration);

    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        
        base.SetScriptableData(power);

        particles = power.prefabs[0];
        speedUp = power.values[0];
        duration = power.values[1];
        
    }

    void UnPower()
    {
        Destroy(currentParticles);
    }
}
