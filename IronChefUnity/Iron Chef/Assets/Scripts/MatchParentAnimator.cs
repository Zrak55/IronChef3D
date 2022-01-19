using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchParentAnimator : MonoBehaviour
{
    public List<string> bools;
    public List<string> ints;
    public List<string> floats;

    Animator myAnim;
    Animator parentAnim;

    public bool randomizeAnimSpeedOnStart;
    public float minAnimSpeed;
    public float maxAnimSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        parentAnim = GetComponentInParent<Animator>();
        StartCoroutine(UpdateAnims());

        if(randomizeAnimSpeedOnStart)
        {
            myAnim.speed = Random.Range(minAnimSpeed, maxAnimSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator UpdateAnims()
    {
        foreach(var b in bools)
        {
            myAnim.SetBool(b, parentAnim.GetBool(b));
        }
        foreach(var i in ints)
        {

            myAnim.SetInteger(i, parentAnim.GetInteger(i));
        }
        foreach(var f in floats)
        {
            myAnim.SetFloat(f, parentAnim.GetFloat(f));
        }

        yield return new WaitForSeconds(Random.Range(0.05f, 0.25f));
    }
}
