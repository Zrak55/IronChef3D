using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freezer : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        FreezerHitModifier mod = new FreezerHitModifier();
        mod.hitCount = (int)applianceScriptable.values[0];
        mod.damage = applianceScriptable.values[1];
        mod.duration = IronChefUtils.InfiniteDuration;
        mod.modName = PlayerAttackHitModifier.PlayerHitModName.Freezer;
        mod.soundEffect = SoundEffectSpawner.SoundEffect.Freezer;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(mod);
    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.Freezer);
    }
}
