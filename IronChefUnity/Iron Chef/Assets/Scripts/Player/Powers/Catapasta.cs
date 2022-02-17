using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapasta : PlayerPower
{
    GameObject targeter;
    GameObject projectile;
    int numProjectiles;
    float offsetAmount;

    GameObject spawnedTargeter;
    GameObject[] spawnedProjectiles;
    Vector3[] offset;
    float goSpeed;
    float remainTime = 0f;

    float time;
    float damage;

    Transform throwPoint;

    bool[] shouldMove;

    



    // Start is called before the first frame update
    void Start()
    {
        throwPoint = GetComponent<PlayerAttackController>().throwPoint;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < numProjectiles; i++)
        {
            var spawnedProjectile = spawnedProjectiles[i];
            if (spawnedProjectile != null)
            {
                if (shouldMove[i] && spawnedProjectile.transform.position != spawnedTargeter.transform.position + offset[i])
                {
                    spawnedProjectile.transform.position = Vector3.MoveTowards(spawnedProjectile.transform.position, spawnedTargeter.transform.position + offset[i], goSpeed * Time.deltaTime);
                }
                else
                {
                    if(spawnedTargeter != null)
                    {
                        if (spawnedTargeter.GetComponentInChildren<AudioSource>() != null)
                            spawnedTargeter.GetComponentInChildren<AudioSource>().enabled = false;
                    }
                    if (shouldMove[i])
                    {
                        shouldMove[i] = false;
                        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(spawnedTargeter.transform.position, SoundEffectSpawner.SoundEffect.CatapastaHit);
                    }
                    if (remainTime >= 1f)
                    {
                        Destroy(spawnedProjectile);

                        
                    }
                    else
                    {
                        remainTime += Time.deltaTime;
                    }
                }
            }
        }
        if(spawnedTargeter != null)
        {
            bool allDone = true;
            foreach (var o in spawnedProjectiles)
                if (o != null)
                {
                    allDone = false;
                    break;
                }
            if (allDone)
                Destroy(spawnedTargeter);
        }
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        spawnedTargeter = Instantiate(targeter, throwPoint.position, throwPoint.rotation);
        for (int i = 0; i < numProjectiles; i++)
        {
            var spawnedProjectile = Instantiate(projectile, spawnedTargeter.transform.position + (spawnedTargeter.transform.right * 200) + 200 * Vector3.up, Quaternion.identity);
            spawnedProjectile.GetComponent<PlayerProjectile>().damage = damage;
            spawnedProjectiles[i] = spawnedProjectile;
            offset[i] = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * (offsetAmount * Random.Range(0f, 1f));
            goSpeed = Vector3.Distance(spawnedTargeter.transform.position, spawnedProjectile.transform.position) / time;
            shouldMove[i] = true;
        }
        spawnedTargeter.GetComponent<ProjectileLaunch>().Launch(10, IronChefUtils.GetSoftLockDirection(GetComponent<CharacterMover>().model.transform.forward, spawnedTargeter.transform.position, 1 << LayerMask.NameToLayer("Enemy"), 20, true), 45);
        remainTime = 0;
        SoundEffectSpawner.soundEffectSpawner.MakeFollowingSoundEffect(spawnedTargeter.transform, SoundEffectSpawner.SoundEffect.CatapastaFly);
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        time = power.values[0];
        damage = power.values[1];
        projectile = power.prefabs[0];
        targeter = power.prefabs[1];
        numProjectiles = (int)power.values[2];
        offsetAmount = power.values[3];
        spawnedProjectiles = new GameObject[numProjectiles];
        offset = new Vector3[numProjectiles];
        shouldMove = new bool[numProjectiles];
        for (int i = 0; i < numProjectiles; i++)
            shouldMove[i] = false;
    }
}
