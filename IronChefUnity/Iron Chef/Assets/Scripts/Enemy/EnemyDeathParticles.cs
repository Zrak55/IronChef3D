using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour
{
    Transform character;
    public int numParticles;
    List<GameObject> rbs;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveParticles()
    {
        foreach (var go in rbs)
            go.GetComponent<DeadEnemyParticle>().chasing = true;
        StartCoroutine(GetMoving());
    }

    IEnumerator GetMoving()
    {
        bool movedSomething = true;
        float t = 0;
        for (int i = 0; i < rbs.Count; i++)
        {
            if (rbs[i] != null)
                Destroy(rbs[i].GetComponent<Rigidbody>());
        }
        while(movedSomething)
        {
            t += Time.deltaTime;
            movedSomething = false;
            for (int i = 0; i < rbs.Count; i++)
            {
                var rb = rbs[i];
                if(rb != null)
                {
                    movedSomething = true;
                    rb.transform.position = Vector3.MoveTowards(rb.transform.position, character.position + Vector3.up, t * 3);
                }
            }
            yield return null;
        }
        Destroy(gameObject);
    }




    public void MakeParticles(GameObject particle)
    {
        character = FindObjectOfType<CharacterMover>().transform;
        rbs = new List<GameObject>();
        for (int i = 0; i < numParticles; i++)
        {
            rbs.Add(Instantiate(particle, transform.position, Quaternion.identity));
            rbs[i].GetComponent<Rigidbody>().AddForce((Vector3.up + (Vector3.right * Random.Range(-0.5f, 0.5f)) + (Vector3.forward * Random.Range(-0.5f, 0.5f))).normalized * force, ForceMode.Impulse);
        }
        Invoke("MoveParticles", 2f);
    }
}
