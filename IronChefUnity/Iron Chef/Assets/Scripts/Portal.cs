using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform outputLocation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CharacterMover>() != null)
        {
            var cc = other.GetComponent<CharacterController>();
            cc.enabled = false;
            other.transform.position = outputLocation.position;
            cc.enabled = true;
        }
    }
}
