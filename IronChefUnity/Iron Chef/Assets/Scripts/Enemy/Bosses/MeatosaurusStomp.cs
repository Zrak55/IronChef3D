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

    public Material BurningRockMat;

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

    public void DestroyRocksAtHead(Transform head)
    {
        foreach(var go in currentRocks)
        {
            if(Vector3.Distance(head.position, go.transform.position) <= 13)
            {
                Destroy(go);
            }
        }    
    }

    public void WarnRocksAtHead(Transform head)
    {
        foreach (var go in currentRocks)
        {
            if (Vector3.Distance(head.position, go.transform.position) <= 13)
            {
                var mrs = go.GetComponentsInChildren<MeshRenderer>();
                foreach(var mr in mrs)
                {
                    mr.material = BurningRockMat;
                }
            }
        }
        DynamicGI.UpdateEnvironment();
    }

    public void SwapAllRocks()
    {
        foreach (var go in currentRocks)
        {
            if (go != null)
            {
                var mrs = go.GetComponentsInChildren<MeshRenderer>();
                foreach (var mr in mrs)
                {
                    mr.material = BurningRockMat;

                }
            }
        }
        DynamicGI.UpdateEnvironment();
    }

    public void DoStomp()
    {
        pcam.ShakeCam(5, 2);
        hitbox.DoCollisionThings();
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(transform.position, SoundEffectSpawner.SoundEffect.TrexStomp);
        for(int i = 0; i < rocksPerStomp; i++)
        {
            int rand = 0;
            bool goodPoint = false;
            int attempts = 0;
            while(!goodPoint)
            {
                rand = Random.Range(0, rockPoints.Count);
                if (!chosenPoints.Contains(rockPoints[rand]) && Vector3.Distance(rockPoints[rand].transform.position, transform.position) >= 12)
                {
                    goodPoint = true;
                }
                else if(attempts >= 50)
                {
                    goodPoint = true;
                }
                attempts++;
            }
            chosenPoints.Add(rockPoints[rand]);
            Destroy(Instantiate(targeterPrefab, rockPoints[rand].position + (Vector3.up * 0.25f), Quaternion.identity), fallDelay);
            StartCoroutine(SpawnStomp(fallDelay, rockPoints[rand]));
        }
        
    }

    IEnumerator SpawnStomp(float t, Transform target)
    {
        yield return new WaitForSeconds(t);
        var newGo = Instantiate(rockPrefab, target.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
        currentRocks.Add(newGo);
        foreach (var col in newGo.GetComponentsInChildren<Collider>())
            col.enabled = false;

        yield return new WaitForSeconds(rockFallAnimTime);

        if(Vector3.Distance(target.position, player.transform.position) <= 6)
        {

            Vector3 dir = (player.transform.position - newGo.transform.position);
        
            dir.y = 0;

            if (dir.magnitude < 1)
                dir = player.transform.right;

            dir = dir.normalized;
            float force = 50;
            player.ForceDirection((dir * force));
            player.GetComponent<PlayerHitpoints>().TakeDamage(rockDamage, SoundEffectSpawner.SoundEffect.Cleaver);
        }

        yield return new WaitForSeconds(0.2f);

        foreach (var col in newGo.GetComponentsInChildren<Collider>())
            col.enabled = true;

    }


    public void DestroyAllRocks()
    {
        foreach (var go in currentRocks)
            if(go != null)
                Destroy(go);
        currentRocks.Clear();
        ResetStomp();
    }
}
