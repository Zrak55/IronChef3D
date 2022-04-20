using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingHolderRotater : MonoBehaviour
{
    PlayerAttackController pattack;


    // Start is called before the first frame update
    void Start()
    {
        pattack = GetComponentInParent<PlayerAttackController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pattack.AimImage.activeSelf)
        {

            Vector3 rot = Camera.main.transform.rotation.eulerAngles;
            rot.x = 0;
            rot.z = 0;
            transform.rotation = Quaternion.Euler(rot);
        }
    }
}
