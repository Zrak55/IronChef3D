using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXController : MonoBehaviour
{
    [SerializeField]
    public List<VisualEffect> effects;

    public void StartEffect(int i)
    {
        if(i >= 0 && i <= effects.Count)
        {
            effects[i].StartEffect();
        }
    }

    public void EndEffect(int i)
    {
        if (i >= 0 && i <= effects.Count)
        {
            effects[i].EndEffect();
        }
    }




    [System.Serializable]
    public class VisualEffect
    {
        [SerializeField]
        public List<ParticleEffect> particles;
        [SerializeField]
        public List<TrailEffect> trails;


        public void StartEffect()
        {
            foreach(var p in particles)
            {
                p.StartEffect();
            }
            foreach(var t in trails)
            {
                t.StartEffect();
            }
        }
        public void EndEffect()
        {
            foreach(var p in particles)
            {
                p.EndEffect();
                
            }
            foreach(var t in trails)
            {
                t.EndEffect();
            }
        }

    }

    [System.Serializable]
    public class ParticleEffect
    {
        public ParticleSystem particle;
        public void StartEffect()
        {
            particle.Play();
        }
        public void EndEffect()
        {
            particle.Stop();
        }
    }

    [System.Serializable]
    public class TrailEffect
    {
        public TrailRenderer trail;
        public void StartEffect()
        {
            trail.emitting = true ;
        }
        public void EndEffect()
        {
            trail.emitting = false;
        }
    }
}



