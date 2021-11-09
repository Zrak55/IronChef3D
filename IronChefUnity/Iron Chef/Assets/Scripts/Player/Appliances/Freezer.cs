using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        FreezerHitModifier mod = new FreezerHitModifier();
        mod.stunTime = applianceScriptable.values[0];
        mod.stunDelay = applianceScriptable.values[1];
        mod.limit = (int)applianceScriptable.values[2];
        mod.duration = IronChefUtils.InfiniteDuration;
        mod.modName = PlayerAttackHitModifier.PlayerHitModName.Freezer;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(mod);
    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.Freezer);
    }
}
