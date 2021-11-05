using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grill : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        PlayerAttackHitModifier myMod = new PlayerAttackHitModifier();
        myMod.damageIncrease = applianceScriptable.values[0];
        myMod.modName = PlayerAttackHitModifier.PlayerHitModName.Grill;
        myMod.duration = IronChefUtils.InfiniteDuration;

        GetComponent<PlayerAttackModifierController>().HitModifiers.Add(myMod);



        GetComponent<PlayerHitpoints>().SetMax(FindObjectOfType<PlayerHitpoints>().GetMax() * (1 - applianceScriptable.values[1]));
        GetComponent<PlayerHitpoints>().SetCurrent(GetComponent<PlayerHitpoints>().GetMax());

    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();

        GetComponent<PlayerAttackModifierController>().RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName.Grill);

        GetComponent<PlayerHitpoints>().SetMax(FindObjectOfType<PlayerHitpoints>().GetMax() / (1 - applianceScriptable.values[1]));
        GetComponent<PlayerHitpoints>().SetCurrent(GetComponent<PlayerHitpoints>().GetMax());

    }
}
