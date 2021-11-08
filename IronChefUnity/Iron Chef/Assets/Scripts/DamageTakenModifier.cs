using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenModifier
{
    public float amount;
    public float duration;
    public ModifierName name;


    public enum ModifierName
    {
        BenedictImmunity,
        BenedictDouble
    }

    public virtual void SpecialTakeDamageEffect(float OriginalDamageTaken)
    {

    }
}
