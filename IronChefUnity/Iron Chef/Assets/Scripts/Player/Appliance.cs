using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appliance : MonoBehaviour
{
    public PlayerApplianceScriptable applianceScriptable;

    private void Awake()
    {
        ApplyEffects();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void ApplyEffects()
    {

    }
}
