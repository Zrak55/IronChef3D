using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaLauncher : MonoBehaviour
{
    public Transform target;
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerSpatulaJumper>() != null)
        {
            other.GetComponent<PlayerSpatulaJumper>().Jump(target.position, time);
            FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.SpatulaLaunch);
            FindObjectOfType<SoundEffectSpawner>().MakeFollowingSoundEffect(other.transform, SoundEffectSpawner.SoundEffect.SpatulaAir, 1, time);
        }
    }
}
