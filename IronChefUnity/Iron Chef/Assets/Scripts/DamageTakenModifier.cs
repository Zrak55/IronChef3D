using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakenModifier
{
    public float amount;
    public float duration;
    public ModifierName name;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public enum ModifierName
    {
        BenedictImmunity,
        BenedictDouble
    }
}
