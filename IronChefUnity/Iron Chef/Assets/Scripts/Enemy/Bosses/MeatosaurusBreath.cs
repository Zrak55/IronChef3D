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
    public GameObject breathEffect;

    public float DamagePerSecond;
    // Start is called before the first frame update
    void Start()
    {
        playerHeightRaise = player.GetComponent<CapsuleCollider>().height / 2;
        php = FindObjectOfType<PlayerHitpoints>();
        behavior = GetComponent<MeatosaurusBehavior>();
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
        breathing = true;
        haventTipped = true;
        breathEffect.SetActive(true);
    }

    public void StopBreathing()
    {
        haventTipped = false;
        breathing = false;
        GetComponent<MeatosaurusStomp>().DestroyAllRocks();
        breathEffect.SetActive(false);
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
                behavior.Tip("Take cover behind the rocks to avoid his fiery breath!");
            }
        }
    }

}
