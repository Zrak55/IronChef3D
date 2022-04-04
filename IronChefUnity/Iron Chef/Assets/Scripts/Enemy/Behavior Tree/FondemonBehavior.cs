using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondemonBehavior : EnemyBehaviorTree
{
    BehaviorTree fondemonBehaviorTree;
    private Node CheckAttack, CheckPlayer;
    private Transform[] body;

    private void Start()
    {
        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.Find("Player").transform;
        musicManager = FindObjectOfType<MusicManager>();
        soundEffectSpawner = SoundEffectSpawner.soundEffectSpawner;

        //Setup leaf nodes (Note: the fondemon must have attackAngle set to 0)
        MoveReset = new Leaf("Don't move", moveReset);
        CheckAngleRange = new Leaf("Player in Attack Range?", checkAngleRange);
        AttackProjectile = new Leaf("Attack", attackProjectile);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", CheckAngleRange, AttackProjectile);
        CheckPlayer = new Sequence("Still Sequence", MoveReset, CheckAttack);
        fondemonBehaviorTree = new BehaviorTree(CheckPlayer);

        body = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        fondemonBehaviorTree.behavior();
    }

    public override Node.STATUS moveReset()
    {
        //Before anything else, check if stunned
        if (enemyStunHandler.IsStunned())
        {
            //There isn't really an animation state for this
            animator.Play("Base Layer.Idle", 0, 0);
            return MoveReset.status = Node.STATUS.RUNNING;
        }

        //Music and sound effects
        if (aggrod)
            PlayerHitpoints.CombatCount--;
        aggrod = false;
        if (idleSound == null && idleSoundEffect != null && Vector3.Distance(transform.position, player.position) <= 200)
            idleSound = soundEffectSpawner.MakeFollowingSoundEffect(transform, idleSoundEffect[0]);

        return MoveReset.status = Node.STATUS.SUCCESS;
    }

    public override Node.STATUS attackProjectile()
    {
        if (!aggrod)
            PlayerHitpoints.CombatCount++;
        aggrod = true;

        body[1].LookAt(player);
        return base.attackProjectile();
    }
}
