using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapasta : PlayerPower
{
    GameObject targeter;
    GameObject projectile;

    GameObject spawnedTargeter;
    GameObject spawnedProjectile;
    float goSpeed;
    float remainTime = 0f;

    float time;
    float damage;

    Transform throwPoint;



    // Start is called before the first frame update
    void Start()
    {
        throwPoint = GetComponent<PlayerAttackController>().throwPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedProjectile != null)
        {
            if(spawnedProjectile.transform.position != spawnedTargeter.transform.position)
            {
                spawnedProjectile.transform.position = Vector3.MoveTowards(spawnedProjectile.transform.position, spawnedTargeter.transform.position, goSpeed * Time.deltaTime);
            }
            else
            {
                if (remainTime >= 1f)
                {
                    Destroy(spawnedProjectile);
                    Destroy(spawnedTargeter);
                }
                else
                {
                    remainTime += Time.deltaTime;
                }
            }
        }
    }

    public override void DoPowerEffects()
    {
        base.DoPowerEffects();
        spawnedTargeter = Instantiate(targeter, throwPoint.position, throwPoint.rotation);
        spawnedProjectile = Instantiate(projectile, spawnedTargeter.transform.position + (spawnedTargeter.transform.right * 200) + 200*Vector3.up, Quaternion.identity);
        spawnedProjectile.GetComponent<PlayerProjectile>().damage = damage;
        goSpeed = Vector3.Distance(spawnedTargeter.transform.position, spawnedProjectile.transform.position) / time;
        spawnedTargeter.GetComponent<ProjectileLaunch>().Launch(10, GetComponent<CharacterMover>().model.transform.forward, 45);
        remainTime = 0;
    }

    public override void SetScriptableData(PlayerPowerScriptable power)
    {
        base.SetScriptableData(power);
        time = power.values[0];
        damage = power.values[1];
        projectile = power.prefabs[0];
        targeter = power.prefabs[1];
    }
}
