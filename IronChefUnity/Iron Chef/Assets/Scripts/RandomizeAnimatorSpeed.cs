using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimatorSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = Random.Range(0.25f, 0.75f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
