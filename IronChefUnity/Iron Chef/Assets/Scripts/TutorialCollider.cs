using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCollider : MonoBehaviour
{
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().speed = Random.Range(0.5f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<TutorialManager>().SetImage(sprite);
            Debug.Log("Fade In");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            FindObjectOfType<TutorialManager>().LeaveImage();
            Debug.Log("FadeOut");
        }
    }
}
