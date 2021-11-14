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

    private void OnEnable()
    {
        anim = gameObject.GetComponent<Animator>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
        if (enemyMove == null)
            isAggro = true;
    }

    public void projectileAttack()
    {
        Instantiate(projectile, gameObject.transform.position + spawn, gameObject.transform.rotation);
        if (!isAggro)
            anim.SetTrigger("Projectile");
    }
}
