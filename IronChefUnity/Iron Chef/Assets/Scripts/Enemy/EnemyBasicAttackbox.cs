using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicAttackbox : MonoBehaviour
{
    //Copied and pasted from PlayerBasicAttackbox (with small edits)
    [Tooltip("Collider representing the enemies' attack range.")]
    public Collider attackbox;
    [HideInInspector] public bool CanHit = false;
    [HideInInspector] public List<PlayerHitpoints> playersHit;
    private EnemyAttackModifierController modifier;
    [SerializeField] public SoundEffectSpawner.SoundEffect sound = SoundEffectSpawner.SoundEffect.Cleaver;
    public float damage;
    [HideInInspector] private EnemyBehaviorTree enemyBehaviorTree;

    //bool hasPlayedSound = false;


    public SoundEffectSpawner.SoundEffect swingingSound = SoundEffectSpawner.SoundEffect.Cleaver;

    private void Awake()
    {
        playersHit = new List<PlayerHitpoints>();
        enemyBehaviorTree = GetComponentInParent<EnemyBehaviorTree>();

    }
    private void Start()
    {
        modifier = GetComponentInParent<EnemyAttackModifierController>();
    }

    private void FixedUpdate()
    {
        if (CanHit && attackbox != null)
        {
            DoCollisionThings();
        }
    }

    public void HitOn()
    {
        CanHit = true;
    }
    public void HitOff()
    {
        CanHit = false;
        playersHit.Clear();
    }

    public void PlayAttackSound(int value)
    {
        enemyBehaviorTree.playSound(value);
    }

    public void SimpleFlag()
    {
        enemyBehaviorTree.simpleFlag = enemyBehaviorTree.simpleFlag ? false : true;
    }

    public void DoCollisionThings()
    {
        var hits = IronChefUtils.GetCastHits(attackbox);
        foreach (var hit in hits)
        {
            var player = hit.GetComponentInParent<PlayerHitpoints>();
            if (player != null)
            {
                if (!playersHit.Contains(player))
                {
                    if (playersHit.Count < 1)
                    {
                        playersHit.Add(player);

                        var dmgMod = 0f;
                        float dmgToDeal;
                        foreach (var mod in modifier.HitModifiers)
                        {
                            dmgMod += mod.damageIncrease;
                            if (mod.slowAmount > 0)
                            {
                                var oppSpeed = player.GetComponent<PlayerSpeedController>();
                                IronChefUtils.AddSlow(oppSpeed, mod.slowAmount, mod.slowDuration, mod.slowName);


                            }
                        }
                        dmgToDeal = damage * (1 + dmgMod);
                        player.TakeDamage(dmgToDeal, sound);

                        
                    }
                }
            }
        }
    }

    public void PlaySwingSound()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, swingingSound);
    }
}
