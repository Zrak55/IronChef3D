using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCostCooldownManager))]
public class PlayerPower : MonoBehaviour
{
    [Header("Basic Power Information")]
    public PlayerPowerScriptable powerInformation;
    protected PlayerCostCooldownManager cooldownManager;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DoAwakeThings();
    }

    protected virtual void DoAwakeThings()
    {
        cooldownManager = GetComponent<PlayerCostCooldownManager>();
        cooldownManager.PowerCooldown = powerInformation.cooldown;
    }

    public virtual void PerformPower()
    {
        if(cooldownManager.PowerOnCooldown == false)
        {
            anim.SetInteger("PowerNumber", powerInformation.animationNumber);
        }
    }

    public virtual void DoPowerEffects()
    {
        cooldownManager.SetPowerCooldown();
        
    }
}
