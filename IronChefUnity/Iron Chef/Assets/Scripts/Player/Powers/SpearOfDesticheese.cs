using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearOfDesticheese : PlayerPower
{
    GameObject spear;
    Transform throwPoint;
    Transform rotation;
    float damage;
    float speed;
    float distance;

    // Start is called before the first frame update
    void Start()
    {
        throwPoint = GetComponent<PlayerAttackController>().throwPoint;
        rotation = GetComponent<CharacterMover>().model.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        var fp = Instantiate(spear, throwPoint.position, rotation.rotation);
        fp.GetComponent<PlayerProjectile>().damage = damage;
        fp.GetComponent<PlayerProjectile>().speed = speed;
        fp.GetComponent<PlayerProjectile>().maxAllowedDistance = distance;
        fp.GetComponent<PlayerProjectile>().FireProjectile(fp.transform.position + fp.transform.forward);
        FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.SpearThrow);
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        spear = powerInformation.prefabs[0];
        damage = powerInformation.values[0];
        speed = powerInformation.values[1];
        distance = powerInformation.values[2];

    }
}
