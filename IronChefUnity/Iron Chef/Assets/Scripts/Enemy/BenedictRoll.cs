using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenedictRoll : MonoBehaviour
{
    // Start is called before the first frame update

    public EnemyBasicAttackbox RollCollider;
    public KnockPlayerAway RollKnockbox;
    private BenedictBehavior behavior;
    public Collider collider;
    public Animator animator;

    bool startingRoll = false;
    bool rolling = false;
    Vector3 targetFacing;

    bool recentTerrainHit = false;
    bool recentHit = false;

    public Transform RoomCenter;

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
            if(!recentHit)
            {
                var list = IronChefUtils.GetCastHits(collider, "Terrain");
                foreach (var i in list)
                {
                    if(i.gameObject.name != "Floor")
                    {
                        if (recentTerrainHit)
                        {
                            targetFacing = (RoomCenter.position - transform.position).normalized;
                            targetFacing.y = 0;
                        }
                        else
                        {
                            recentTerrainHit = true;
                            Invoke("UndoRecentTerrain", 0.5f);


                            targetFacing = FindObjectOfType<CharacterMover>().transform.position - transform.position;
                            targetFacing.y = 0;
                        }

                        recentHit = true;
                        Invoke("RecentHit", 0.25f);
                    }
                    
                }
            }
            

            //Phase Check
            var slist = IronChefUtils.GetCastHits(collider, "SpecialBossLayer1");
            if(slist.Count > 0)
            {
                behavior.GoToNextPhase();
            }


            //Move
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (targetFacing * 1000), rollSpeed * Time.deltaTime);
            transform.LookAt(transform.position + targetFacing);

            RollCollider.playersHit.Clear();
            RollKnockbox.playersHit.Clear();
        }
    }

    void UndoRecentTerrain()
    {
        recentTerrainHit = false;
    }
    void RecentHit()
    {
        recentHit = false;
    }

    private void LaunchRoll()
    {
        startingRoll = false;
        rolling = true;
        targetFacing = transform.forward;
        targetFacing.y = 0;
        recentTerrainHit = false;
        RollCollider.HitOn();
        RollKnockbox.HitOn();
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
        RollKnockbox.HitOff();
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
