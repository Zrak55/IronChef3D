using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusBreath : MonoBehaviour
{
    bool breathing;

    public Transform breathPoint;
    public Transform player;
    float playerHeightRaise;

    public float DamagePerSecond;
    // Start is called before the first frame update
    void Start()
    {
        playerHeightRaise = player.GetComponent<CapsuleCollider>().height / 2;
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
    }

    public void StopBreathing()
    {
        breathing = false;
        GetComponent<MeatosaurusStomp>().DestroyAllRocks();
    }

    public void BreathTick()
    {
        RaycastHit hit;
        Ray ray = new Ray(breathPoint.position, (player.position + (Vector3.up * playerHeightRaise)) - breathPoint.position);
        if (Physics.Raycast(ray, out hit, 1000, 1 << LayerMask.NameToLayer("Player") + 1 << LayerMask.NameToLayer("SpecialBossLayer1")))
        {
            PlayerHitpoints php = hit.collider.gameObject.GetComponent<PlayerHitpoints>();
            if(php == null)
            {
                hit.collider.gameObject.GetComponentInParent<PlayerHitpoints>();
            }

            php?.TakeDamage(DamagePerSecond, SoundEffectSpawner.SoundEffect.Cleaver, true);
            
        }
    }

}
