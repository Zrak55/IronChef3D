using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public SoundEffectSpawner.SoundEffect soundName;

    public void MakeFootstepEffect()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, soundName);
    }
}
