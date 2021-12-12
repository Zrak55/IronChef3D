using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusLightBreathEffect : MonoBehaviour
{
    public GameObject[] TailLights;
    public ParticleSystem RibcageParticles;

    bool stopping = false;

    private void Start()
    {
        //StartCoroutine(TestFire());
    }

    IEnumerator TestFire()
    {
        PlayEffect();
        yield return new WaitForSeconds(7f);
        StopEffect();
    }
    public void PlayEffect()
    {
        //RibcageParticles.Play();
        StartCoroutine(ManageTailLights());
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexFireBreathWindUp);
    }

    public void StopEffect()
    {
        //RibcageParticles.Stop();

        StartCoroutine(TurnLightsOff());
    }

    IEnumerator ManageTailLights()
    {

        stopping = false;

        float flickerRate = 20f;
        float percentRandomRange = 0.1f;

        float[] averageIntensity = new float[TailLights.Length];
        for (int i = 0; i < TailLights.Length; i++)
        {
            averageIntensity[i] = 0;
            TailLights[i].transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
        }

        float growRate = 60f;
        float maxTarget = 120f;
        float maxTime = 3f;



        float cTime = 0;
        while(cTime < maxTime)
        {
            cTime += Time.deltaTime;
            int numLights = TailLights.Length;
            for(int i = 0; i < numLights; i++)
            {
                averageIntensity[i] = Mathf.Min(averageIntensity[i] + growRate * Time.deltaTime, maxTarget);

                float scale = Mathf.MoveTowards(TailLights[i].transform.localScale.x, averageIntensity[i], growRate * Time.deltaTime);

                TailLights[i].transform.localScale = new Vector3(scale, scale, scale);

            }


            yield return null;
        }

        for(int i = 0; i < TailLights.Length; i++)
        {
            averageIntensity[i] = maxTarget;

            float scale = Mathf.MoveTowards(TailLights[i].transform.localScale.x, averageIntensity[i], growRate * Time.deltaTime);

            TailLights[i].transform.localScale = new Vector3(scale, scale, scale);
        }

    }

    IEnumerator TurnLightsOff()
    {
        stopping = true;
        bool modifiedSomething = true;
        float rate = 100f;
        while(modifiedSomething)
        {
            modifiedSomething = false;
            foreach(var l in TailLights)
            {
                if(l.transform.localScale.x > 0)
                {
                    float scale = Mathf.Max(l.transform.localScale.x - (rate * Time.deltaTime), 0);
                    l.transform.localScale = new Vector3(scale, scale, scale);
                    modifiedSomething = true;
                }
            }
            yield return null;
        }
    }
}
