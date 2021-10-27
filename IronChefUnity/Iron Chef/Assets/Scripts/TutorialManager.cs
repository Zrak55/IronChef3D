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


    public Image image;

    IEnumerator fader = null;

    // Start is called before the first frame update
    void Start()
    {
        IronChefUtils.TurnOffCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetImage(Sprite i)
    {
        if (fader != null)
            StopCoroutine(fader);
        fader = fadeImageIn(i);
        StartCoroutine(fader);
    }
    public void LeaveImage()
    {
        if (fader != null)
            StopCoroutine(fader);
        fader = fadeImageOut();
        StartCoroutine(fader);
    }

    IEnumerator fadeImageIn(Sprite s)
    {
        if(image.sprite != s)
        {
            while (image.color.a > 0)
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp(image.color.a - (2*Time.deltaTime), 0, 1));
                yield return null;
            }
            image.sprite = null;
        }

        image.sprite = s;

        while (image.color.a < 1)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp(image.color.a + (2*Time.deltaTime), 0, 1));
            yield return null;
        }

    }

    IEnumerator fadeImageOut()
    {
        while(image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Clamp(image.color.a - (2*Time.deltaTime), 0, 1));
            yield return null;
        }
        image.sprite = null;
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
