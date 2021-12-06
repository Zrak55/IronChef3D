using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadEnemyParticle : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 spin;
    public float spinRateDegrees = 180f;
    public bool chasing = false;
    
    void Start()
    {
        spin = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(spin * spinRateDegrees * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (chasing)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.XpPickup);
                Destroy(gameObject);
            }
        }
    }
}
