using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusLightBreathEffect : MonoBehaviour
{
    public Light[] TailLights;
    public ParticleSystem RibcageParticles;



    public void PlayEffect()
    {
        RibcageParticles.Play();
    }

    public void StopEffect()
    {

    }
}
