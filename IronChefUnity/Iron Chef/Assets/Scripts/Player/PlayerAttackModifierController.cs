using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModifierController : MonoBehaviour
{
    public List<PlayerAttackHitModifier> HitModifiers;
    List<PlayerAttackHitModifier> RemoveHitModifiers;

    private void Awake()
    {
        HitModifiers = new List<PlayerAttackHitModifier>();
        RemoveHitModifiers = new List<PlayerAttackHitModifier>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHitModifiers();
    }


    void UpdateHitModifiers()
    {
        foreach (var mod in HitModifiers)
        {
            if (mod.duration != IronChefUtils.InfiniteDuration)
            {
                mod.duration -= Time.deltaTime;
                if (mod.duration <= 0)
                {
                    RemoveHitModifiers.Add(mod);
                }
            }
        }
        foreach (var mod in RemoveHitModifiers)
        {
            HitModifiers.Remove(mod);
        }
        RemoveHitModifiers.Clear();
    }
}
