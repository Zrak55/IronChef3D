using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafColorChanger : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material mainMat;
    public Material[] alternateMats;

    [Range(0, 1)]
    public float percentChangeChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeColor()
    {
        if(Random.Range(0f, 1f) <= percentChangeChance)
        {
            mesh.material = alternateMats[Random.Range(0, alternateMats.Length)];
        }
        else
        {
            mesh.material = mainMat;
        }
    }
}
