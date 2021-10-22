using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamControl : MonoBehaviour
{
    public Camera cam;
    public Cinemachine.CinemachineFreeLook cinemachine;

    public SkinnedMeshRenderer playerView;
    public float clipPlayerDistance;
    float currentShakeIntensity = 0;
    float currentShakeFrequency = 0;
    public float frequencyAcceleration;
    public float intensityAcceleration;

    bool shouldChange;


    private void Awake()
    {
        playerView = FindObjectOfType<CharacterMover>().model.GetComponentInChildren<SkinnedMeshRenderer>();

        ShakeCam(0, 0);   
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


        TickCamShake();
        



    }

    public void CanMoveCam(bool CanMove)
    {
        if (CanMove)
        {
            cinemachine.m_YAxis.m_MaxSpeed = 0.35f * Settings.Sensitivity;
            cinemachine.m_XAxis.m_MaxSpeed = 30 * Settings.Sensitivity;
        }
        else
        {
            cinemachine.m_YAxis.m_MaxSpeed = 0;
            cinemachine.m_XAxis.m_MaxSpeed = 0;
        }

        cinemachine.m_YAxis.m_InvertInput = Settings.InvertVerticalCam;
    }

    void TickCamShake()
    {
        if (shouldChange)
        {

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

            shouldChange = false;
        }

        if (currentShakeIntensity != 0)
        {
            currentShakeIntensity = Mathf.Max(currentShakeIntensity + (Time.deltaTime * -1f * intensityAcceleration), 0);
            shouldChange = true;

        }
        if (currentShakeFrequency != 0)
        {
            currentShakeFrequency = Mathf.Max(currentShakeFrequency + (Time.deltaTime * -1f * frequencyAcceleration), 0);
            shouldChange = true;
        }

    }

    public void ShakeCam(float intensity, float frequency)
    {
        currentShakeIntensity = intensity;
        currentShakeFrequency = frequency;
        shouldChange = true;
    }

}
