using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeatosaurusCharge : MonoBehaviour
{
    bool charging = false;

    public EnemyBasicAttackbox ChargeCollider;
    public KnockPlayerAway ChargeKnockbox;
    private MeatosaurusBehavior behavior;
    EnemySpeedController speed;
    [SerializeField]
    private Animator animator;


    bool chargeing = false;
    Vector3 targetFacing;
    public float chargeSpeed;

    public Collider collider;


    void Start()
    {
        behavior = GetComponent<MeatosaurusBehavior>();
        speed = GetComponent<EnemySpeedController>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (charging)
        {
            DoChargeThings();
        }
    }

    public void StartCharge()
    {
        if(!charging)
        {
            charging = true;
            targetFacing = transform.forward;
            ChargeCollider.HitOn();
            ChargeKnockbox.HitOn();
        }
    }

    void DoChargeThings()
    {
        var list = IronChefUtils.GetCastHits(collider, "Terrain");
        foreach (var i in list)
        {
            if (i.gameObject.name != "Floor")
            {
                StopCharge();

            }


        }

        //Move
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (targetFacing * 1000), chargeSpeed * speed.GetMod() * Time.deltaTime);


    }
    void StopCharge()
    {
        animator.SetBool("Charge", false);   
        ChargeCollider.HitOff();
        ChargeKnockbox.HitOff();
        behavior.attackEnd();
        behavior.StartChargeCD();
    }

}
