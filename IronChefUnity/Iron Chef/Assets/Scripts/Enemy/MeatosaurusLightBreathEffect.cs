using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusLightBreathEffect : MonoBehaviour
{
    public GameObject[] TailLights;
    public ParticleSystem RibcageParticles;

    bool stopping = false;

    public void PlayEffect()
    {
        //RibcageParticles.Play();
        StartCoroutine(ManageTailLights());
    }

    public void StopEffect()
    {
        //RibcageParticles.Stop();

        StartCoroutine(TurnLightsOff());
    }

    IEnumerator ManageTailLights()
    {

        stopping = false;

        float flickerRate = 2f;
        float percentRandomRange = 0.25f;

        float[] averageIntensity = new float[TailLights.Length];
        for (int i = 0; i < TailLights.Length; i++)
            averageIntensity[i] = 0;

        float growRate = 4f;
        float maxTarget = 2f;
        float maxTime = 3f;

        float turnOnRate = TailLights.Length / maxTime;


        float cTime = 0;
        while(cTime < maxTime)
        {
            cTime += Time.deltaTime;
            int numLights = (int)Mathf.Clamp(Mathf.Ceil(cTime * turnOnRate), 1, TailLights.Length);
            for(int i = 0; i < numLights; i++)
            {
                averageIntensity[i] += Mathf.Min(growRate * Time.deltaTime, maxTarget);

                float randomIntesity = Mathf.Max(averageIntensity[i] + Random.Range(-averageIntensity[i] * percentRandomRange, averageIntensity[i] * percentRandomRange), 0);

                float scale = Mathf.MoveTowards(TailLights[i].transform.localScale.x, randomIntesity, flickerRate * Time.deltaTime);

                TailLights[i].transform.localScale = new Vector3(scale, scale, scale);

            }


            yield return null;
        }

        for(int i = 0; i < TailLights.Length; i++)
        {
            averageIntensity[i] = maxTarget;
        }

        while (!stopping)
        {
            for (int i = 0; i < TailLights.Length; i++)
            {
                float randomIntesity = Mathf.Max(averageIntensity[i] + Random.Range(-averageIntensity[i] * percentRandomRange, averageIntensity[i] * percentRandomRange), 0);

                float scale = Mathf.MoveTowards(TailLights[i].transform.localScale.x, randomIntesity, flickerRate * Time.deltaTime);

                TailLights[i].transform.localScale = new Vector3(scale, scale, scale);


            }


            yield return null;
        }
    }

    IEnumerator TurnLightsOff()
    {
        stopping = true;
        bool modifiedSomething = true;
        float rate = 1.5f;
        while(modifiedSomething)
        {
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
