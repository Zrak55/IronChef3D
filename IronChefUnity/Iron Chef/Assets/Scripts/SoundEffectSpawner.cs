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
    public AudioClip[] Glockamole;
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
    public AudioClip[] HydraSpawn;
    public AudioClip TrollSlam;
    public AudioClip[] BenedictLaugh;
    public AudioClip Eat;
    public AudioClip EatEmpty;
    public AudioClip EatFull;
    public AudioClip CauldronTravel;
    public AudioClip AreaDiscover;
    public AudioClip IngredFull;
    public AudioClip LevelCom;
    public AudioClip LevelCom100;
    public AudioClip GummyBearAttack;
    public AudioClip GummyBearIdle;
    public AudioClip GummyBearStep;
    public AudioClip GummyBearDeath;
    public AudioClip GummyZombieIdle;
    public AudioClip GummyZombieAttack;
    public AudioClip Parry;
    public AudioClip HammerBuild;
    public AudioClip CarbUp;
    public AudioClip PlayerDeath;
    public AudioClip[] MortaterEffects;
    public AudioClip[] M1BrownieEffects;
    public AudioClip[] Carrot50CaloryEffects;
    public AudioClip CritHit;
    public AudioClip[] OnionKnightEffects;
    public AudioClip[] MeatleEffects;
    public AudioClip[] FudgeEffects;

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
        if(x != null)
        {

            x.transform.SetParent(follow);
        }
        return x;
    }
    private AudioSource MakeSoundEffect(Vector3 location, float volume, AudioClip Clip, float pitch, bool isBossEffect,float overrideTimeAlive = -1)
    {
        GameObject go;
        if (!isBossEffect)
            go = Instantiate(audioSource, location, Quaternion.Euler(Vector3.zero));
        else
            go = Instantiate(bossAudioSource, location, Quaternion.Euler(Vector3.zero));


        AudioSource ac = null;

        if (Clip != null)
        {
            ac = go.GetComponent<AudioSource>();


            ac.clip = Clip;
            ac.volume = volume;
            ac.pitch = pitch;

            ac.Play();

            if (overrideTimeAlive > 0)
                Destroy(go, overrideTimeAlive);
            else
                Destroy(go, ac.clip.length * 1.1f);

        }
        else
        {
            Destroy(go, 1f);
        }
            

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
                clipToPlay = Glockamole[0];
                break;
            case SoundEffect.GlockamoleHit:
                clipToPlay = Glockamole[1];
                break;
            case SoundEffect.Hammer:
                clipToPlay = Hammer;
                break;
            case SoundEffect.FiftyPunches:
                index = Random.Range(0, FiftyPunchEffects.Length -1);
                clipToPlay = FiftyPunchEffects[index];
                break;
            case SoundEffect.PunchMiss:
                clipToPlay = FiftyPunchEffects[FiftyPunchEffects.Length - 1];
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
                isBossEffect = true;
                break;
            case SoundEffect.HydraSlam:
                clipToPlay = HydraSlam;
                isBossEffect = true;
                break;
            case SoundEffect.HydraSpit:
                clipToPlay = HydraSpit;
                isBossEffect = true;
                break;
            case SoundEffect.HydraSpitLand:
                index = Random.Range(0, HydraSpitLand.Length);
                clipToPlay = HydraSpitLand[index];
                isBossEffect = true;
                break;
            case SoundEffect.HydraSweep:
                clipToPlay = HydraSweep;
                isBossEffect = true;
                break;
            case SoundEffect.HydraSpawnEffects:
                index = Random.Range(0, HydraSpawn.Length);
                clipToPlay = HydraSpawn[index];
                isBossEffect = true;
                break;
            case SoundEffect.TrollSlam:
                clipToPlay = TrollSlam;
                break;
            case SoundEffect.BenedictLaugh:
                clipToPlay = BenedictLaugh[Random.Range(0, BenedictLaugh.Length)];
                isBossEffect = true;
                break;
            case SoundEffect.Eat:
                clipToPlay = Eat;
                break;
            case SoundEffect.EatEmpty:
                clipToPlay = EatEmpty;
                break;
            case SoundEffect.EatFull:
                clipToPlay = EatFull;
                break;
            case SoundEffect.CauldronTravel:
                clipToPlay = CauldronTravel;
                break;
            case SoundEffect.AreaDiscover:
                clipToPlay = AreaDiscover;
                break;
            case SoundEffect.IngredFull:
                clipToPlay = IngredFull;
                break;
            case SoundEffect.LevelCom:
                pitch = 1;
                clipToPlay = LevelCom;
                break;
            case SoundEffect.LevelCom100:
                pitch = 1;
                clipToPlay = LevelCom100;
                break;
            case SoundEffect.GummyBearDeath:
                clipToPlay = GummyBearDeath;
                break;
            case SoundEffect.GummyBearAttack:
                clipToPlay = GummyBearAttack;
                break;
            case SoundEffect.GummyBearIdle:
                clipToPlay = GummyBearIdle;
                break;
            case SoundEffect.GummyBearStep:
                clipToPlay = GummyBearStep;
                break;
            case SoundEffect.GummyZombieAttack:
                clipToPlay = GummyZombieAttack;
                break;
            case SoundEffect.GummyZombieIdle:
                clipToPlay = GummyZombieIdle;
                break;
            case SoundEffect.Parry:
                clipToPlay = Parry;
                break;
            case SoundEffect.HammerBuild:
                clipToPlay = HammerBuild;
                break;
            case SoundEffect.CarbUp:
                clipToPlay = CarbUp;
                break;
            case SoundEffect.PlayerDeath:
                clipToPlay = PlayerDeath;
                break;
            case SoundEffect.MortaterFire:
                clipToPlay = MortaterEffects[0];
                break;
            case SoundEffect.MortaterHit:
                clipToPlay = MortaterEffects[1];
                break;
            case SoundEffect.M1BrownieFire:
                clipToPlay = M1BrownieEffects[0];
                break;
            case SoundEffect.M1BrownieHit:
                clipToPlay = M1BrownieEffects[1];
                break;
            case SoundEffect.Carrot50CaloryFire:
                clipToPlay = Carrot50CaloryEffects[0];
                break;
            case SoundEffect.Carrot50CaloryHit:
                clipToPlay = Carrot50CaloryEffects[1];
                break;
            case SoundEffect.CritHit:
                clipToPlay = CritHit;
                break;
            case SoundEffect.OnionKnightAggro:
                clipToPlay = OnionKnightEffects[0];
                break;
            case SoundEffect.OnionKnightAttack:
                index = Random.Range(1,2);
                clipToPlay = OnionKnightEffects[index];
                break;
            case SoundEffect.OnionKnightCounter:
                clipToPlay = OnionKnightEffects[3];
                break;
            case SoundEffect.MeatleAttack:
                clipToPlay = MeatleEffects[0];
                break;
            case SoundEffect.MeatleIdle:
                clipToPlay = MeatleEffects[1];
                break;
            case SoundEffect.MeatleRoll:
                clipToPlay = MeatleEffects[2];
                break;
            case SoundEffect.FudgeAttack:
                clipToPlay = FudgeEffects[0];
                break;
            case SoundEffect.FudgeIdle:
                clipToPlay = FudgeEffects[1];
                break;
            case SoundEffect.FudgePool:
                clipToPlay = FudgeEffects[2];
                break;
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
        }
        
        return MakeSoundEffect(location, volume, clipToPlay, pitch, isBossEffect, overrideTimeAlive);
        
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
        GlockamoleHit,
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
        HydraSweep,
        HydraSpawnEffects,
        TrollSlam,
        GummyBearAttack,
        GummyBearIdle,
        GummyBearStep,
        GummyZombieIdle,
        GummyZombieAttack,
        GummyBearDeath,
        BenedictLaugh,
        Eat,
        EatEmpty,
        EatFull,
        CauldronTravel,
        AreaDiscover,
        IngredFull,
        LevelCom,
        LevelCom100,
        Parry,
        HammerBuild,
        CarbUp,
        PlayerDeath,
        MortaterFire,
        MortaterHit,
        M1BrownieFire,
        M1BrownieHit,
        Carrot50CaloryFire,
        Carrot50CaloryHit,
        CritHit,
        OnionKnightAggro,
        OnionKnightAttack,
        OnionKnightCounter,
        MeatleIdle,
        MeatleAttack,
        MeatleRoll,
        FudgeIdle,
        FudgeAttack,
        FudgePool,
    }
}

