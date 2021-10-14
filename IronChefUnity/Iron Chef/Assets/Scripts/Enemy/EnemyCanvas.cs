using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCanvas : MonoBehaviour
{
    PlayerCameraSetup pcam;
    public EnemyHitpoints hp;
    public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        pcam = FindObjectOfType<PlayerCameraSetup>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pcam.cam.transform);
        IronChefUtils.moveABar(hpSlider, hp.GetPercentHP());
    }
}
