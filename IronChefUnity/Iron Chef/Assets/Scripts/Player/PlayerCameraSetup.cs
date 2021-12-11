using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviour
{
    public Camera cam;
    public Cinemachine.CinemachineFreeLook cinemachine;
    public static PlayerCameraSetup pcamSetup;


    private void Awake()
    {
        cinemachine.Follow = FindObjectOfType<CharacterMover>().CamFollowPoint;
        cinemachine.LookAt = FindObjectOfType<CharacterMover>().CamLookPoint;
        pcamSetup = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
