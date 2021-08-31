using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicAttackbox : MonoBehaviour
{
    public bool CanHit = false;
    public List<EnemyHitpoints> enemiesHit;
    private PlayerAttackModifierController modifier;

    public bool IsCleave = false;

    public float damage;

    private void Awake()
    {
        enemiesHit = new List<EnemyHitpoints>();
        modifier = GetComponentInParent<PlayerAttackModifierController>();
    }

    public void HitOn()
    {
        CanHit = true;
    }
    public void HitOff()
    {
        CanHit = false;
        enemiesHit.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        DoCollisionThings(other);
    }

    private void DoCollisionThings(Collider other)
    {
        if (CanHit)
        {
            var opponent = other.gameObject.GetComponentInParent<EnemyHitpoints>();
            if (opponent != null)
            {
                if (!enemiesHit.Contains(opponent))
                {
                    if (IsCleave || enemiesHit.Count < 1)
                    {
                        Debug.Log("Hit!");
                        enemiesHit.Add(opponent);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        foreach(var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if(mod.slowAmount > 0)
                            {
                                var oppSpeed = opponent.GetComponent<EnemySpeedController>();
                                bool alreadyThere = false;
                                foreach(var speedMod in oppSpeed.Modifiers)
                                {
                                    if(mod.slowName == speedMod.effectName)
                                    {
                                        if(mod.slowDuration > speedMod.duration)
                                        {
                                            alreadyThere = true;
                                            speedMod.duration = mod.slowDuration;
                                            break;
                                        }
                                    }
                                }
                                if(!alreadyThere)
                                {
                                    SpeedEffector slow = new SpeedEffector();
                                    slow.duration = mod.slowDuration;
                                    slow.percentAmount = -mod.slowAmount;
                                    slow.effectName = mod.slowName;
                                    oppSpeed.Modifiers.Add(slow);
                                }

                            }
                        }
                        dmgToDeal = damage * (1 + dmgMod);

                        

                        opponent.TakeDamage(dmgToDeal);
                    }
                }
            }
        }
    }
}
