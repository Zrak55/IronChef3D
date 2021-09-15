using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviour
{
    public Camera cam;
    public Cinemachine.CinemachineFreeLook cinemachine;


    private void Awake()
    {
        cinemachine.Follow = FindObjectOfType<CharacterMover>().CamFollowPoint;
        cinemachine.LookAt = FindObjectOfType<CharacterMover>().CamLookPoint;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cinemachine.m_YAxis.m_MaxSpeed = 0.25f * Settings.Sensitivity;
        cinemachine.m_XAxis.m_MaxSpeed = 40 * Settings.Sensitivity;

        cinemachine.m_YAxis.m_InvertInput = Settings.InvertVerticalCam;
    }
}
