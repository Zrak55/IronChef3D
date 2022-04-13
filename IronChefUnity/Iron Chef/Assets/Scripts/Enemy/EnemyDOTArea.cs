using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDOTArea : MonoBehaviour
{
    public float dps;
    public float radius;
    List<PlayerHitpoints> hitPlayer;
    public GameObject particles;
    private GameObject instParticles;


    private void Awake()
    {
        hitPlayer = new List<PlayerHitpoints>();
        DoSpawnThings();
        Destroy(gameObject, 5);
    }

    void Update()
    {
        DoDmg();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 15, 1 << LayerMask.NameToLayer("Terrain")))
        {
            transform.position = Vector3.MoveTowards(transform.position, hit.point, 15 * Time.deltaTime);
        }

    }
    public void DoSpawnThings()
    {
        transform.localScale = new Vector3(.01f, .01f, .01f);
        instParticles = Instantiate(particles, transform);
        instParticles.transform.localPosition = Vector3.zero;
    }

    void DoDmg()
    {
        var allHits = Physics.OverlapSphere(transform.position, transform.localScale.magnitude);
        foreach (var col in allHits)
        {
            var eh = col.GetComponentInParent<PlayerHitpoints>();
            if (eh != null && hitPlayer.Contains(eh) == false)
            {
                hitPlayer.Add(eh);
            }
        }
        foreach (var e in hitPlayer)
        {
            e.TakeDamage(dps * Time.deltaTime);
        }
        hitPlayer.Clear();

    }
}
