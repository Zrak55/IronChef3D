using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject Tutorial;
    public GameObject selector;
    bool on = false;

    // Start is called before the first frame update
    void Start()
    {
        IronChefUtils.TurnOffCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string s)
    {
        Time.timeScale = 0;
        IronChefUtils.TurnOffCharacter();


        Tutorial.SetActive(true);
        text.text = s;

    }
    public void Dismiss()
    {
        Time.timeScale = 1;
        IronChefUtils.TurnOnCharacter();
        Tutorial.SetActive(false);
    
        if(!on)
        {
            on = true;
            selector.SetActive(true);
        }
    }
}
