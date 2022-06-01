using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerStats))]
public class PlayerHitpoints : MonoBehaviour
{
    [Tooltip("Float for how much time the player's invicibility after being hit lasts.")]
    [SerializeField] private float IFramesAmount;
    private PlayerStats playerStats;
    private Animator anim;
    private bool isIFrames = false;
    PlayerCamControl pcam;
    SoundEffectSpawner sounds;
    PlayerDamageTakenModifierController mods;
    bool isDead = false;
    bool isGetHitSoundDelay = false;
    [SerializeField] private GameObject HealEffect;
    bool internalParticleCooldown = false;

    public static int CombatCount = 0;

    bool tookDamageDuringBoss = true;
    bool fightingBoss = false;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        anim = gameObject.GetComponentInChildren<Animator>();
        sounds = SoundEffectSpawner.soundEffectSpawner;
        pcam = FindObjectOfType<PlayerCamControl>();
        mods = GetComponent<PlayerDamageTakenModifierController>();
        isDead = false;
        Invoke("combatReset", 0.1f);
    }

    private void combatReset()
    {
        CombatCount = 0;
        foreach (var e in FindObjectsOfType<EnemyBehaviorTree>())
        {
            if (e.isAggrod())
                CombatCount++;

        }
    }
    public static bool InCombat()
    {
        return CombatCount > 0;
    }

    public void StartBossCombat()
    {
        tookDamageDuringBoss = false;
        fightingBoss = true;
    }

    public bool EndBossCombat()
    {
        fightingBoss = false;
        return tookDamageDuringBoss;
    }    


    public void TakeDamage(float amount, SoundEffectSpawner.SoundEffect sound = SoundEffectSpawner.SoundEffect.Cleaver, bool IgnoresIframes = false)
    {
        if(!isDead)
        {
            if (isIFrames == false || IgnoresIframes)
            {
                //TODO: Play animation of getting hit here. I'm not sure yet if the animation
                //will include knockback or not so I won't include it yet.

                if (!IgnoresIframes)
                    InvincibilityFrame(IFramesAmount);

                //Get Modifier effects
                mods.DoModifierSpecials(amount);
                amount = amount * mods.getMultiplier();

                playerStats.CurrentHP -= amount;

                pcam.ShakeCam(Mathf.Max(amount, 30) / 12.5f, 0.5f);

                if (!isGetHitSoundDelay)
                {
                    sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Grunt);
                    isGetHitSoundDelay = true;
                    Invoke("UndoHitSoundDelay", IFramesAmount);
                }

                if (playerStats.CurrentHP <= 0)
                {
                    Die();
                }

                if (sound != SoundEffectSpawner.SoundEffect.Cleaver)
                {
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, sound);
                }


            }
        }
       
    }

    public void UndoHitSoundDelay()
    {
        isGetHitSoundDelay = false;
    }

    public void InvincibilityFrame(float time)
    {
        isIFrames = true;
        Invoke("isIFramesEnd", time);
    }

    public void Die()
    {
        FindObjectOfType<ChapterManager>().ShowLoseScreen();
        ChapterManager.deathsThisLevel++;
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.PlayerDeath);
        isDead = true;
        //TODO: Play death animation before deletion

    }

    private void isIFramesEnd()
    {
        isIFrames = false;
    }

    public void RestoreHP(float amount)
    {
        if(GetPercent() != 1)
        {
            if (!internalParticleCooldown)
            {
                Destroy(Instantiate(HealEffect, transform), 5f);
                internalParticleCooldown = true;
                Invoke("ResetParticleCooldown", 0.5f);
            }
        }
        
        playerStats.CurrentHP = Mathf.Clamp(playerStats.CurrentHP + amount, 0, playerStats.MaximumHP);
    }

    void ResetParticleCooldown()
    {
        internalParticleCooldown = false;
    }

    public bool isIframed()
    {
        return isIFrames;
    }

    public float GetMax()
    {
        return playerStats.MaximumHP;
    }
    public void SetMax(float hp)
    {
        playerStats.MaximumHP = hp;
    }
    public void SetCurrent(float hp)
    {
        playerStats.CurrentHP = hp;
    }
    public float GetPercent()
    {
        return playerStats.CurrentHP / playerStats.MaximumHP;
    }
    

    
}
