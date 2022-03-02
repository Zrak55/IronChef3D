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
        Invoke("DestroySelf", 5f);
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
        StartCoroutine("Spawn");
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

    void DestroySelf()
    {
        StartCoroutine(DeSpawn());
    }

    private IEnumerator Spawn()
    {
        while (transform.localScale != new Vector3(1, 1, 1))
        {
            transform.localScale += new Vector3(.01f, .01f, .01f);
            yield return new WaitForSeconds(.01f);
        }
        yield return null;
    }

    private IEnumerator DeSpawn()
    {
        while (transform.localScale != new Vector3(0, 0, 0))
        {
            transform.localScale -= new Vector3(.01f, .01f, .01f);
            yield return new WaitForSeconds(.005f);
        }
        Destroy(gameObject);
        yield return null;
    }
}
