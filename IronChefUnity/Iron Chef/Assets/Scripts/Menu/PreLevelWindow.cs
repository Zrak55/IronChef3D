using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreLevelWindow : MonoBehaviour
{
    public string levelName;
    public Text descText;

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    public void Play()
    {
        FindObjectOfType<MenuController>().PlayGame(levelName);
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
