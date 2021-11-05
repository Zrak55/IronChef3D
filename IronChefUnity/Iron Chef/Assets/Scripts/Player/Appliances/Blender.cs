using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();

        PlayerAttackHitModifier mod = new PlayerAttackHitModifier();
        mod.critPercent = applianceScriptable.values[0];
        mod.duration = IronChefUtils.InfiniteDuration;
        mod.modName = PlayerAttackHitModifier.PlayerHitModName.Blender;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(mod);




    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.Fridge);

    }
}
