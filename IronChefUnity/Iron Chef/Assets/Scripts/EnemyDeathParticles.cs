using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathParticles : MonoBehaviour
{
    ParticleSystem ps;
    Transform character;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Invoke("MoveParticles", 1f);
        character = FindObjectOfType<CharacterMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveParticles()
    {
        
        /*
        // Extract copy
        ParticleSystem.Particle[] particles = ps.emission.
        // Do changes
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].position = Vector3.MoveTowards(particleEmitter.particles[i].position, spot.transform.position, Time.deltaTime * speed);
        }
        // Reassign back to emitter
        particleEmitter.particles = particles;
        */
    }

}
