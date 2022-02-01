using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashCamAnimatorEventer : MonoBehaviour
{
    [SerializeField]
    public Animator[] snakonSceneAnimators;
    [SerializeField]
    public Animator benedictAnimator;
    [SerializeField]
    public Animator cheeseAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySnakonScene()
    {
        foreach (var a in snakonSceneAnimators)
            a.Play("Anim");
    }

    public void PlayBenedictScene()
    {
        benedictAnimator.Play("Anim");
    }

    public void PlayCheeseScene()
    {
        cheeseAnimator.Play("Anim");
    }

   
}
