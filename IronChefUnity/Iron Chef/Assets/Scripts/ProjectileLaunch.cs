using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileLaunch : MonoBehaviour
{
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.AddForce(Physics.gravity);
    }


    public void Launch(float power, Vector3 forward, float angle)
    {
        Vector3 direction = IronChefUtils.RotateUpByDegree(forward, angle);
        GetComponent<Rigidbody>().AddForce(direction.normalized * power, ForceMode.Impulse);
    }

}
