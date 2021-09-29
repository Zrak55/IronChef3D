using UnityEngine;

public class PlayerAudioEvents : MonoBehaviour
{
    public SoundEffectSpawner sfx;

    private void Awake()
    {
        sfx = FindObjectOfType<SoundEffectSpawner>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeFootstep()
    {
        //sfx.MakeSoundEffect(transform.position, 0.5f, SoundEffectSpawner.SoundEffect.Cleaver);
    }
}
