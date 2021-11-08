using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Microwave : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();

        var mod = new MicrowaveDamageTakenModifier();
        mod.duration = IronChefUtils.InfiniteDuration;
        mod.amount = applianceScriptable.values[0];
        mod.healMultiplier = applianceScriptable.values[1];
        mod.hpsTarget = applianceScriptable.values[2];
        mod.name = DamageTakenModifier.ModifierName.Microwave;

        GetComponent<PlayerDamageTakenModifierController>().AddMod(mod);
    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerDamageTakenModifierController>().removeMod(DamageTakenModifier.ModifierName.Microwave);
    }
}
