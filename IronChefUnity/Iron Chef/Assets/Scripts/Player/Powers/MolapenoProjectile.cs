using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ProjectileLaunch))]
public class MolapenoProjectile : MonoBehaviour
{
    float dps;
    float force;
    float launchAngle;
    float duration;
    float radius;

    bool hasSpawned = false;

    public GameObject fireArea;


    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (checkCollider(collision.collider))
        {
            DoCollideThings(collision.contacts[0].point);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(checkCollider(other))
        {
            DoCollideThings(other.transform.position);
        }
    }

    private void DoCollideThings(Vector3 point)
    {
        if(!hasSpawned)
        {
            hasSpawned = true;
            var area = Instantiate(fireArea, point + new Vector3(0, 0.5f, 0), Quaternion.identity).GetComponent<MolapenoArea>();
            area.dps = dps;
            area.radius = radius;
            area.DoSpawnThings();
            area.DestroySelf(duration);

            SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.MalapenoExplosion);

            Destroy(gameObject);

        }

    }

    public void SetData(float dmg, float power, float angle, float dur, float rad)
    {
        dps = dmg;
        force = power;
        launchAngle = angle;
        duration = dur;
        radius = rad;

        GetComponent<ProjectileLaunch>().Launch(force, transform.forward, angle);
    }

    public bool checkCollider(Collider other)
    {
        return (other.GetComponentInParent<PlayerStats>() == null && other.GetComponent<PlayerStats>() == null);
           
    }
}
