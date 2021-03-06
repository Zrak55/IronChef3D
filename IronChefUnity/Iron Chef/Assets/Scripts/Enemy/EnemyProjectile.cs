using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Tooltip("Gameobject the enemy creates with its attacks.")]
    [SerializeField] private GameObject projectile;
    [Tooltip("Vector3 indicating the location the projectile spawns relative to the enemy.")]
    [SerializeField] private Vector3 spawn;
    private Animator anim;
    private EnemyMove enemyMove;
    //If there is no enemyMove attached, this will become true and the enemy will always attack.
    //Honestly unsure if enemyMove is required at all, this section of scripts is dated.
    private bool isAggro = false;

    public SoundEffectSpawner.SoundEffect ProjectileSoundEffect;

    EnemyVFXController vfx;
    [SerializeField] private int vfxNumber = -1;

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
        if (enemyMove == null)
            isAggro = true;

        vfx = GetComponent<EnemyVFXController>();
        if (vfx == null)
            vfx = GetComponentInParent<EnemyVFXController>();
        if (vfx == null)
            vfx = GetComponentInChildren<EnemyVFXController>();

    }

    public void projectileAttack()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, ProjectileSoundEffect);
        Instantiate(projectile, gameObject.transform.position + spawn, gameObject.transform.rotation);
        vfx?.StartEffect(vfxNumber);
        if (!isAggro)
            anim.SetTrigger("Projectile");
    }

    public void projectileAttack(Vector3 facing)
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, ProjectileSoundEffect);
        Instantiate(projectile, gameObject.transform.position + spawn, Quaternion.Euler(facing));
        if (!isAggro)
            anim.SetTrigger("Projectile");
    }
}
