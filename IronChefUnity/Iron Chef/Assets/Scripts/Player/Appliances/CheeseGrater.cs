using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGrater : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        foreach(var hp in FindObjectsOfType<EnemyHitpoints>())
        {
            hp.SetMax(hp.GetMax() * (1 - applianceScriptable.values[0]));
            hp.SetCurrent(hp.GetMax());
        }
    }

    public override void RemoveEffects()
    {
        base.RemoveEffects(); 
        foreach (var hp in FindObjectsOfType<EnemyHitpoints>())
        {
            hp.SetMax(hp.GetMax() / (1 - applianceScriptable.values[0]));
            hp.SetCurrent(hp.GetMax());
        }
    }
}
