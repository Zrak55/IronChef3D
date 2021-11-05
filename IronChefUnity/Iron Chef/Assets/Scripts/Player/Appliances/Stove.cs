using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stove : Appliance
{
    protected override void ApplyEffects()
    {
        base.ApplyEffects();
        GetComponent<PlayerHitpoints>().SetMax(FindObjectOfType<PlayerHitpoints>().GetMax() * (1 + applianceScriptable.values[0]));
        GetComponent<PlayerHitpoints>().SetCurrent(GetComponent<PlayerHitpoints>().GetMax());
    }
    public override void RemoveEffects()
    {
        base.RemoveEffects();
        GetComponent<PlayerHitpoints>().SetMax(FindObjectOfType<PlayerHitpoints>().GetMax() / (1 + applianceScriptable.values[0]));
        GetComponent<PlayerHitpoints>().SetCurrent(GetComponent<PlayerHitpoints>().GetMax());
    }

}
