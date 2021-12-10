using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSoundOnSelect : MonoBehaviour
{
    static Transform audioLocation;

    private void Awake()
    {
        if(audioLocation == null)
            audioLocation = FindObjectOfType<AudioListener>().transform;

    }


    public void PlaySelectSound()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(audioLocation.position, SoundEffectSpawner.SoundEffect.MenuSelect);
    }
    
    
}
