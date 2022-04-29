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
        setupWaypoints();
        setupEncounter();

        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        enemyStunHandler = GetComponent<EnemyStunHandler>();
        enemyCanvas = GetComponentInChildren<EnemyCanvas>(true);
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
        //Check if nearby enemies are aggrod
        if (Vector3.Distance(player.transform.position, transform.position) < aggroRange)
            becomeAggro();
        foreach (EnemyBehaviorTree enemyBehaviorTree in enemyBehaviorTrees)
        {
            if (enemyBehaviorTree.isAggrod())
                becomeAggro();
            if (enemyBehaviorTree.isSpawnRange())
                simpleFlag = false;
        }
        if (simpleFlag == true)
            becomeDeAggro();
        simpleFlag = true;

        //Sound effects
        if (idleSound == null && idleSoundEffect != null && Vector3.Distance(transform.position, player.position) <= 200)
            idleSound = soundEffectSpawner.MakeFollowingSoundEffect(transform, idleSoundEffect[0]);

        return MoveReset.status = Node.STATUS.SUCCESS;
    }
    
    public override Node.STATUS checkAngleRange()
    {
        if (aggrod)
            return base.checkAngleRange();
        return CheckAngleRange.status = Node.STATUS.FAILURE;
    }

    public override Node.STATUS attackProjectile()
    {
        body[1].LookAt(player);
        return base.attackProjectile();
    }
}
