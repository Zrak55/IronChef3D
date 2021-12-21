using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreamyComets : MonoBehaviour
{
    public GameObject CometPrefab;
    public GameObject Warning;
    public int numComets = 12;
    public float interCometDelay = 0.5f;
    public Transform stageCener;
    public float stageRadius;
    
    public void DoComets()
    {
        
        StartCoroutine(SpawnComets());
        

    }

    IEnumerator SpawnComets()
    {
        for(int i = 0; i < numComets; i++)
        {
            Vector3 target = stageCener.position + (Random.Range(0, stageRadius) * new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized);
            var comet = Instantiate(CometPrefab, target + new Vector3(Random.Range(-100f, 100f), 150, Random.Range(-100f, 100f)), Quaternion.identity);
            comet.transform.LookAt(target);

            Destroy(Instantiate(Warning, target, Quaternion.identity), 2f);

            yield return new WaitForSeconds(interCometDelay);
        }

    }


}
