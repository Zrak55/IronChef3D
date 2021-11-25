using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidedEnemyProjectileLauncher : MonoBehaviour
{
    public GameObject projectile;
    public Transform launchPoint;
    public float randomRadiusFromPlayer;

    public float height;
    public float time;

    public bool ShouldShowWarning;
    public GameObject PlayerWarningPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchProjectile()
    {
        var proj = Instantiate(projectile, launchPoint.position, Quaternion.identity).GetComponent<GuidedEnemyArcingProjectile>();
        Vector3 target = FindObjectOfType<CharacterMover>().transform.position;
        if (randomRadiusFromPlayer > 0)
            target += (new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)).normalized * randomRadiusFromPlayer);

        //Set position to ground;
        Ray r = new Ray(target + (Vector3.up * 100), Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, 1 << LayerMask.NameToLayer("Terrain")))
            target = hit.point;


        proj.Launch(target, time, height);
        if (ShouldShowWarning)
            Destroy(Instantiate(PlayerWarningPrefab, target, Quaternion.identity), 2f);
    }
}
