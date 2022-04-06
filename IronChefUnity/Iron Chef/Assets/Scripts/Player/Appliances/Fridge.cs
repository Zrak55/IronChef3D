using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();

        PlayerProjectile.ExtraFryingPanBounces += (int)applianceScriptable.values[0];




    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        PlayerProjectile.ExtraFryingPanBounces -= (int)applianceScriptable.values[0];

    }
}
