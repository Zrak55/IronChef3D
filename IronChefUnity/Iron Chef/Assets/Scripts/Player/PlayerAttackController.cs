using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCostCooldownManager))]
public class PlayerAttackController : MonoBehaviour
{
    [Header("Basic Attacks")]
    private bool attacking = false;
    private Animator animator;

    public PlayerBasicAttackbox[] PlayerBasics;
    public int currentPlayerBasic = 0;

    public GameObject[] PlayerBasicWeaponModels;

    bool canBasicAttack = true;

    [Header("Frying Pan")]
    public GameObject FryingPanPrefab;

    [Header("PowerInfo")]
    public Transform throwPoint;



    PlayerCostCooldownManager CDandCost;
    CharacterMover mover;


    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        CheckBasicWeaponSwap();
        CheckBasic();
        CheckFryingPan();
        CheckPower();
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

            }
        }
        
    }

    private void ActivateBasicWeapon()
    {
        DeactivateBasicWeapon();
        PlayerBasicWeaponModels[currentPlayerBasic].SetActive(true);
    }

    private void DeactivateBasicWeapon()
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
        animator.SetBool("BasicAttack", true);
        animator.SetInteger("BasicAttackNum", currentPlayerBasic);
    }

    private void BasicHitOn()
    {
        PlayerBasics[currentPlayerBasic].HitOn();
    }
    private void BasicHitOff()
    {

        PlayerBasics[currentPlayerBasic].HitOff();
    }

    private void DoneAttacking()
    {
        attacking = false;
        animator.SetBool("BasicAttack", false);
    }

    private void CheckFryingPan()
    {
        if (InputControls.controls.Gameplay.FryingPan.triggered)
        {
            if(!attacking && CDandCost.FryingPanOnCooldown == false)
            {
                attacking = true;
                animator.SetBool("RangedAttack", true);
            }
        }
    }

    private void PerformFryingPan()
    {
        var fp = Instantiate(FryingPanPrefab, throwPoint.position, mover.model.transform.rotation);;
        fp.GetComponent<PlayerProjectile>().FireProjectile(fp.transform.position + fp.transform.forward);
        CDandCost.SetFryingPanCooldown();
    }
    private void DoneFryingPan()
    {
        attacking = false;
        animator.SetBool("RangedAttack", false);
        ActivateBasicWeapon();
    }

    private void CheckPower()
    {
        //Debug.Log("Input: " + InputControls.controls.Gameplay.UsePower.triggered + " On CD: " + CDandCost.PowerOnCooldown + " Attacking: " + attacking);
        if (InputControls.controls.Gameplay.UsePower.triggered && CDandCost.PowerOnCooldown == false && !attacking)
        {
            attacking = true;
            animator.SetBool("UsingPower", true);
        }
    }
    private void EndPower()
    {
        attacking = false;
        animator.SetBool("UsingPower", false);
        ActivateBasicWeapon();
    }
}
