using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCostCooldownManager))]
[RequireComponent(typeof(CharacterMover))]
public class PlayerAttackController : MonoBehaviour
{
    [Header("Basic Attacks")]
    private bool attacking = false;
    public Animator animator;

    public PlayerBasicAttackbox[] PlayerBasics;
    public int currentPlayerBasic = 0;

    public GameObject[] PlayerBasicWeaponModels;

    [HideInInspector]
    public bool canAct = true;
    bool canBasicAttack = true;

    [Header("Frying Pan")]
    public GameObject FryingPanPrefab;

    [Header("PowerInfo")]
    public Transform throwPoint;

    public SoundEffectSpawner manager;


    PlayerCostCooldownManager CDandCost;
    CharacterMover mover;


    private float maxAttackTimer = 3f;
    private float attackTimer = 0f;

    float targetOverrideWeight = 0;

    public GameObject[] BasicTrails;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        currentPlayerBasic = 0;
        animator.SetInteger("BasicAttackNum", 0);
        CDandCost = GetComponent<PlayerCostCooldownManager>();
        mover = GetComponent<CharacterMover>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canAct)
        {

            CheckBasicWeaponSwap();
            CheckBasic();
            CheckFryingPan();
            CheckPower();


            animator.SetLayerWeight(animator.GetLayerIndex("Mid Layer"), 1f-Mathf.Clamp(animator.GetFloat("Speed"), 0, 1));

            if(animator.GetLayerWeight(animator.GetLayerIndex("Override Layer")) != targetOverrideWeight)
            {
                float tickRate = Time.deltaTime * 10;
                if (targetOverrideWeight < animator.GetLayerWeight(animator.GetLayerIndex("Override Layer")))
                {
                    tickRate *= -1;
                }
                animator.SetLayerWeight(animator.GetLayerIndex("Override Layer"), Mathf.Clamp(animator.GetLayerWeight(animator.GetLayerIndex("Override Layer")) + tickRate, 0, 1));
            }

        }
    }




    public void TurnBasicAttackTrailOn()
    {
        foreach(var ps in BasicTrails[currentPlayerBasic].GetComponentsInChildren<ParticleSystem>())
        {
            ps.Play();
        }
        foreach (var tr in BasicTrails[currentPlayerBasic].GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = true;
        }
        var psbt = BasicTrails[currentPlayerBasic].GetComponent<ParticleSystem>();
        var trbt = BasicTrails[currentPlayerBasic].GetComponent<TrailRenderer>();
        if (psbt != null)
            psbt.Play();
        if (trbt != null)
            trbt.emitting = true;
    }

    public void TurnBasicAttackTrailOff()
    {
        foreach (var ps in BasicTrails[currentPlayerBasic].GetComponentsInChildren<ParticleSystem>())
        {
            ps.Stop();
        }
        foreach (var tr in BasicTrails[currentPlayerBasic].GetComponentsInChildren<TrailRenderer>())
        {
            tr.emitting = false;
        }
        var psbt = BasicTrails[currentPlayerBasic].GetComponent<ParticleSystem>();
        var trbt = BasicTrails[currentPlayerBasic].GetComponent<TrailRenderer>();
        if (psbt != null)
            psbt.Stop();
        if (trbt != null)
            trbt.emitting = false;

    }


    private void CheckBasicWeaponSwap()
    {
        if(!attacking && canBasicAttack)
        {
            bool swapped = false;
            float input = InputControls.controls.Gameplay.SwapBasicWeapon.ReadValue<float>();
            if (input > 0)
            {
                currentPlayerBasic++;
                if (currentPlayerBasic == 3)
                    currentPlayerBasic = 0;
                swapped = true;
            }
            else if (input < 0)
            {
                currentPlayerBasic--;
                if (currentPlayerBasic == -1)
                    currentPlayerBasic = 2;
                swapped = true;
            }

            if (swapped)
            {
                animator.SetInteger("BasicAttackNum", currentPlayerBasic);
                ActivateBasicWeapon();
                StartCoroutine(basicAttackDelay());
                FindObjectOfType<PlayerHUDManager>().SetWeaponImage(currentPlayerBasic);

            }
        }
        
    }

    public void ActivateBasicWeapon()
    {
        DeactivateBasicWeapon();
        PlayerBasicWeaponModels[currentPlayerBasic].SetActive(true);
    }

    public void DeactivateBasicWeapon()
    {
        foreach (var w in PlayerBasicWeaponModels)
        {
            w.SetActive(false);
        }
    }

    private IEnumerator basicAttackDelay()
    {
        canBasicAttack = false;
        yield return new WaitForSeconds(0.5f);
        canBasicAttack = true;
    }


    public void AddAttackSpeed(float amount)
    {
        animator.speed += amount;
        foreach(var pb in PlayerBasics)
        {
            pb.attackAnimTime *= (1 - amount);
        }
    }
    public void RemoveAttackSpeed(float amount)
    {
        animator.speed -= amount;

        foreach (var pb in PlayerBasics)
        {
            pb.attackAnimTime /= (1 - amount);
        }
    }

    private void CheckBasic()
    {
        if(!attacking && canBasicAttack)
        {

            if (InputControls.controls.Gameplay.BasicAttack.triggered)
            {
                PerformBasic();
            }
        }
    }
    private void PerformBasic()
    {
        attacking = true;

        targetOverrideWeight = 1;
        animator.SetTrigger("BasicAttack");
        animator.SetInteger("BasicAttackNum", currentPlayerBasic);

        Invoke("DoneAttacking", PlayerBasics[currentPlayerBasic].attackAnimTime);

    }


    public void BasicHitOn()
    {
        PlayerBasics[currentPlayerBasic].HitOn();
    }
    public void BasicHitOff()
    {

        PlayerBasics[currentPlayerBasic].HitOff();
    }

    public void DoneAttacking()
    {
        //animator.SetBool("BasicAttack", false);
        Invoke("ResetOverrideWeight", 0.1f);
        attacking = false;
    }

    public void ResetOverrideWeight()
    {
        if (!attacking)
            targetOverrideWeight = 0;
    }


    private void CheckFryingPan()
    {
        if (InputControls.controls.Gameplay.FryingPan.triggered)
        {
            if(!attacking && CDandCost.FryingPanOnCooldown == false && !mover.IsRolling())
            {
                attacking = true;
                animator.SetBool("RangedAttack", true);
                targetOverrideWeight = 1;
            }
        }
    }

    public void PerformFryingPan()
    {
        var fp = Instantiate(FryingPanPrefab, throwPoint.position, mover.model.transform.rotation);;
        fp.GetComponent<PlayerProjectile>().FireProjectile(fp.transform.position + fp.transform.forward);
        CDandCost.SetFryingPanCooldown();
    }
    public void DoneFryingPan()
    {
        attacking = false;
        animator.SetBool("RangedAttack", false);
        ActivateBasicWeapon();
        targetOverrideWeight = 0;
    }

    private void CheckPower()
    {
        //Debug.Log("Input: " + InputControls.controls.Gameplay.UsePower.triggered + " On CD: " + CDandCost.PowerOnCooldown + " Attacking: " + attacking);
        if (InputControls.controls.Gameplay.UsePower.triggered && CDandCost.PowerOnCooldown == false && !attacking && !mover.IsRolling())
        {
            attacking = true;
            animator.SetBool("UsingPower", true);
            targetOverrideWeight = 1;
            GetComponent<PlayerPower>().PerformPower();
            Invoke("EndPower", GetComponent<PlayerPower>().powerInformation.powerAnimDuration);
        }
    }
    public void EndPower()
    {
        DoneAttacking();
        animator.SetBool("UsingPower", false);
        ActivateBasicWeapon();
    }
}
