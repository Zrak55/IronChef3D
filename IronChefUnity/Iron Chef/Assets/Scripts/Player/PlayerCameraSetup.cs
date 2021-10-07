using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraSetup : MonoBehaviour
{
    public Camera cam;
    public Cinemachine.CinemachineFreeLook cinemachine;
    public bool CanMoveCam;

    public SkinnedMeshRenderer playerView;
    public float clipPlayerDistance;


    private void Awake()
    {
        cinemachine.Follow = FindObjectOfType<CharacterMover>().CamFollowPoint;
        cinemachine.LookAt = FindObjectOfType<CharacterMover>().CamLookPoint;
        playerView = FindObjectOfType<CharacterMover>().model.GetComponentInChildren<SkinnedMeshRenderer>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMoveCam)
        {
            cinemachine.m_YAxis.m_MaxSpeed = 0.25f * Settings.Sensitivity;
            cinemachine.m_XAxis.m_MaxSpeed = 40 * Settings.Sensitivity;
        }
        else
        {
            cinemachine.m_YAxis.m_MaxSpeed = 0;
            cinemachine.m_XAxis.m_MaxSpeed = 0;
        }

        if (Vector3.Distance(cam.transform.position, playerView.transform.position) < clipPlayerDistance)
        {

            playerView.enabled = false;
        }
        else //if(playerRig.activeSelf == false && Vector3.Distance(transform.position, playerRig.transform.position) >= clipPlayerDistance)
        {
            playerView.enabled = true;
        }

        cinemachine.m_YAxis.m_InvertInput = Settings.InvertVerticalCam;
    }
}
