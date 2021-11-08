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
        effectors = new List<HealOverTimeEffector>();
        phealth = GetComponent<PlayerHitpoints>();
    }

    // Update is called once per frame
    void Update()
    {
        TickMods();
    }

    private void TickMods()
    {
        List<HealOverTimeEffector> remList = new List<HealOverTimeEffector>();
        foreach(var e in effectors)
        {
            phealth.RestoreHP(e.HealPerSecond * Time.deltaTime);
            e.Duration -= Time.deltaTime;
            if (e.Duration <= 0)
                remList.Add(e);
        }
        foreach (var r in remList)
            effectors.Remove(r);
    }

    public void AddHealOverTime(HealOverTimeEffector effect)
    {
        effectors.Add(effect);
    }
    public void AddHealOverTime(float hps, float duration)
    {
        HealOverTimeEffector newEff = new HealOverTimeEffector();
        newEff.HealPerSecond = hps;
        newEff.Duration = duration;
        effectors.Add(newEff);
    }
}
