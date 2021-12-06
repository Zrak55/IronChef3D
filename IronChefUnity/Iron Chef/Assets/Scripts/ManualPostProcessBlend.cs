using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

[ExecuteInEditMode]
public class ManualPostProcessBlend : MonoBehaviour
{
    public Volume myVol;
    public SphereCollider myCol;
    Transform Player;

    float currentDistance;
    [Range(0, 1)]
    public float dist;

    public KeyValues red;
    public KeyValues blue;
    public KeyValues green;

    void Start()
    {
        Player = FindObjectOfType<CharacterMover>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Mathf.Clamp(Vector3.Distance(myCol.transform.position, Player.position) - myCol.radius, 0, myVol.blendDistance);
        dist = 1 - (currentDistance / myVol.blendDistance);

        myVol.sharedProfile.TryGet<ColorCurves>(out ColorCurves ccc);

        /*
        ccc.blue.Interp(bc1, bc2, dist);
        ccc.red.Interp(rc1, rc2, dist);
        ccc.green.Interp(gc1, gc2, dist);
        */
        //this snaps, the dist value is ceiled (0 at 0, 1 at >0)

        //This may work, but might have some issues with the tangentss
        Keyframe redKey = new Keyframe(1, Mathf.Lerp(red.baseValue, red.targetValue, dist), Mathf.Lerp(red.baseInTan, red.targetInTan, dist), Mathf.Lerp(red.baseOutTan, red.TargetOutTan, dist));
        ccc.red.value.MoveKey(1, redKey);

        Keyframe blueKey = new Keyframe(1, Mathf.Lerp(blue.baseValue, blue.targetValue, dist), Mathf.Lerp(blue.baseInTan, blue.targetInTan, dist), Mathf.Lerp(blue.baseOutTan, blue.TargetOutTan, dist));
        ccc.blue.value.MoveKey(1, blueKey);

        Keyframe greenKey = new Keyframe(1, Mathf.Lerp(green.baseValue, green.targetValue, dist), Mathf.Lerp(green.baseInTan, green.targetInTan, dist), Mathf.Lerp(green.baseOutTan, green.TargetOutTan, dist));
        ccc.green.value.MoveKey(1, greenKey);



        //TODO: Lerp between 2 curves
    }


    [System.Serializable]
    public struct KeyValues
    {
        public float baseValue;
        public float targetValue;
        public float baseInTan;
        public float targetInTan;
        public float baseOutTan;
        public float TargetOutTan;
    }
    
}
