using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterZoneTrigger : MonoBehaviour
{
    public string LocationName;
    PlayerHUDManager hud;



    // Start is called before the first frame update
    void Start()
    {
        hud = FindObjectOfType<PlayerHUDManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            DisplayLocation();
        }
    }

    void DisplayLocation()
    {
        hud.DisplayLocationName(LocationName);
    }
}
