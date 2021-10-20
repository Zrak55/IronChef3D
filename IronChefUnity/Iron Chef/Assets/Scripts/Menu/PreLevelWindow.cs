using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreLevelWindow : MonoBehaviour
{
    public string levelName;
    public Text descText;
    public GameObject PlayButton;

    private void Awake()
    {
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        
        EventSystem.current.SetSelectedGameObject(transform.parent.GetComponentInChildren<Button>().gameObject);
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
