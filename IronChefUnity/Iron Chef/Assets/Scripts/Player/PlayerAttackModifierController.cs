using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackModifierController : MonoBehaviour
{
    public List<PlayerAttackHitModifier> HitModifiers = new List<PlayerAttackHitModifier>();
    List<PlayerAttackHitModifier> RemoveHitModifiers;

    private void Awake()
    {
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
            mod.SpecialTickAction();
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

    public void RemoveHitModifier(PlayerAttackHitModifier.PlayerHitModName mod)
    {
        for (int i = 0; i < HitModifiers.Count; i++)
        {
            if (HitModifiers[i].modName == mod)
            {
                HitModifiers.RemoveAt(i);
                break;
            }
        }
    }


}
