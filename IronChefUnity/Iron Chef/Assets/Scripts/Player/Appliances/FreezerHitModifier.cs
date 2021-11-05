using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerHitModifier : PlayerAttackHitModifier
{
    public int limit;
    public float stunDelay;
    public float stunTime;

    List<Mark> allMarks = new List<Mark>();
    public override void SpecialTickAction()
    {
        base.SpecialTickAction();
        for (int i = 0; i < allMarks.Count; i++)
        {
            if (allMarks[i].enemy == null)
            {
                allMarks.RemoveAt(i);
                i--;
            }
            else
            {
                var tmp = allMarks[i];
                tmp.delay -= Time.deltaTime;
                allMarks[i] = tmp;
            }
        }
    }

    public override void DoSpecialModifier(EnemyHitpoints enemyHP, float damage)
    {
        base.DoSpecialModifier(enemyHP, damage);
        bool foundEnemy = false;
        for(int i = 0; i < allMarks.Count; i++)
        {
            if(allMarks[i].enemy == null)
            {
                allMarks.RemoveAt(i);
                i--;
            }
            else
            {
                var e = enemyHP.GetComponent<EnemyStunHandler>();
                if(allMarks[i].enemy == e)
                {
                    foundEnemy = true;
                    if(allMarks[i].delay <= 0)
                    {
                        var tmp = allMarks[i];
                        tmp.numMarks++;

                        if(tmp.numMarks >= limit)
                        {
                            tmp.numMarks = 0;
                            tmp.delay = stunDelay;
                            tmp.enemy.Stun(stunTime);
                        }
                        allMarks[i] = tmp;
                    }
                    break;
                }
                
            }
        }
        if(!foundEnemy)
        {
            var nm = new Mark();
            nm.enemy = enemyHP.GetComponent<EnemyStunHandler>();
            nm.numMarks = 1;
            nm.delay = 0;
            allMarks.Add(nm);
        }
    }


    struct Mark
    {
        public EnemyStunHandler enemy;
        public int numMarks;
        public float delay;
    }
}
