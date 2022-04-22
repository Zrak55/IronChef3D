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
    protected bool internalCooldown = false;

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
        if(!internalCooldown)
        {
            internalCooldown = true;
            cooldownManager.SetPowerCooldown();
            attkControl.lining = false;
            Invoke("undoInternalCooldown", 0.1f);
        }
    }

    void undoInternalCooldown()
    {
        internalCooldown = false;
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
