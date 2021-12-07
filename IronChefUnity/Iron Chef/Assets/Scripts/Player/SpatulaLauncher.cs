using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaLauncher : MonoBehaviour
{
    public Transform target;
    public float time;
    bool launchDelay = false;
    public Animator anim;
    [Header("Leave as -1 for default height")]
    public float height = -1;
    public bool holdInPlace = false;

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
        if(!launchDelay && other.GetComponent<PlayerSpatulaJumper>() != null)
        {
            other.GetComponent<PlayerSpatulaJumper>().Jump(target.position, time, holdInPlace, height);
            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.SpatulaLaunch);
            SoundEffectSpawner.soundEffectSpawner.MakeFollowingSoundEffect(other.transform, SoundEffectSpawner.SoundEffect.SpatulaAir, 1, time);
            anim.SetTrigger("Launch");
            launchDelay = true;
            Invoke("UnDelay", 1f);
        }
    }

    void UnDelay()
    {
        launchDelay = false;
    }
}
