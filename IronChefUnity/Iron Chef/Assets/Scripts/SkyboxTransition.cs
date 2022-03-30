using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxTransition : MonoBehaviour
{
    public Material transition;
    public Material endResult;
    public float timeToTransition;
    public float originalRotation;
    public float targetRotation;
    public bool ICanSwap = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            SwapSkyboxes();
        }
    }

    public void SwapSkyboxes()
    {
        if(ICanSwap)
            StartCoroutine(swapping()); 
    }

    IEnumerator swapping()
    {
        Debug.Log("Swapping Skybox");

        foreach(var swapper in FindObjectsOfType<SkyboxTransition>())
        {
            swapper.ICanSwap = false;
        }    
        transition.SetFloat("_Blend", 0);
        RenderSettings.skybox = transition;
        float currentBlend = 0;
        float currentRotation = originalRotation;
        while(currentBlend != 1f)
        {
            currentBlend = Mathf.Min(currentBlend + (Time.deltaTime / timeToTransition), 1);
            currentRotation = Mathf.Lerp(originalRotation, targetRotation, currentBlend);
            transition.SetFloat("_Blend", currentBlend);
            transition.SetFloat("_Rotation", currentRotation);
            yield return null;
        }
        RenderSettings.skybox = endResult;


        foreach (var swapper in FindObjectsOfType<SkyboxTransition>())
        {
            if (swapper != this)
                swapper.ICanSwap = true;
        }
    }
}
