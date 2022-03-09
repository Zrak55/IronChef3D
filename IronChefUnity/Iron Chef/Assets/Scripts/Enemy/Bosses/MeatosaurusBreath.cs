using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusBreath : MonoBehaviour
{
    bool breathing;

    public Transform breathPoint;
    public Transform player;
    float playerHeightRaise;
    PlayerHitpoints php;
    MeatosaurusBehavior behavior;
    bool haventTipped = false;
    //public GameObject breathEffect;
    EnemyVFXController vfx;
    public int VFXNumber;

    public float DamagePerSecond;

    MeatosaurusLightBreathEffect effect;

    [HideInInspector]
    public bool firstBreath;

    public Transform HeadPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerHeightRaise = player.GetComponent<CapsuleCollider>().height / 2;
        php = FindObjectOfType<PlayerHitpoints>();
        behavior = GetComponent<MeatosaurusBehavior>();
        effect = GetComponent<MeatosaurusLightBreathEffect>();
        firstBreath = true;
        vfx = GetComponent<EnemyVFXController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(breathing)
        {
            BreathTick();
        }
    }

    public void StartBreathing()
    {
        MakeBreathSound();
        breathing = true;
        haventTipped = true;
        //breathEffect.SetActive(true);
        vfx.StartEffect(VFXNumber);
        GetComponent<MeatosaurusStomp>().DestroyRocksAtHead(HeadPoint);
        GetComponent<MeatosaurusStomp>().SwapAllRocks();
    }

    public void WarnHeadRocks()
    {

        GetComponent<MeatosaurusStomp>().WarnRocksAtHead(HeadPoint);
    }

    public void StopBreathing()
    {
        firstBreath = false;
        haventTipped = false;
        breathing = false;
        GetComponent<MeatosaurusStomp>().DestroyAllRocks();
        effect.StopEffect();
        //breathEffect.SetActive(false);
        vfx.EndEffect(VFXNumber);
    }

    public void BreathTick()
    {
        RaycastHit hit;
        Ray ray = new Ray(breathPoint.position, (player.position + (Vector3.up * playerHeightRaise)) - breathPoint.position);
        Debug.DrawRay(breathPoint.position, (player.position + (Vector3.up * playerHeightRaise)) - breathPoint.position, Color.red, Time.deltaTime * 1.1f);
        if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("SpecialBossLayer1")))
        {

        }
        else
        {
            php.TakeDamage(DamagePerSecond * Time.deltaTime, SoundEffectSpawner.SoundEffect.Cleaver, true);
            if(haventTipped)
            {
                if(!firstBreath)
                    behavior.Tip("Take cover behind the rocks to avoid his fiery breath!");
            }
        }
    }

    public void MakeWindupSound()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexFireBreathWindUp);
    }

    public void MakeBreathSound()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexFireBreath);
    }

}
