using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenedictRoll : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyBasicAttackbox RollCollider;
    private BenedictBehavior behavior;
    public Collider collider;
    public Animator animator;

    bool startingRoll = false;
    bool rolling = false;
    Vector3 targetFacing;

    public float rollSpeed;

    void Start()
    {
        behavior = GetComponent<BenedictBehavior>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startingRoll)
        {
            if(animator.speed < 1)
            {
                animator.speed = Mathf.Clamp(animator.speed + (Time.deltaTime / 2), 0, 1);
            }
            if(animator.speed == 1)
            {
                LaunchRoll();
            }
        }
    }


    private void FixedUpdate()
    {
        if(rolling)
        {
            //Collision Check
            var list = IronChefUtils.GetCastHits(collider, "Terrain");
            foreach (var i in list)
            {

                targetFacing = Vector3.MoveTowards(targetFacing, FindObjectOfType<CharacterMover>().transform.position - transform.position, Vector3.Angle(targetFacing, FindObjectOfType<CharacterMover>().transform.position - transform.position) / 2);
                targetFacing.y = 0;

            }

            //Phase Check
            list = IronChefUtils.GetCastHits(collider, "SpecialBossLayer1");
            if(list.Count > 0)
            {
                behavior.GoToNextPhase();
            }


            //Move
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (targetFacing * 1000), rollSpeed * Time.deltaTime);
            transform.LookAt(transform.position + targetFacing);

            RollCollider.playersHit.Clear();
            
        }
    }

    private void LaunchRoll()
    {
        startingRoll = false;
        rolling = true;
        targetFacing = transform.forward;
        targetFacing.y = 0;
        RollCollider.HitOn();
        Physics.IgnoreCollision(collider, FindObjectOfType<CharacterController>().GetComponent<Collider>(), true);
        foreach(var c in FindObjectOfType<CharacterMover>().GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, c, true);
        }
    }
    public void BeginRolling(float time)
    {
        animator.speed = 0;
        startingRoll = true;

        Invoke("EndRolling", time);
    }

    public void EndRolling()
    {
        RollCollider.HitOff();
        rolling = false;
        behavior.DoneRolling = true;
        animator.SetBool("Roll", false);
        Physics.IgnoreCollision(collider, FindObjectOfType<CharacterController>().GetComponent<Collider>(), false);
        foreach (var c in FindObjectOfType<CharacterMover>().GetComponents<Collider>())
        {
            Physics.IgnoreCollision(collider, c, false);
        }
    }
}
