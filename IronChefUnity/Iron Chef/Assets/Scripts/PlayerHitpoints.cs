using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitpoints : MonoBehaviour
{
    //This maybe should be moved to playerStats
    [Tooltip("Float for how much time the player's invicibility after being hit lasts.")]
    [SerializeField] private float IFramesAmount;
    private PlayerStats playerStats;
    private Animator anim;
    private bool isIFrames = false;

    private void Awake()
    {
        playerStats = gameObject.GetComponent<PlayerStats>();
        anim = gameObject.GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        //TODO: Check powers/status effects for taking damage
        if (isIFrames == false)
        {
            Debug.Log("Hurt!");

            //Play animation of getting hit here. I'm not sure yet if the animation
            //will include knockback or not so I won't include it yet.
            isIFrames = true;
            Invoke("isIFramesEnd", IFramesAmount);

            playerStats.CurrentHP -= amount;

            if (playerStats.CurrentHP <= 0)
            {
                Die();
            }
            Debug.Log(isIFrames);
        }
    }
    public void Die()
    {
        //TODO: Say you lose, restart level, other things of that nature

        //TODO: Play death animation before deletion

    }

    private void isIFramesEnd()
    {
        isIFrames = false;
    }
}
