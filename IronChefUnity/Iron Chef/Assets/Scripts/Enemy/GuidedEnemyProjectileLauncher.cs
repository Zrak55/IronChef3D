using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedEnemyProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint, projectilePoint;
    public float randomRadiusFromPlayer;

    public float height;
    public float time;

    public bool ShouldShowWarning;
    public GameObject PlayerWarningPrefab;
    private GameObject item;

    public SoundEffectSpawner.SoundEffect sound;
    public SoundEffectSpawner.SoundEffect projectileTravelSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ProjectileAppear()
    {
        item = Instantiate(projectile, projectilePoint.position, Quaternion.identity);
        item.transform.SetParent(projectilePoint);
        item.GetComponent<Collider>().enabled = false;
        foreach(var item in GetComponentsInChildren<ParticleSystem>())
        {
            item.Stop();
        }
    }

    public void ProjectileDissapear()
    {
        Destroy(item);
    }

    public void LaunchProjectile()
    {
        var proj = Instantiate(projectile, launchPoint.position, Quaternion.identity).GetComponent<GuidedEnemyArcingProjectile>();
        SoundEffectSpawner.soundEffectSpawner.MakeSoundEffect(launchPoint.position, sound);
        Vector3 target = FindObjectOfType<CharacterMover>().transform.position;
        if (randomRadiusFromPlayer > 0)
            target += (new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * randomRadiusFromPlayer);
        SoundEffectSpawner.soundEffectSpawner.MakeFollowingSoundEffect(proj.transform, projectileTravelSound);
        //Set position to ground;
        Ray r = new Ray(target + (Vector3.up * 2), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, 1 << LayerMask.NameToLayer("Terrain")))
            target = hit.point;


        proj.Launch(target, time, height);
        if (ShouldShowWarning)
            Destroy(Instantiate(PlayerWarningPrefab, target, Quaternion.identity), 2f);
    }
}
