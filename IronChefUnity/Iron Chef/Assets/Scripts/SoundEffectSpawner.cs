using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectSpawner : MonoBehaviour
{
    public GameObject audioSource;

    public AudioMixer mixer;

    public AudioClip[] CleaverEffects;
    public AudioClip[] RollingPinEffects;
    public AudioClip[] FilletKnifeEffects;

    

 




    private void MakeSoundEffect(Vector3 location, float volume, AudioClip Clip)
    {
        var go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        var ac = go.GetComponent<AudioSource>();

        ac.outputAudioMixerGroup = mixer.FindMatchingGroups("FX")[0];
        ac.volume = volume;
        ac.clip = Clip;
        ac.Play();
        Destroy(go, ac.clip.length * 1.1f);
        
    }


    public void MakeSoundEffect(Vector3 location, float volume, SoundEffect effect)
    {
        AudioClip clipToPlay = null;


        int index;
        switch(effect)
        {
            case SoundEffect.Cleaver:
                index = Random.Range(0, CleaverEffects.Length);
                clipToPlay = CleaverEffects[index];
                break;
            case SoundEffect.RollingPin:
                index = Random.Range(0, RollingPinEffects.Length);
                clipToPlay = RollingPinEffects[index];
                break;
            case SoundEffect.FilletKnife:
                index = Random.Range(0, FilletKnifeEffects.Length);
                clipToPlay = FilletKnifeEffects[index];
                break;
        }

        if(clipToPlay != null)
        {
            MakeSoundEffect(location, volume, clipToPlay);
        }
    }

    public void MakeSoundEffect(Vector3 location, SoundEffect effect)
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
   
