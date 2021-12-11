using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundEffectSpawner : MonoBehaviour
{
    public GameObject audioSource;
    public GameObject bossAudioSource;

    public static SoundEffectSpawner soundEffectSpawner;

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
    public AudioClip AbilityRecharge;
    public AudioClip[] PlayerArmorHitEffects;
    public AudioClip[] monsterDeathEffects;
    public AudioClip freezerEffect;
    public AudioClip[] xpEffects;
    public AudioClip SpearHit;
    public AudioClip SpearThrow;
    public AudioClip[] FiftyPunchEffects;
    public AudioClip PortableLunch;
    public AudioClip Glockamole;
    public AudioClip BreadTrap;
    public AudioClip Hammer;
    public AudioClip CatapastaFly;
    public AudioClip[] CatapastaHitEffects;
    public AudioClip SugarRush;
    public AudioClip[] TrexStep;
    public AudioClip[] TrexBite;    
    public AudioClip TrexWallHit;
    public AudioClip TrexStomp;
    public AudioClip TrexFireBreathWindUp;
    public AudioClip TrexFireBreath;
    public AudioClip TrexRoar;
    public AudioClip TrexTailSwipe;
    public AudioClip TrexTailSwipeHit;
    public AudioClip IsopodMove;
    public AudioClip IsopodAttack;
    public AudioClip[] OgreIdle;
    public AudioClip OgreAttack;
    public AudioClip OgreStep;
    public AudioClip MenuSwitch;
    public AudioClip MenuSelect;
    public AudioClip[] HydraIdle;
    public AudioClip TrollStep;
    public AudioClip HydraSpit;
    public AudioClip[] HydraSpitLand;
    public AudioClip HydraSlam;
    public AudioClip HydraSweep;

    private void Awake()
    {
        soundEffectSpawner = this;
    }
    public AudioSource MakeFollowingSoundEffect(Transform follow, SoundEffect effect)
    {
        return MakeFollowingSoundEffect(follow, effect, 1, -1);
    }
    public AudioSource MakeFollowingSoundEffect(Transform follow, SoundEffect effect, float volume, float overrideTimeAlive)
    {
        var x = MakeSoundEffect(follow.position, volume, effect, overrideTimeAlive);
        x.transform.SetParent(follow);
        return x;
    }
    private AudioSource MakeSoundEffect(Vector3 location, float volume, AudioClip Clip, float pitch, bool isBossEffect,float overrideTimeAlive = -1)
    {
        GameObject go;
        if (!isBossEffect)
            go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        else
            go = Instantiate(bossAudioSource, location, Quaternion.Euler(Vector3.zero));
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

        bool isBossEffect = false;

        int index;
        switch (effect)
        {

            /*
             * BLANK CASE STATEMENT:             
             case SoundEffect.:
                index = Random.Range(0, .Length);
                clipToPlay = [index];
                break;

             case SoundEffect.:
                clipToPlay = ;
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
                isBossEffect = true;
                break;
            case SoundEffect.EggRollHit:
                clipToPlay = EggRollHit;
                isBossEffect = true;
                break;
            case SoundEffect.EggRollStart:
                clipToPlay = EggRollStart;
                break;
            case SoundEffect.EggRoll:
                index = Random.Range(0, EggRoll.Length);
                clipToPlay = EggRoll[index];
                isBossEffect = true;
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
            case SoundEffect.AbilityRecharge:
                clipToPlay = AbilityRecharge;
                break;
            case SoundEffect.PlayerArmorHit:
                index = Random.Range(0, PlayerArmorHitEffects.Length);
                clipToPlay = PlayerArmorHitEffects[index];
                break;
            case SoundEffect.XpPickup:
                index = Random.Range(0, xpEffects.Length);
                clipToPlay = xpEffects[index];
                break;
            case SoundEffect.MonsterDeath:
                index = Random.Range(0, monsterDeathEffects.Length);
                clipToPlay = monsterDeathEffects[index];
                break;
            case SoundEffect.Freezer:
                clipToPlay = freezerEffect;
                break;
            case SoundEffect.SpearHit:
                clipToPlay = SpearHit;
                break;
            case SoundEffect.SpearThrow:
                clipToPlay = SpearThrow;
                break;
            case SoundEffect.BreadTrap:
                clipToPlay = BreadTrap;
                break;
            case SoundEffect.Glockamole:
                clipToPlay = Glockamole;
                break;
            case SoundEffect.Hammer:
                clipToPlay = Hammer;
                break;
            case SoundEffect.FiftyPunches:
                index = Random.Range(0, FiftyPunchEffects.Length -1);
                clipToPlay = FiftyPunchEffects[index];
                break;
            case SoundEffect.PunchMiss:
                clipToPlay = FiftyPunchEffects[FiftyPunchEffects.Length];
                break;
            case SoundEffect.PortableLunch:
                clipToPlay = PortableLunch;
                break;
            case SoundEffect.CatapastaFly:
                clipToPlay = CatapastaFly;
                break;
            case SoundEffect.CatapastaHit:
                index = Random.Range(0, CatapastaHitEffects.Length);
                clipToPlay = CatapastaHitEffects[index];
                break;
            case SoundEffect.SugarRush:
                clipToPlay = SugarRush;
                break;
            case SoundEffect.TrexStepEffects:
                index = Random.Range(0, TrexStep.Length);
                clipToPlay = TrexStep[index];
                break;
            case SoundEffect.TrexWallHit:
                clipToPlay = TrexWallHit;
                isBossEffect = true;
                break;
            case SoundEffect.TrexStomp:
                clipToPlay = TrexStomp;
                isBossEffect = true;
                break;
            case SoundEffect.TrexFireBreathWindUp:
                clipToPlay = TrexFireBreathWindUp;
                isBossEffect = true;
                break;
            case SoundEffect.TrexFireBreath:
                clipToPlay = TrexFireBreath;
                isBossEffect = true;
                break;
            case SoundEffect.TrexRoar:
                clipToPlay = TrexRoar;
                isBossEffect = true;
                break;
            case SoundEffect.TrexBiteEffects:
                index = Random.Range(0, TrexBite.Length);
                clipToPlay = TrexBite[index];
                isBossEffect = true;
                break;
            case SoundEffect.TrexTailSwipe:
                clipToPlay = TrexTailSwipe;
                break;
            case SoundEffect.TrexTailSwipeHit:
                clipToPlay = TrexTailSwipeHit;
                break;
            case SoundEffect.IsopodMove:
                clipToPlay = IsopodMove;
                break;
            case SoundEffect.IsopodAttack:
                clipToPlay = IsopodAttack;
                break;
            case SoundEffect.OgreStep:
                clipToPlay = OgreStep;
                break;
            case SoundEffect.OgreIdle:
                index = Random.Range(0, OgreIdle.Length);
                clipToPlay = OgreIdle[index];
                break;
            case SoundEffect.OgreAttack:
                clipToPlay = OgreAttack;
                break;
            case SoundEffect.MenuSelect:
                clipToPlay = MenuSelect;
                break;
            case SoundEffect.MenuSwitch:
                clipToPlay = MenuSwitch;
                break;
            case SoundEffect.TrollStep:
                clipToPlay = TrollStep;
                break;
            case SoundEffect.HydraIdle:
                index = Random.Range(0, HydraIdle.Length);
                clipToPlay = HydraIdle[index];
                break;
            case SoundEffect.HydraSlam:
                clipToPlay = HydraSlam;
                break;
            case SoundEffect.HydraSpit:
                clipToPlay = HydraSpit;
                break;
            case SoundEffect.HydraSpitLand:
                index = Random.Range(0, HydraSpitLand.Length);
                clipToPlay = HydraSpitLand[index];
                break;
            case SoundEffect.HydraSweep:
                clipToPlay = HydraSweep;
                break;

        }
        if (clipToPlay != null)
        {
            return MakeSoundEffect(location, volume, clipToPlay, pitch, isBossEffect, overrideTimeAlive);
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
        BaconIdle,
        AbilityRecharge,
        PlayerArmorHit,
        Freezer,
        XpPickup,
        MonsterDeath,
        SpearHit,
        SpearThrow,
        Hammer,
        Glockamole,
        BreadTrap,
        FiftyPunches,
        PunchMiss,
        PortableLunch,
        CatapastaFly,
        CatapastaHit,
        SugarRush,
        TrexStepEffects,
        TrexBiteEffects,
        TrexWallHit,
        TrexStomp,
        TrexFireBreathWindUp,
        TrexFireBreath,
        TrexRoar,
        TrexTailSwipe,
        TrexTailSwipeHit,
        IsopodMove,
        IsopodAttack,
        OgreStep,
        OgreIdle,
        OgreAttack,
        MenuSwitch,
        MenuSelect,
        HydraIdle,
        TrollStep,
        HydraSpit,
        HydraSpitLand,
        HydraSlam,
        HydraSweep
    }
}

