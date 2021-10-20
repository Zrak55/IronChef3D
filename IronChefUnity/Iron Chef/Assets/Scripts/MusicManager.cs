using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource normalMusicSource;
    public AudioSource combatMusicSource;
    public List<AudioClip> normalClips;
    public List<AudioClip> combatClips;

    GameObject player;

    int currentNormalClip;
    int currentCombatClip;

    public int combatCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        

        currentNormalClip = 0;
        currentCombatClip = 0;
        normalMusicSource.clip = normalClips[0];
        combatMusicSource.clip = combatClips[0];

    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
            player = FindObjectOfType<CharacterMover>().gameObject;
        updateMusicVolume();
        updateMusicClips();
    }

    void updateMusicVolume()
    {
        if (player != null)
        {

            bool inCombat = combatCount > 0;

            if (inCombat)
            {
                combatMusicSource.volume = Mathf.Clamp(combatMusicSource.volume + Time.deltaTime, 0, 1);
                normalMusicSource.volume = Mathf.Clamp(normalMusicSource.volume - Time.deltaTime, 0, 1);
            }
            else
            {
                combatMusicSource.volume = Mathf.Clamp(combatMusicSource.volume - Time.deltaTime, 0, 1);
                normalMusicSource.volume = Mathf.Clamp(normalMusicSource.volume + (Time.deltaTime / 3f), 0, 1);
            }
        }
        
    }
    void updateMusicClips()
    {
        if(normalMusicSource.isPlaying == false)
        {
            currentNormalClip++;
            if(currentNormalClip >= normalClips.Count)
            {
                currentNormalClip = 0;
            }
            normalMusicSource.clip = normalClips[currentNormalClip];
            normalMusicSource.Play();
        }
        if (combatMusicSource.isPlaying == false)
        {
            currentCombatClip++;
            if (currentCombatClip >= combatClips.Count)
            {
                currentCombatClip = 0;
            }
            combatMusicSource.clip = combatClips[currentCombatClip];
            combatMusicSource.Play();
        }
    }
}
