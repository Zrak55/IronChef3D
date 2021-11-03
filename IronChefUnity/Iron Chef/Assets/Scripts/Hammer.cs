using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : PlayerPower
{
    float stunTime;

    float width;
    float depth;
    float height;

    float animTime;

    GameObject meatMallet;

    PlayerAttackController pattack;

    BoxCollider aBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();

        var hits = IronChefUtils.GetCastHits(aBox);
        foreach(var h in hits)
        {
            if(h.GetComponent<EnemyStunHandler>() != null)
            {
                h.GetComponent<EnemyStunHandler>().Stun(stunTime);
                break;
            }
        }

    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);

        pattack = GetComponent<PlayerAttackController>();

        stunTime = power.values[0];
        width = power.values[1];
        depth = power.values[2];
        height = power.values[3];
        animTime = power.values[4];

        meatMallet = Instantiate(power.prefabs[0], pattack.throwPoint.parent);
        aBox = meatMallet.AddComponent<BoxCollider>();
        aBox.size = new Vector3(width, height, depth);
        aBox.center = Vector3.zero;
        aBox.isTrigger = true;
        meatMallet.SetActive(false);
    }

    public override void PerformPower()
    {
        base.PerformPower();
        meatMallet.SetActive(true);
        pattack.DeactivateBasicWeapon();
        Invoke("MalletOff", animTime);

    }

    void MalletOff()
    {
        meatMallet.SetActive(false);
        pattack.ActivateBasicWeapon();

    }

    public override void DoRemovalThings()
    {
        base.DoRemovalThings();
        Destroy(meatMallet);
    }
}
