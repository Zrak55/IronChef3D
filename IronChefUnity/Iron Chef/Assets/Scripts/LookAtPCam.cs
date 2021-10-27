using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPCam : MonoBehaviour
{

    PlayerCameraSetup pcam;

    void Start()
    {
        pcam = FindObjectOfType<PlayerCameraSetup>();


    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pcam.cam.transform);
    }

}