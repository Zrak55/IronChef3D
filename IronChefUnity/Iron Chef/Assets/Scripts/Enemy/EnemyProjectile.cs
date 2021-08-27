using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Tooltip("Gameobject the enemy creates with its attacks.")]
    [SerializeField] private GameObject projectile;
    //private Animator animator;
    private EnemyMove enemyMove;
    private Vector3 currentPosition;
    private bool coroutineRunning;

    private void OnEnable()
    {
        //There isn't an animator yet
        //animator = gameObject.GetComponent<Animator>();
        enemyMove = gameObject.GetComponent<EnemyMove>();
    }

    private void Update()
    {
        if (enemyMove.isAggro == true && coroutineRunning == false)
            StartCoroutine("Projectile");
        else if (coroutineRunning == true)
        {
            StopCoroutine("Projectile");
            coroutineRunning = false;
        }
    }

    IEnumerator Projectile()
    {
        currentPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        Instantiate(projectile, currentPosition, gameObject.transform.rotation, gameObject.transform);
        coroutineRunning = true;
        yield return new WaitForSeconds(10f);
    }
}
