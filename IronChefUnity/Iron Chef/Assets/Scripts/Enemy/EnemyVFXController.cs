using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVFXController : MonoBehaviour
{
    [SerializeField]
    public List<VisualEffect> effects;
    [SerializeField]
    public List<GameObject> DynamicSpawnEffects;
    [SerializeField]
    public List<OneTimeShowEffect> OneTimeEffects;

    private void Start()
    {
        foreach(var ote in OneTimeEffects)
        {
            ote.vfx = this;
        }
    }


    public void SpawnDynamicEffect(int i, Vector3 pos)
    {
        if (i >= 0 && i < DynamicSpawnEffects.Count)
        {
            var go = Instantiate(DynamicSpawnEffects[i], pos, Quaternion.identity);
            Destroy(go, 10f);
        }
    }


    public void StartEffect(int i)
    {
        if(i >= 0 && i < effects.Count)
        {
            effects[i].StartEffect();
        }
    }

    public void EndEffect(int i)
    {
        if (i >= 0 && i < effects.Count)
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

    [System.Serializable]
    public class OneTimeShowEffect
    {
        public GameObject[] thingsToShow;
        public float ejectionForce = 0;
        public float liveTime;
        [HideInInspector]public EnemyVFXController vfx;
        public void StartEffect()
        {
            foreach(var go in thingsToShow)
            {
                if(go != null)
                {

                    go.SetActive(true);
                    if(ejectionForce > 0)
                    {
                        go.GetComponent<Rigidbody>()?.AddForce((go.transform.position - vfx.transform.position).normalized * ejectionForce);
                    }
                    Destroy(go, liveTime);
                }
            }
        }
    }
}



