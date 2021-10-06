using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PreLevelWindow : MonoBehaviour
{
    public string levelName;
    public Text descText;

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(GetComponentInChildren<Button>().gameObject);
    }
    public void Hide()
    {
        EventSystem.current.SetSelectedGameObject(GetComponentInParent<GameObject>().GetComponentInChildren<Button>().gameObject);
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
