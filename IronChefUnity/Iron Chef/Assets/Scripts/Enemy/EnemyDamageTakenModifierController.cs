using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageTakenModifierController : MonoBehaviour
{

    public List<DamageTakenModifier> modifiers = new List<DamageTakenModifier>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tickMods();
    }

    public void AddMod(DamageTakenModifier.ModifierName name, float amount, float duration)
    {
        DamageTakenModifier newMod = new DamageTakenModifier();
        newMod.name = name;
        newMod.amount = amount;
        newMod.duration = duration;

        modifiers.Add(newMod);
    }


    private void tickMods()
    {
        var removeList = new List<DamageTakenModifier>();
        foreach(var mod in modifiers)
        {
            if(mod.duration != IronChefUtils.InfiniteDuration)
            {
                mod.duration -= Time.deltaTime;
                if (mod.duration <= 0)
                    removeList.Add(mod);
            }
        }
        foreach(var remMod in removeList)
        {
            modifiers.Remove(remMod);
        }
    }

    public void removeMod(DamageTakenModifier.ModifierName name)
    {
        int i = 0;
        while(i < modifiers.Count)
        {
            if(modifiers[i].name == name)
            {
                modifiers.RemoveAt(i);
            }
            else
            {
                i++;
            }

        }
    }

    public float getMultiplier()
    {
        float amount = 1;
        foreach(var mod in modifiers)
        {
            amount += mod.amount;
        }
        return amount;
    }
    public void DoModifierSpecials(float originalDamageTaken)
    {
        foreach (var mod in modifiers)
        {
            mod.SpecialTakeDamageEffect(originalDamageTaken);
        }
    }


}
