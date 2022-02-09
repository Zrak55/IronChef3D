using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardenGamseysHarshInstructions : Appliance 
{
    protected override void ApplyEffects()
    {
        /*
        WardenGamseyHitModifier mod = new WardenGamseyHitModifier();
        mod.stunTime = applianceScriptable.values[0];
        mod.stunChancePerDamage = applianceScriptable.values[1];
        mod.modName = PlayerAttackHitModifier.PlayerHitModName.WardenGamsey;
        mod.duration = IronChefUtils.InfiniteDuration;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(mod);
        */

        GetComponent<PlayerAttackController>().AddAttackSpeed(applianceScriptable.values[0]);
        base.ApplyEffects();
        
    }

    public override void RemoveEffects()
    {
        //GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.WardenGamsey);
        GetComponent<PlayerAttackController>().RemoveAttackSpeed(applianceScriptable.values[0]);


        base.RemoveEffects();
        
    }
}
