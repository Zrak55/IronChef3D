using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeselectColorReset : MonoBehaviour
{
    Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != gameObject)
        {
            var myColor = btn.colors;
            myColor.normalColor = Color.white;
            btn.colors = myColor;
        }
        else
        {
            var myColor = btn.colors;
            myColor.normalColor = new Color(255f/255f, 155f/255f, 0);
            btn.colors = myColor;
        }

    }
}
