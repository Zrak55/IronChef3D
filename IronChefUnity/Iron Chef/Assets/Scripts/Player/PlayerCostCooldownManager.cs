using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCostCooldownManager : MonoBehaviour
{
    public float JumpCost = 30f;
    public float RollCost = 30f;
    public float SprintCostPerSecond = 10f;
    [Space]
    public float EatCD;
    [HideInInspector]
    public bool EatOnCD = false;
    float currentEatCD;
    public float FryingPanCooldown = 5f;
    [HideInInspector]
    public bool FryingPanOnCooldown = false;
    float currentFryingPanCooldown = 0;
    public float jumpCD = 0.5f;
    float currentJumpCD = 0f;

    public float PowerCooldown;
    float currentPowerCooldown = 0;
    public bool PowerOnCooldown = false;
    public bool JumpOnCooldown = false;

    public SoundEffectSpawner sounds;

    // Start is called before the first frame update
    void Start()
    {
        currentFryingPanCooldown = 0;
        currentPowerCooldown = 0;
        sounds = SoundEffectSpawner.soundEffectSpawner;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownAll();
    }

    public float GetPowerCDPercent()
    {
        return currentPowerCooldown / PowerCooldown;
    }
    public float GetFryingPanCDPercent()
    {
        return currentFryingPanCooldown / FryingPanCooldown;
    }

    void cooldownAll()
    {
        if(currentFryingPanCooldown > 0)
        {
            currentFryingPanCooldown -= Time.deltaTime;
        }
        FryingPanOnCooldown = currentFryingPanCooldown > 0;

        if(currentPowerCooldown > 0)
        {
            currentPowerCooldown -= Time.deltaTime;
        }
        bool playPowerSound = false;
        if (PowerOnCooldown)
            playPowerSound = true;
        PowerOnCooldown = currentPowerCooldown > 0;
        if (!PowerOnCooldown && playPowerSound)
            sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.AbilityRecharge);

        if(currentEatCD > 0)
        {
            currentEatCD -= Time.deltaTime;
        }
        EatOnCD = currentEatCD > 0;
        if (currentJumpCD > 0)
        {
            currentJumpCD -= Time.deltaTime;
        }
        JumpOnCooldown = currentJumpCD > 0;
    }

    public void SetFryingPanCooldown()
    {
        FryingPanOnCooldown = true;
        currentFryingPanCooldown = FryingPanCooldown;
    }

    public void SetPowerCooldown()
    {
        PowerOnCooldown = true;
        currentPowerCooldown = PowerCooldown;
    }

    public void SetEatCD()
    {
        EatOnCD = true;
        currentEatCD = EatCD;
    }

    public void SetJumpCD()
    {
        JumpOnCooldown = true;
        currentJumpCD = jumpCD;
    }
}
