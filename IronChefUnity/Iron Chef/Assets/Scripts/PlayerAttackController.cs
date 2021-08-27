using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentPlayerBasic = 0;
        animator.SetInteger("BasicAttackNum", 0);

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
            if (input < 0)
            {
                currentPlayerBasic--;
                if (currentPlayerBasic == -1)
                    currentPlayerBasic = 2;
                swapped = true;
            }

            if (swapped)
            {
                animator.SetInteger("BasicAttackNum", currentPlayerBasic);
                foreach(var w in PlayerBasicWeaponModels)
                {
                    w.SetActive(false);
                }
                PlayerBasicWeaponModels[currentPlayerBasic].SetActive(true);
                StartCoroutine(basicAttackDelay());

            }
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
        //TODO: Different anim for different auto
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
            if(!attacking)
            {
                var fp = Instantiate(FryingPanPrefab, transform.position, transform.rotation);
                //fp.GetComponent<PlayerProjectile>().FireProjectile()

            }
        }
    }
}
