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
    public AudioClip[] EggRoll;
    public AudioClip[] TomatrollEffects;
    public AudioClip FryingPanFly;
    public AudioClip FryingPanHit;
    public AudioClip[] FondemonEffects;
    public AudioClip SpatulaLaunch;
    public AudioClip SpatulaAir;
    public AudioClip CrabWalk;
    public AudioClip CrabSnapOpen;
    public AudioClip CrabSnapClose;
    public AudioClip BaconBite;
    public AudioClip BaconIdle;

    public AudioSource MakeFollowingSoundEffect(Transform follow, SoundEffect effect)
    {
        return MakeFollowingSoundEffect(follow, effect, 1, -1);
    }
    public AudioSource MakeFollowingSoundEffect(Transform follow, SoundEffect effect, float volume, float overrideTimeAlive)
    {
        var x = MakeSoundEffect(transform.position, volume, effect, overrideTimeAlive);
        x.transform.SetParent(follow);
        return x;
    }
    private AudioSource MakeSoundEffect(Vector3 location, float volume, AudioClip Clip, float pitch, float overrideTimeAlive = -1)
    {
        var go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        var ac = go.GetComponent<AudioSource>();


        ac.clip = Clip;
        ac.volume = volume;
        ac.pitch = pitch;
        ac.Play();

        if (overrideTimeAlive > 0)
            Destroy(go, overrideTimeAlive);
        else
            Destroy(go, ac.clip.length * 1.1f);

        return ac;

    }


    public AudioSource MakeSoundEffect(Vector3 location, float volume, SoundEffect effect, float overrideTimeAlive = -1)
    {
        AudioClip clipToPlay = null;

        float pitch = Random.Range(0.9333f, 1.0667f);

        int index;
        switch (effect)
        {

            /*
             * BLANK CASE STATEMENT:             
             case SoundEffect.:
                index = Random.Range(0, .Length);
                clipToPlay = [index];
                break;
             */


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
            case SoundEffect.EggRoll:
                index = Random.Range(0, EggRoll.Length);
                clipToPlay = EggRoll[index];
                break;
            case SoundEffect.Tomatroll:
                index = Random.Range(0, TomatrollEffects.Length);
                clipToPlay = TomatrollEffects[index];
                break;
            case SoundEffect.Fondemon:
                index = Random.Range(0, FondemonEffects.Length);
                clipToPlay = FondemonEffects[index];
                break;
            case SoundEffect.FryingPanHit:
                clipToPlay = FryingPanHit;
                break;
            case SoundEffect.FryingPanFly:
                clipToPlay = FryingPanFly;
                break;
            case SoundEffect.SpatulaLaunch:
                clipToPlay = SpatulaLaunch;
                break;
            case SoundEffect.SpatulaAir:
                clipToPlay = SpatulaAir;
                break;
            case SoundEffect.CrabSnapClose:
                clipToPlay = CrabSnapClose;
                break;
            case SoundEffect.CrabSnapOpen:
                clipToPlay = CrabSnapOpen;
                break;
            case SoundEffect.CrabWalk:
                clipToPlay = CrabWalk;
                break;
            case SoundEffect.BaconBite:
                clipToPlay = BaconBite;
                break;
            case SoundEffect.BaconIdle:
                clipToPlay = BaconIdle;
                break;
        }
        if (clipToPlay != null)
        {
            return MakeSoundEffect(location, volume, clipToPlay, pitch, overrideTimeAlive);
        }
        else
            return null;
    }

    public AudioSource MakeSoundEffect(Vector3 location, SoundEffect effect)
    {
        return MakeSoundEffect(location, 1, effect);
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
        EggRollHit,
        EggRoll,
        Tomatroll,
        FryingPanHit,
        FryingPanFly,
        Fondemon,
        SpatulaLaunch,
        SpatulaAir,
        CrabWalk,
        CrabSnapOpen,
        CrabSnapClose,
        BaconBite,
        BaconIdle
    }
}

