using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpeedController : MonoBehaviour
{
    GenericEnemyBehavior mover;
    [SerializeField]
    public List<SpeedEffector> Modifiers = new List<SpeedEffector>();
    List<SpeedEffector> removeList;

    private void Awake()
    {
        removeList = new List<SpeedEffector>();
        mover = GetComponent<GenericEnemyBehavior>();
    }

    private void Update()
    {
        float totalMod = 1;
        foreach (var mod in Modifiers)
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
        foreach (var mod in removeList)
        {
            Modifiers.Remove(mod);
        }
        removeList.Clear();

        //mover.SetCurrentSpeed(mover.GetStartSpeed() * totalMod);
    }
}
