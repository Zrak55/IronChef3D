using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamControl : MonoBehaviour
{
    public Camera cam;
    public Cinemachine.CinemachineFreeLook cinemachine;

    CinemachineInputProvider input;

    public SkinnedMeshRenderer playerView;
    [SerializeField]
    public ObscureObject[] obscureTheseObjects;
    public float clipPlayerDistance;
    float currentShakeIntensity = 0;
    public float currentShakeFrequency = 0;

    float timer = 0f;

    float verticalMult = 0.35f;


    bool canMove;

    Vector3 currentSnapRotation;

    public bool aiming;


    float baseXSpeed;
    float baseXAccel;
    float baseXDeccel;
    InputActionReference xyInput;

    [SerializeField] List<GameObject> normalCameraObjs;
    [SerializeField] List<GameObject> aimingCameraObjs;


    private void Awake()
    {

        aiming = false;

        playerView = FindObjectOfType<CharacterMover>().model.GetComponentInChildren<SkinnedMeshRenderer>();

        ShakeCam(0, 0, true);

        input = GetComponentInChildren<CinemachineInputProvider>();

        baseXSpeed = cinemachine.m_XAxis.m_MaxSpeed;
        baseXAccel = cinemachine.m_XAxis.m_AccelTime;
        baseXDeccel = cinemachine.m_XAxis.m_DecelTime;
        xyInput = input.XYAxis;
        
    }

    public bool IsAiming()
    {
        return aiming;
    }

    public void EnterAimingMode()
    {
        aiming = true;

        foreach(var g in normalCameraObjs)
        {
            g.SetActive(false);
        }
        foreach(var g in aimingCameraObjs)
        {
            g.SetActive(true);
        }
    }
    public void LeaveAimingMode()
    {
        aiming = false;

        foreach (var g in aimingCameraObjs)
        {
            g.SetActive(false);
        }
        foreach (var g in normalCameraObjs)
        {
            g.SetActive(true);
        }
        
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



        if (Vector3.Distance(cam.transform.position, playerView.transform.position) < clipPlayerDistance)
        {

            playerView.enabled = false;
        }
        else //if(playerRig.activeSelf == false && Vector3.Distance(transform.position, playerRig.transform.position) >= clipPlayerDistance)
        {
            playerView.enabled = true;
        }

        foreach(var g in obscureTheseObjects)
        {
            if (Vector3.Distance(cam.transform.position, g.mesh.transform.position) < g.obscureDistance + g.objectRadius)
            {

                g.mesh.enabled = false;
            }
            else //if(playerRig.activeSelf == false && Vector3.Distance(transform.position, playerRig.transform.position) >= clipPlayerDistance)
            {
                g.mesh.enabled = true;
            }
        }



        TickCamShake();



        float oldVert = verticalMult;
        if (InputControls.controls.Gameplay.RotateCameraControllerCheck.ReadValue<Vector2>() != Vector2.zero)
        {

            verticalMult = 0.5f;
            
        }
        else
        {
            verticalMult = 0.5f;
        }

        if (oldVert != verticalMult)
            SetCamSensitivity();



        if(InputControls.controls.Gameplay.SnapCameraToBack.triggered)
        {
            SnapCamToBack();
        }

        if(aiming)
        {
            cinemachine.m_XAxis.Value = Camera.main.transform.rotation.eulerAngles.y;
        }
    }

    void SetCamSensitivity()
    {
        if (canMove)
        {

            cinemachine.m_YAxis.m_MaxSpeed = verticalMult * Settings.Sensitivity;
            cinemachine.m_XAxis.m_MaxSpeed = 30 * Settings.Sensitivity;
        }
        else
        {
            cinemachine.m_YAxis.m_MaxSpeed = 0;
            cinemachine.m_XAxis.m_MaxSpeed = 0;
        }

        cinemachine.m_YAxis.m_InvertInput = Settings.InvertVerticalCam;
    }

    public void CanMoveCam(bool CanMove)
    {
        canMove = CanMove;
        SetCamSensitivity();
    }

    void TickCamShake()
    {
        if (timer != 0)
        {
            timer = Mathf.Max(timer - Time.deltaTime, 0);

        }
        else
        {
            currentShakeIntensity = 0;

        }

        CinemachineBasicMultiChannelPerlin c;
            c = cinemachine.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = currentShakeIntensity;
            c.m_FrequencyGain = currentShakeFrequency;
            c = cinemachine.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = currentShakeIntensity;
            c.m_FrequencyGain = currentShakeFrequency;
            c = cinemachine.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            c.m_AmplitudeGain = currentShakeIntensity;
            c.m_FrequencyGain = currentShakeFrequency;

       
        

    }

    public void ShakeCam(float intensity, float shakeTime, bool overrideAmount = false)
    {
        if (overrideAmount || (intensity > currentShakeIntensity))
        {
            timer = shakeTime;
            currentShakeIntensity = intensity;
        }
    }

    [Serializable]
    public class ObscureObject
    {
        public MeshRenderer mesh;
        public float obscureDistance;
        public float objectRadius;
    }


    public void SnapCamToBack()
    {
        cinemachine.m_RecenterToTargetHeading.m_enabled = true;
        Invoke("Uncenter", 1f);
    }

    void Uncenter()
    {
        cinemachine.m_RecenterToTargetHeading.m_enabled = false;
    }

}


