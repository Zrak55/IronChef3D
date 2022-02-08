using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFoodPickup : MonoBehaviour
{
    bool readyToGive = false;
    EnemyFoodDropper.FoodType food;
    [Space]
    public GameObject deathParticleSystem;
    public GameObject deathParticle;


    // Start is called before the first frame update
    void Start()
    {
        readyToGive = false;
        Invoke("ActivateGive", 2f);
        GetComponent<Rigidbody>().AddForce(Vector3.up * 12, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateGive()
    {
        readyToGive = true;
        var col = IronChefUtils.GetCastHits(1, 1, 1, transform.position, Quaternion.identity, "Player");
        if (col.Count > 0)
            Award();
    }

    public void SetFood(EnemyFoodDropper.FoodType f)
    {
        food = f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (readyToGive)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Award();
            }
        }
    }

    public void Award()
    {
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.MonsterDeath);

        LevelProgressManager.levelProgressManager.AwardFood(food);
        var dp = Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        dp.GetComponent<EnemyDeathParticles>().MakeParticles(deathParticle);

        Destroy(gameObject);
    }
}
