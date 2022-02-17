using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsHandler : MonoBehaviour
{
    public PlayerAttackController playerAttacks;
    public PlayerPower power;
    public CharacterMover characterMover;
    public PlayerAudioEvents audioEvent;


    bool doubleFPPrevent = false;

    private void Awake()
    {
        playerAttacks = GetComponentInParent<PlayerAttackController>();
        characterMover = GetComponentInParent<CharacterMover>();
        audioEvent = GetComponentInParent<PlayerAudioEvents>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (power == null)
            power = FindObjectOfType<PlayerPower>();
    }

    public void BasicHitOn()
    {
        playerAttacks.BasicHitOn();
    }
    public void BasicHitOff()
    {
        playerAttacks.BasicHitOff();
    }
    public void DoPowerEffects()
    {
        power.DoPowerEffects();
    }
    public void DeactivateBasicWeapon()
    {
        playerAttacks.DeactivateBasicWeapon();
    }
    public void PerformFryingPan()
    {
        if(!doubleFPPrevent)
        {
            doubleFPPrevent = true;
            playerAttacks.PerformFryingPan();
            Invoke("UndoFPPrevent", 0.5f);
        }
    }

    public void TurnBasicAttackTrailOn()
    {
        playerAttacks.TurnBasicAttackTrailOn();
    }
    public void TurnBasicAttackTrailOff()
    {
        playerAttacks.TurnBasicAttackTrailOff();
    }
    void UndoFPPrevent()
    {
        doubleFPPrevent = false;
    }
    public void UndoRoll()
    {
        characterMover.UndoRoll();
    }
    
    public void MakeFootstepSound()
    {
        audioEvent.MakeFootstepSound();
    }

    public void MakeFootstepEffect(string foot)
    {
        bool isLeftFoot = (foot.ToUpper()) == "LEFT";
        characterMover.MakeFootstepEffect(isLeftFoot);
    }

    public void ExitPotRollPositionUpdate()
    {
        transform.root.position = transform.root.position + transform.root.forward * 10f;
    }
}
