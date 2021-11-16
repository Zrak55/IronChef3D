using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatosaurusStomp : MonoBehaviour
{
    public List<Transform> rockPoints;
    public float fallDelay;
    public GameObject rockPrefab;
    public GameObject targeterPrefab;
    public int rocksPerStomp;
    List<Transform> chosenPoints;
    List<GameObject> currentRocks;
    CharacterMover player;
    [SerializeField]
    private float force;
    [SerializeField]
    private float rockDamage;
    public float rockFallAnimTime;
    PlayerCamControl pcam;

    [SerializeField]
    private EnemyBasicAttackbox hitbox;

    private void Start()
    {
        chosenPoints = new List<Transform>();
        currentRocks = new List<GameObject>();
        player = FindObjectOfType<CharacterMover>();
        pcam = FindObjectOfType<PlayerCamControl>();

        ResetStomp();
    }

    public void ResetStomp()
    {
        chosenPoints.Clear();
    }

    public void DoStomp()
    {
        pcam.ShakeCam(5, 2);
        hitbox.DoCollisionThings();
        for(int i = 0; i < rocksPerStomp; i++)
        {
            int rand = 0;
            bool goodPoint = false;
            int attempts = 0;
            while(!goodPoint)
            {
                rand = Random.Range(0, rockPoints.Count);
                if (!chosenPoints.Contains(rockPoints[i]))
                {
                    goodPoint = true;
                }
                else if(attempts >= 20)
                {
                    goodPoint = true;
                }
                attempts++;
            }
            chosenPoints.Add(rockPoints[rand]);
            Destroy(Instantiate(targeterPrefab, rockPoints[rand].position, Quaternion.identity), fallDelay);
            StartCoroutine(SpawnStomp(fallDelay, rockPoints[rand]));
        }
        
    }

    IEnumerator SpawnStomp(float t, Transform target)
    {
        yield return new WaitForSeconds(t);
        var newGo = Instantiate(rockPrefab, target.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        currentRocks.Add(newGo);

        yield return new WaitForSeconds(rockFallAnimTime);

        if(Vector3.Distance(target.position, player.transform.position) <= 6)
        {

            Vector3 dir = (player.transform.position - newGo.transform.position).normalized;
            float force = 50;
            player.ForceDirection((dir * force));
            player.GetComponent<PlayerHitpoints>().TakeDamage(rockDamage, SoundEffectSpawner.SoundEffect.Cleaver);
        }

    }


    public void DestroyAllRocks()
    {
        foreach (var go in currentRocks)
            Destroy(go);
        currentRocks.Clear();
        ResetStomp();
    }
}
