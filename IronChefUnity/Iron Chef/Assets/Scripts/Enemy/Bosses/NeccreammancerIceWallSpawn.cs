using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeccreammancerIceWallSpawn : MonoBehaviour
{
    public GameObject IceWallPrefab;
    public Transform[] SpawnPoints;

    public void SpawnWall()
    {
        int rand = Random.Range(0, SpawnPoints.Length);
        Instantiate(IceWallPrefab, SpawnPoints[rand].position, SpawnPoints[rand].rotation);
    }
}
