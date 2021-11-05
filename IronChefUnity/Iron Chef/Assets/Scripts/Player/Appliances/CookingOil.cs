using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingOil : Appliance
{

    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        PlayerAttackHitModifier myMod = new PlayerAttackHitModifier();
        myMod.damageIncrease = applianceScriptable.values[0];
        myMod.modName = PlayerAttackHitModifier.PlayerHitModName.CookingOil;
        myMod.duration = IronChefUtils.InfiniteDuration;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(myMod);

    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();

        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.CookingOil);
    }
}
