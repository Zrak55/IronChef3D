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
    protected PlayerAttackController attkControl;

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
        anim = GetComponentInChildren<Animator>();
        attkControl = GetComponent<PlayerAttackController>();
    }

    public virtual void PerformPower()
    {


    }

    public virtual void DoPowerEffects()
    {
        cooldownManager.SetPowerCooldown();
        attkControl.lining = false;
    }

    public virtual void PlayerPowerPressed()
    {

    }

    public virtual void SetScriptableData(PlayerPowerScriptable power)
    {
        powerInformation = power;
        cooldownManager.PowerCooldown = powerInformation.cooldown;
        anim.SetInteger("PowerNumber", powerInformation.animationNumber);
    }

    public virtual void DoRemovalThings()
    {

    }
}
