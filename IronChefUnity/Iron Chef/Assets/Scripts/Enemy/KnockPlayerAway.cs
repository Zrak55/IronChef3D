using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockPlayerAway : MonoBehaviour
{
    //Copied and pasted from PlayerBasicAttackbox
    public bool CanHit = false;
    public List<CharacterMover> playersHit;
    private EnemyAttackModifierController modifier;
    //No sound yet
    //public SoundEffectSpawner.SoundEffect soundEffect;
    //private SoundEffectSpawner sfx;
    public Collider myCollider;

    public bool IsCleave = false;

    public float force;

    //bool hasPlayedSound = false;

    private void Awake()
    {
        playersHit = new List<CharacterMover>();
        //sfx = FindObjectOfType<SoundEffectSpawner>();

    }
    private void Start()
    {
        modifier = GetComponentInParent<EnemyAttackModifierController>();
    }

    private void Update()
    {
        /*if (hasPlayedSound)
        {
            hasPlayedSound = CanHit;
        }*/


    }

    private void FixedUpdate()
    {
        if (CanHit)
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

    private void OnTriggerEnter(Collider other)
    {
    }

    private void DoCollisionThings()
    {
        var hits = IronChefUtils.GetCastHits(myCollider);
        foreach (var hit in hits)
        {
            var player = hit.GetComponentInParent<CharacterMover>();
            if (player != null)
            {
                if (!playersHit.Contains(player))
                {
                    if (IsCleave || playersHit.Count < 1)
                    {
                        playersHit.Add(player);

                        Vector3 dir = player.transform.position - transform.position;
                        dir.y = 0;
                        player.ForceDirection((dir * force));

                        
                    }
                }
            }
        }
    }
}

