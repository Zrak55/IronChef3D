using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    private bool attacking = false;
    private Animator animator;

    public PlayerBasicAttackbox[] PlayerBasics;
    public int currentPlayerBasic = 0;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckBasic();
    }

    private void CheckBasic()
    {
        if(!attacking)
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
}
