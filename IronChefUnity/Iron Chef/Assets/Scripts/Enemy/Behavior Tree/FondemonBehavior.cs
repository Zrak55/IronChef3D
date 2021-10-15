using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondemonBehavior : MonoBehaviour
{
    //Behavior for the Fondemon prefab, not meant for other use.
    //It is simpler than most of the other behaviors because the fondemon doesn't move
    BehaviorTree fondemonBehaviorTree;
    [Tooltip("Float for the maximum distance the fondemon will begin to attack from.")]
    [SerializeField] private float attackRange;
    [Tooltip("Float for the time it takes the fondemon to attack. (Will be replaced later with animation).")]
    [SerializeField] private float attackTime;
    private Transform player;
    private Animator animator;
    private EnemyHitpoints enemyHitpoints;
    private EnemyProjectile enemyProjectile;
    //Nodes for the behavior tree. Will be adding more later.
    private Node CheckAttack, CheckPlayer, PlayerAttackRange, Attack, Idle;
    //The spawn location of the enemy is automatically set based on scene placement.
    private Vector3 startPosition;
    //Ensure the enemy doesn't start a new attack in the middle of an old one.
    private bool isAttacking = false;

    private void Start()
    {
        startPosition = transform.position;
        enemyProjectile = GetComponent<EnemyProjectile>();
        enemyHitpoints = GetComponent<EnemyHitpoints>();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;

        //Setup leaf nodes
        PlayerAttackRange = new Leaf("Player in Attack Range?", checkPlayerAttackRange);
        Attack = new Leaf("Attack", attack);
        Idle = new Leaf("Idle", idle);

        //Setup sequence nodes and root
        CheckAttack = new Sequence("Attack Sequence", PlayerAttackRange, Attack);
        CheckPlayer = new Selector("Player Location Sequence", CheckAttack, Idle);
        fondemonBehaviorTree = new BehaviorTree(CheckPlayer);
        fondemonBehaviorTree.printTree();
    }

    //TODO: Fix multiple things same frame.
    private void Update()
    {
        fondemonBehaviorTree.behavior();
    }

    //This is intended to be running in the update function through the behavior tree.
    public Node.STATUS attack()
    {
        transform.LookAt(player);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        //When the attack animation has finished this will play.
        if (Attack.status == Node.STATUS.RUNNING && !isAttacking)
        {
            Attack.status = Node.STATUS.SUCCESS;
            return Attack.status;
        }
        //If we aren't already attack and the cd is done, then attack.
        else if (!isAttacking)
        {
            FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Fondemon);
            isAttacking = true;
            animator.SetTrigger("Attack");
            Invoke("attackEnd", attackTime);
            enemyProjectile.projectileAttack();
        }
        Attack.status = Node.STATUS.RUNNING;
        return Attack.status;
    }

    public Node.STATUS idle()
    {
        return Node.STATUS.RUNNING;
    }

    public Node.STATUS checkPlayerAttackRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < attackRange)
            PlayerAttackRange.status = Node.STATUS.SUCCESS;
        else
            PlayerAttackRange.status = Node.STATUS.FAILURE;
        return PlayerAttackRange.status;
    }
    private void attackEnd()
    {
        isAttacking = false;
    }
}
