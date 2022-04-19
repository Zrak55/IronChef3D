using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterMover))]
public class PlayerSpeedController : MonoBehaviour
{
    CharacterMover mover;
    
    public List<SpeedEffector> Modifiers = new List<SpeedEffector>();
    List<SpeedEffector> removeList;

    float totalMod;

    private void Awake()
    {
        removeList = new List<SpeedEffector>();
        mover = GetComponent<CharacterMover>();
    }

    private void Update()
    {
        totalMod = 1;
        foreach(var mod in Modifiers)
        {
            totalMod += mod.percentAmount;
            if(mod.duration != IronChefUtils.InfiniteDuration)
            {
                mod.duration -= Time.deltaTime;
                if (mod.duration <= 0)
                {
                    removeList.Add(mod);
                }
            }
        }
        foreach(var mod in removeList)
        {
            Modifiers.Remove(mod);
        }
        removeList.Clear();

        if (totalMod < 0)
            totalMod = 0;

        mover.speed = mover.GetBaseSpeed() * totalMod;
        mover.acceleration = Mathf.Max(mover.GetBaseAcceleration() * totalMod, mover.GetBaseAcceleration());
    }

    public void RemoveSpeedEffector(SpeedEffector.EffectorName speedName)
    {
        var rl = new List<SpeedEffector>();
        foreach(var s in Modifiers)
        {
            if (s.effectName == speedName)
            {
                rl.Add(s);
            }
        }
        foreach(var rs in rl)
        {
            Modifiers.Remove(rs);
        }
    }

    public float GetModifer()
    {
        return totalMod;
    }
}
