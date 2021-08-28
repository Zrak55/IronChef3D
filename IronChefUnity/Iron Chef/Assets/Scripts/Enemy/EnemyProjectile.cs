using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Tooltip("Gameobject the enemy creates with its attacks.")]
    [SerializeField] private GameObject projectile;
    [Tooltip("Vector3 indicating the location the projectile spawns relative to the enemy.")]
    [SerializeField] private Vector3 spawn;
    //private Animator animator;
    private EnemyMove enemyMove;

    private void OnEnable()
    {
        //There isn't an animator yet
        //animator = gameObject.GetComponent<Animator>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
    }

    private void Update()
    {
        //Later on, add logic here for when enemy attacks. Different enemies will attack at different times in different ways.
        if (enemyMove.isAggro == true)
        {
            Invoke("projectileAttack", 2f);
            this.enabled = false;
        }
    }

    private void projectileAttack()
    {
        Instantiate(projectile, gameObject.transform.position + spawn, gameObject.transform.rotation);
    }
}
