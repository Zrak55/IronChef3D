using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppliancePowerPage : MonoBehaviour
{
    public Button lastButton;


    private void Awake()
    {
        if (lastButton == null)
            lastButton = GetComponentInChildren<Button>();

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
