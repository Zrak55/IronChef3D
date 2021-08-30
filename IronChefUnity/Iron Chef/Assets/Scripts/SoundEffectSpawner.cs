using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectSpawner : MonoBehaviour
{
    public static GameObject audioSource;

    public AudioClip[] CleaverEffects;
    public AudioClip[] RollingPinEffects;
    public AudioClip[] FilletKnifeEffects;

    

    private static void MakeSoundEffect(Vector3 location, float volume, AudioClip Clip)
    {
        var go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        var ac = go.GetComponent<AudioSource>();

        ac.volume = volume;
        ac.clip = Clip;
        ac.Play();
        Destroy(go, ac.clip.length * 1.1f);
        
    }


    public static void MakeSoundEffect(Vector3 location, float volume, SoundEffect effect)
    {
        AudioClip clipToPlay = null;

        switch(effect)
        {
            case SoundEffect.Cleaver:
                break;
            case SoundEffect.RollingPin:
                break;
            case SoundEffect.FilletKnife:
                break;
        }

        if(clipToPlay != null)
        {
            MakeSoundEffect(location, volume, clipToPlay);
        }
    }

    public static void MakeSoundEffect(Vector3 location, SoundEffect effect)
    {
        MakeSoundEffect(location, 1, effect);
    }



    public enum SoundEffect
    {
        Cleaver,
        RollingPin,
        FilletKnife
    }
}
   
