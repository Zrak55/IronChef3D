using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyMove))]
public class EnemySpeedController : MonoBehaviour
{
    EnemyMove mover;
    [SerializeField]
    public List<SpeedEffector> Modifiers;
    List<SpeedEffector> removeList;

    private void Awake()
    {
        Modifiers = new List<SpeedEffector>();
        removeList = new List<SpeedEffector>();
        mover = GetComponent<EnemyMove>();
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

        mover.SetCurrentSpeed(mover.GetStartSpeed() * totalMod);
    }
}
