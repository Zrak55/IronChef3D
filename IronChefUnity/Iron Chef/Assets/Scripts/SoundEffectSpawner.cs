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
    public AudioClip[] FootstepEffects;
    public AudioClip FridgeSlow;
    public AudioClip MalapenoExplosion;
    public AudioClip[] GruntEffects;
    public AudioClip[] EggCrackEffects;
    public AudioClip EggRollHit;
    public AudioClip EggRollStart;
    public AudioClip[] SlimeEffects;




    private void MakeSoundEffect(Vector3 location, float volume, AudioClip Clip, float pitch)
    {
        var go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        var ac = go.GetComponent<AudioSource>();


        ac.clip = Clip;
        ac.volume = volume;
        ac.pitch = pitch;
        ac.Play();


        Destroy(go, ac.clip.length * 1.1f);

    }


    public void MakeSoundEffect(Vector3 location, float volume, SoundEffect effect)
    {
        AudioClip clipToPlay = null;

        float pitch = Random.Range(0.9333f, 1.0667f);

        int index;
        switch (effect)
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
            case SoundEffect.MalapenoExplosion:
                clipToPlay = MalapenoExplosion;
                break;
            case SoundEffect.FridgeSlow:
                clipToPlay = FridgeSlow;
                break;
            case SoundEffect.Footstep:
                index = Random.Range(0, FootstepEffects.Length);
                clipToPlay = FootstepEffects[index];
                break;
            case SoundEffect.Grunt:
                index = Random.Range(0, GruntEffects.Length);
                clipToPlay = GruntEffects[index];
                break;
            case SoundEffect.Slime:
                index = Random.Range(0, SlimeEffects.Length);
                clipToPlay = SlimeEffects[index];
                break;
            case SoundEffect.EggCrack:
                index = Random.Range(0, EggCrackEffects.Length);
                clipToPlay = EggCrackEffects[index];
                break;
            case SoundEffect.EggRollHit:
                clipToPlay = EggRollHit;
                break;
            case SoundEffect.EggRollStart:
                clipToPlay = EggRollStart;
                break;

        }
                if (clipToPlay != null)
        {
            MakeSoundEffect(location, volume, clipToPlay, pitch);
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
        FilletKnife,
        MalapenoExplosion,
        FridgeSlow,
        Footstep,
        Grunt,
        Slime,
        EggRollStart,
        EggCrack,
        EggRollHit
    }
}

