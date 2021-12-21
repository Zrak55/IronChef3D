using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeccreammancerPhylactery : MonoBehaviour
{
    EnemyHitpoints myHP;
    public Slider timer;
    void Start()
    {
        myHP = GetComponent<EnemyHitpoints>();
        myHP.DeathEvents += OnDeath;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.value > 0)
        {
            timer.value = Mathf.Max(timer.value - Time.deltaTime, 0);
        }    
    }

    public void OnDeath()
    {
        var nec = FindObjectOfType<NeccreammancerBehavior>().GetComponent<EnemyHitpoints>();
        nec.transform.position = transform.position;
        nec.Die();
    }

    public void ShowTimer(float t)
    {
        timer.maxValue = t;
        timer.value = t;
    }
}
