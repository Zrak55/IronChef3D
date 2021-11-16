using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusStomp : MonoBehaviour
{
    public List<Transform> rockPoints;
    public float fallDelay;
    public GameObject rockPrefab;
    public int rocksPerStomp;
    List<Transform> chosenPoints;
    List<GameObject> currentRocks;

    private void Start()
    {
        chosenPoints = new List<Transform>();
        currentRocks = new List<GameObject>();
        ResetStomp();
    }

    public void ResetStomp()
    {
        chosenPoints.Clear();
    }

    public void DoStomp()
    {
        for(int i = 0; i < rocksPerStomp; i++)
        {
            int rand = 0;
            bool goodPoint = false;
            int attempts = 0;
            while(!goodPoint)
            {
                rand = Random.Range(0, rockPoints.Count);
                if (!chosenPoints.Contains(rockPoints[i]))
                {
                    goodPoint = true;
                }
                else if(attempts >= 20)
                {
                    goodPoint = true;
                }
                attempts++;
            }
            chosenPoints.Add(rockPoints[rand]);
            StartCoroutine(SpawnStomp(fallDelay, rockPoints[rand]));
        }
        
    }

    IEnumerator SpawnStomp(float t, Transform target)
    {
        yield return new WaitForSeconds(t);
        currentRocks.Add(Instantiate(rockPrefab, target.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0))));

    }


    public void DestroyAllRocks()
    {
        foreach (var go in currentRocks)
            Destroy(go);
        currentRocks.Clear();
        ResetStomp();
    }
}
