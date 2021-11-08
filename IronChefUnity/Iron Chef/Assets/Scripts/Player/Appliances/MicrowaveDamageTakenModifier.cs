using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrowaveDamageTakenModifier : DamageTakenModifier
{
    
    public float hpsTarget;
    public float healMultiplier;
    public override void SpecialTakeDamageEffect(float OriginalDamageTaken)
    {
        base.SpecialTakeDamageEffect(OriginalDamageTaken);

        GameObject.FindObjectOfType<PlayerHealOverTimeManager>().AddHealOverTime(hpsTarget, OriginalDamageTaken * healMultiplier / hpsTarget);
    }
}
