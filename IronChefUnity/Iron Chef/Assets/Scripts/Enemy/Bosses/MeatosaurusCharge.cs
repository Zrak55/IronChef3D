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

    public Collider collider;


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
        }
    }

    void DoChargeThings()
    {
        Debug.Log("Charging");
        var list = IronChefUtils.GetCastHits(collider, "Terrain");
        foreach (var i in list)
        {
            if (i.gameObject.name != "Floor")
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexWallHit);
                pcam.ShakeCam(5, 2);
                StopCharge();

            }


        }

        //Move
        transform.position = Vector3.MoveTowards(transform.position, transform.position + (targetFacing * 1000), chargeSpeed * speed.GetMod() * Time.fixedDeltaTime);

        var ray = new Ray(transform.position + (2 * Vector3.up), transform.forward);
        if(Physics.Raycast(ray, 5 * collider.bounds.size.z * chargeSpeed * speed.GetMod() * Time.fixedDeltaTime, 1 << LayerMask.NameToLayer("Terrain")))
        {
            //StopCharge();
        }
        //Debug.DrawRay(transform.position + (2 * Vector3.up), transform.forward * (10 * chargeSpeed * speed.GetMod() * Time.fixedDeltaTime), Color.red, Time.fixedDeltaTime * 1.1f);
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

        }
    }

}
