using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemAwakeSelector : MonoBehaviour
{
    EventSystem e;

    private void Awake()
    {
        e = GetComponent<EventSystem>();
        EventSystem.current = e;
        e.SetSelectedGameObject(e.firstSelectedGameObject);
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
