using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _50CheeseStrike : PlayerPower
{
    GameObject particle;
    float SinglePunchDamage;
    float NumPunches;
    float Duration;
    float Width;
    float Depth;
    float Height;
    float scaleAmount;
    Transform model;
    List<EnemyHitpoints> allhits;

    // Start is called before the first frame update
    void Start()
    {
        model = GetComponent<CharacterMover>().model.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        particle = power.prefabs[0];
        SinglePunchDamage = power.values[0];
        NumPunches = power.values[1];
        Duration = power.values[2];
        Width = power.values[3];
        Depth = power.values[4];
        Height = power.values[5];
        scaleAmount = power.values[6];
    }
    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        StartCoroutine(RepeatPower());
        StartCoroutine(SpawnEffects());

    }

    IEnumerator SpawnEffects()
    {
        float cTime = 0;
        while(cTime <= Duration)
        {

            Vector3 offset;
            float xOffset = 1;
            if (Random.Range(0, 2) < 1)
                xOffset = -1;
            if (Random.Range(0, 5) < 1)
                xOffset = 0;
            float yVal = 4.5f;
            if (xOffset != 0)
                yVal = Random.Range(0f, 4f);
            offset = model.transform.right * xOffset * 2;
            offset.y = yVal;


            Instantiate(particle, model.transform.position + offset, model.rotation);

            yield return new WaitForSeconds(0.05f);
            cTime += .05f;
        }
    }

    IEnumerator RepeatPower()
    {
        int numPunches = 0;
        float tickRate = Duration / NumPunches;

        float currentDamage = SinglePunchDamage;

        while (numPunches < NumPunches)
        {
            Debug.Log("Try Hit");
            allhits = new List<EnemyHitpoints>();

            
            bool hitSomething = false;
            var hits = IronChefUtils.GetCastHits(Width, Height, Depth, transform.position, model.rotation);
            foreach (var h in hits)
            {
                var hp = h.GetComponentInParent<EnemyHitpoints>();
                if (hp != null && allhits.Contains(hp) == false)
                {
                    hitSomething = true;
                    hp.TakeDamage(currentDamage);
                    allhits.Add(hp);
                }
            }
            
                if (hitSomething)
                {
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.FiftyPunches);
                    currentDamage *= scaleAmount;
                }
                else
                {
                    SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.PunchMiss);

                }
            
            


            yield return new WaitForSeconds(tickRate);
            numPunches++;
        }
    }
    
}
