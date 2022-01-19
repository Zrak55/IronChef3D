using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGrater : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();

        var php = FindObjectOfType<PlayerHitpoints>();
        var pspeed = FindObjectOfType<PlayerSpeedController>();

        IronChefUtils.AddSpeedUp(pspeed, applianceScriptable.values[0], IronChefUtils.InfiniteDuration, SpeedEffector.EffectorName.CheeseGrater);
        php.SetMax(php.GetMax() * (1 - applianceScriptable.values[1]));
        php.SetCurrent(php.GetMax());
    }

    public override void RemoveEffects()
    {
        base.RemoveEffects();
        var php = FindObjectOfType<PlayerHitpoints>();
        var pspeed = FindObjectOfType<PlayerSpeedController>();

        pspeed.RemoveSpeedEffector(SpeedEffector.EffectorName.CheeseGrater);
        php.SetMax(php.GetMax() / (1 - applianceScriptable.values[1]));
        php.SetCurrent(php.GetMax());
    }
}
