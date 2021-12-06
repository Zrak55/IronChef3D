using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Dated script. EnemyMultiAttackboxController now contains all functionality.
public class CrustaceanAnimationEventHandler : MonoBehaviour
{
    public EnemyMultiAttackboxController behavior;
    public SoundEffectSpawner sounds;

    // Start is called before the first frame update
    void Start()
    {
        behavior = GetComponentInParent<EnemyMultiAttackboxController>();
        sounds = SoundEffectSpawner.soundEffectSpawner;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RightClawOn()
    {
        //behavior.TurnHitboxOn(0);
    }

    public void LeftClawOn()
    {
        //behavior.TurnHitboxOn(1);

    }
    public void RightClawOff()
    {
        //behavior.TurnHitboxOff(0);
    }

    public void LeftClawOff()
    {
        //behavior.TurnHitboxOff(1);

    }

    public void WalkSound()
    {
        sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.CrabWalk);
    }
    public void SnapSound()
    {

        sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.CrabSnapClose);
    }
    public void SnapOpenSound()
    {

        sounds.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.CrabSnapOpen);
    }

}
