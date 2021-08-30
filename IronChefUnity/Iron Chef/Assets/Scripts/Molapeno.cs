using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molapeno : PlayerPower
{
    [Header("Malapeno Things")]
    public float baseDamagePerSecond;
    public float baseDuration;
    public float baseRadius;
    public float launchAngle;
    public float launchForce;
    [Space]
    public GameObject MolapenoPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();


    }
}
