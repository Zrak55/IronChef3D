using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();

        PlayerAttackHitModifier mod = new PlayerAttackHitModifier();
        mod.damageIncrease = 0;
        mod.slowAmount = applianceScriptable.values[0];
        mod.slowDuration = applianceScriptable.values[1];
        mod.slowName = SpeedEffector.EffectorName.Fridge;
        mod.duration = IronChefUtils.InfiniteDuration;
        mod.modName = PlayerAttackHitModifier.PlayerHitModName.Fridge;
        mod.soundEffect = SoundEffectSpawner.SoundEffect.FridgeSlow;
        mod.shouldPlaySound = true;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(mod);




    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.Fridge);

    }
}
