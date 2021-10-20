using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TutorialManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject Tutorial;
    public GameObject selector;
    public GameObject continueBtn;
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

        EventSystem.current.SetSelectedGameObject(continueBtn);

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
            var btn = selector.GetComponentInChildren<Button>();
            EventSystem.current.SetSelectedGameObject(btn.gameObject);
            if(btn.GetComponent<Image>().color != btn.colors.selectedColor)
            {
                btn.GetComponent<Image>().color = btn.colors.selectedColor;
                btn.gameObject.AddComponent<DeselectColorReset>();
            }

        }
    }
}
