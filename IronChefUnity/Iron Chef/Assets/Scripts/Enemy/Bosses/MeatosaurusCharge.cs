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
    PlayerCamControl pcam;


    Vector3 targetFacing;
    public float chargeSpeed;

    public Collider col;

    public GameObject DestroyRockEffect;

    void Start()
    {
        behavior = GetComponent<MeatosaurusBehavior>();
        speed = GetComponent<EnemySpeedController>();
        animator = GetComponent<Animator>();
        pcam = FindObjectOfType<PlayerCamControl>();
    }
    private void FixedUpdate()
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
            Debug.Log("Starting Charge");
            charging = true;
            targetFacing = transform.forward;
            targetFacing.y = 0;
            ChargeCollider.HitOn();
            ChargeKnockbox.HitOn();
            col.isTrigger = true;
        }
    }

    void DoChargeThings()
    {
        Debug.Log("Charging");

        bool destroyRock = false;
        var list2 = IronChefUtils.GetCastHits(col, "SpecialBossLayer1");
        foreach (var i in list2)
        {
            if (i.gameObject.name != "Floor")
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexWallHit);
                pcam.ShakeCam(5, 2);
                Destroy(Instantiate(DestroyRockEffect, i.transform.position, i.transform.rotation), 5f);
                Destroy(i.transform.parent.gameObject);
                destroyRock = true;
            }


        }


        if (!destroyRock)
        {
            var list = IronChefUtils.GetCastHits(col, "Terrain");
            foreach (var i in list)
            {
                if (i.gameObject.name != "Floor" && i.gameObject.name != "MeatosaurusRockPlayerCol")
                {
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexWallHit);
                    pcam.ShakeCam(5, 2);
                    StopCharge();

                }


            }

            //Move
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (targetFacing * 1000), chargeSpeed * speed.GetMod() * Time.fixedDeltaTime);

            var ray = new Ray(transform.position + (2 * Vector3.up), transform.forward);
            if (Physics.Raycast(ray, 5 * col.bounds.size.z * chargeSpeed * speed.GetMod() * Time.fixedDeltaTime, 1 << LayerMask.NameToLayer("Terrain")))
            {
                //StopCharge();
            }
            //Debug.DrawRay(transform.position + (2 * Vector3.up), transform.forward * (10 * chargeSpeed * speed.GetMod() * Time.fixedDeltaTime), Color.red, Time.fixedDeltaTime * 1.1f);

        }
    }
    void StopCharge()
    {
        if(charging)
        {
            charging = false;
            animator.SetBool("Charge", false);
            ChargeCollider.HitOff();
            ChargeKnockbox.HitOff();
            behavior.attackEnd();
            behavior.StartChargeCD();
            col.isTrigger = false;

        }
    }

}
