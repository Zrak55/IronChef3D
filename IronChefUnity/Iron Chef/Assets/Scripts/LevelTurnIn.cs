using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTurnIn : MonoBehaviour
{
    bool delay = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (!delay)
            {
                FinishLevel();
            }

        }
    }

    public void FinishLevel()
    {
        TurnOffDelay();
        FindObjectOfType<LevelProgressManager>().FinishLevel();
    }

    void TurnOffDelay()
    {
        delay = true;
        Invoke("TurnOffTurnOffDelay", 20f);
    }
    void TurnOffTurnOffDelay()
    {
        delay = false;
    }
}
