using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public SoundEffectSpawner.SoundEffect soundName;
    public float CamShakeAmount = 0;
    PlayerCamControl cam;


    public void MakeFootstepEffect()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, soundName);
        if(CamShakeAmount > 0)
        {
            if (cam == null)
                cam = FindObjectOfType<PlayerCamControl>();

            if (Vector3.Distance(cam.cinemachine.m_Follow.position, transform.position) < 200f) //dont shake from 1000000 miles away
                cam.ShakeCam(CamShakeAmount, 0.25f);
        }
    }
}
