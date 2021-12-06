using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusRoar : MonoBehaviour
{
    public List<Transform> MeatlingSpawns;
    private List<Vector3> MeatlingSpawnV;
    public GameObject Meatling;

    private void Start()
    {
        MeatlingSpawnV = new List<Vector3>();
        foreach (Transform transform in MeatlingSpawns)
            MeatlingSpawnV.Add(transform.position);
    }

    public void SpawnMeatlings()
    {
        foreach (Vector3 vector3 in MeatlingSpawnV)
        {
            Debug.Log(vector3);
            Instantiate(Meatling, vector3, new Quaternion());
        }
    }

    public void MakeRoarSound()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexRoar);
    }
}
