using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileControl : MonoBehaviour
{
    private Vector3 currentPosition;

    private void Update()
    {
        currentPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(currentPosition.x + 1, currentPosition.y, currentPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {      

    }
}
