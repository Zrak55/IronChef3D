using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealOverTimeManager : MonoBehaviour
{
    PlayerHitpoints phealth;
    List<HealOverTimeEffector> effectors;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddHealOverTime(HealOverTimeEffector effect)
    {

    }
    public void AddHealOverTime(float hps, float duration)
    {
        HealOverTimeEffector newEff = new HealOverTimeEffector();
        newEff.HealPerSecond = hps;
        newEff.Duration = duration;
    }
}
