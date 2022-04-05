using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineOscilator : MonoBehaviour
{
    public AnimationCurve oscilation;
    float currentOsc = 0;
    public float OscTime = 1;
    Outline outline;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
    }

    // Update is called once per frame
    void Update()
    {
        currentOsc += Time.deltaTime;
        if (currentOsc > OscTime)
            currentOsc -= OscTime;

        outline.OutlineWidth = oscilation.Evaluate(currentOsc / OscTime);
    }

}
