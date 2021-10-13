using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class SpawnTreesOnMaterial : MonoBehaviour
{
    public Material targetMaterial;

    public GameObject[] TreePrefabs;

    public List<GameObject> spawnedTrees;

    public int lowXBound;
    public int highXBound;
    public int lowZBound;
    public int highZBound;
    public int startRayHeight;
    public int incrementDistance;
    public float jitterDistance;

    public bool clearCurrentTreesOnNewSpawn = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearTrees()
    {
        if (spawnedTrees != null)
        {

            foreach (var x in spawnedTrees)
            {
                if (x != null)
                    DestroyImmediate(x);
            }
            spawnedTrees.Clear();
        }
    }
    public void SpawnTrees()
    {
        if (spawnedTrees == null)
            spawnedTrees = new List<GameObject>();

        if(clearCurrentTreesOnNewSpawn)
        {
            ClearTrees();
        }



        RaycastHit hit;
        for(int i = lowXBound; i <= highXBound; i += incrementDistance)
        {
            for(int j = lowZBound; j < highZBound; j += incrementDistance)
            {
                float iJitter = Random.Range(-jitterDistance, jitterDistance);
                float jJitter = Random.Range(-jitterDistance, jitterDistance);

                if (Physics.Raycast(new Ray(new Vector3(i + iJitter, startRayHeight, j + jJitter), -transform.up), out hit))
                { 
                    if(hit.collider.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        if (hit.collider.gameObject.GetComponent<MeshRenderer>().sharedMaterial == targetMaterial)
                        {
                            spawnedTrees.Add(PrefabUtility.InstantiatePrefab(TreePrefabs[Random.Range(0, TreePrefabs.Length)]) as GameObject);
                            spawnedTrees[spawnedTrees.Count - 1].transform.position = hit.point;
                            spawnedTrees[spawnedTrees.Count - 1].transform.SetParent(transform);
                        }
                    }
                }
            }
        }
    }
}
