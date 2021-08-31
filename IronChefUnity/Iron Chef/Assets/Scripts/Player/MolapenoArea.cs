using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MolapenoArea : MonoBehaviour
{
    public float dps;
    public float radius;
    List<EnemyHitpoints> hitEnemies;
    public GameObject particles;
    private GameObject instParticles;
    

    private void Awake()
    {

        hitEnemies = new List<EnemyHitpoints>();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoDmg();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 15, 1 << LayerMask.NameToLayer("Terrain")))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, 15 * Time.deltaTime);
        }

    }
    public void DoSpawnThings()
    {
        instParticles = Instantiate(particles, transform);
        instParticles.transform.localPosition = Vector3.zero;
        scaleParticles();
    }

    void DoDmg()
    {
        var allHits = Physics.OverlapSphere(transform.position, radius);
        foreach(var col in allHits)
        {
            var eh = col.GetComponentInParent<EnemyHitpoints>();
            if(eh != null && hitEnemies.Contains(eh) == false)
            {
                hitEnemies.Add(eh);
            }
        }
        foreach(var e in hitEnemies)
        {
            e.TakeDamage(dps * Time.deltaTime);
        }
        hitEnemies.Clear();

    }

    public void DestroySelf(float duration)
    {
        Destroy(gameObject, duration);
    }

    private void scaleParticles()
    {
        foreach(var ps in instParticles.GetComponentsInChildren<ParticleSystem>())
        {
            var main = ps.main;
            main.startSpeed = 10 * (radius / FindObjectOfType<Molapeno>().baseRadius);

        }
        
    }
}
