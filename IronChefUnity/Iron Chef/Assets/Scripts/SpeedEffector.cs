using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedEffector
{
    [Range(-1, 10)]
    public float percentAmount;
    public float duration;
    public EffectorName effectName;




    public enum EffectorName
    {
        Fridge,
        BenedictSpeed,
        SugarRush,
        OnionKnight,
        CheeseGrater,
        HammerRoot,
        CarbUp
    }
}
