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
        [SerializeField]
        public List<SpecialTrailEffect> specialTrails;
        [SerializeField]
        public List<OutlineEffect> outlines;


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
            foreach (var t in specialTrails)
            {
                t.StartEffect();
            }
            foreach(var t in outlines)
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
            foreach (var t in specialTrails)
            {
                t.EndEffect();
            }
            foreach (var t in outlines)
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
    public class SpecialTrailEffect
    {
        public MeleeWeaponTrail trail; 
        public void StartEffect()
        {
            trail.Emit = true;
        }
        public void EndEffect()
        {
            trail.Emit = false;
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
                    go.transform.SetParent(null);
                    if(ejectionForce > 0)
                    {
                        var center = go.transform.position;
                        var mr = go.GetComponent<MeshRenderer>();
                        if (mr != null)
                            center = mr.bounds.center;

                        go.GetComponent<Rigidbody>()?.AddForce((center - vfx.transform.position).normalized * ejectionForce);
                    }
                    Destroy(go, liveTime);
                }
            }
        }
    }

    [System.Serializable]
    public class OutlineEffect
    {
        public Outline[] outlines;
        public void StartEffect()
        {
            foreach (var o in outlines)
                o.enabled = true;
        }
        public void EndEffect()
        {
            foreach (var o in outlines)
                o.enabled = false;
        }
    }
}



