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

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        anim = gameObject.GetComponentInChildren<Animator>();
        sounds = FindObjectOfType<SoundEffectSpawner>();
        pcam = FindObjectOfType<PlayerCamControl>();
    }

    public void TakeDamage(float amount, SoundEffectSpawner.SoundEffect sound = SoundEffectSpawner.SoundEffect.Cleaver)
    {
        //TODO: Check player powers/status effects for taking damage
        if (isIFrames == false)
        {
            Debug.Log("Hurt!");

            //TODO: Play animation of getting hit here. I'm not sure yet if the animation
            //will include knockback or not so I won't include it yet.

            InvincibilityFrame(IFramesAmount);

            playerStats.CurrentHP -= amount;

            pcam.ShakeCam(amount / 5f, amount * 0.4f / 5f);

            sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.Grunt);

            if (playerStats.CurrentHP <= 0)
            {
                Die();
            }

            if(sound != SoundEffectSpawner.SoundEffect.Cleaver)
            {
                FindObjectOfType<SoundEffectSpawner>().MakeSoundEffect(transform.position, sound);
            }

            
        }
    }


    public void InvincibilityFrame(float time)
    {
        isIFrames = true;
        Invoke("isIFramesEnd", time);
    }

    public void Die()
    {
        FindObjectOfType<ChapterManager>().ShowLoseScreen();

        //TODO: Play death animation before deletion

    }

    private void isIFramesEnd()
    {
        isIFrames = false;
    }

    public void RestoreHP(float amount)
    {
        playerStats.CurrentHP = Mathf.Clamp(playerStats.CurrentHP + amount, 0, playerStats.MaximumHP);
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
    

    
}
