using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(SpawnTreesOnMaterial))]
public class CustomEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        SpawnTreesOnMaterial myScript = (SpawnTreesOnMaterial)target;
        if (GUILayout.Button("Spawn Trees"))
        {
            myScript.SpawnTrees();
        }
        if (GUILayout.Button("Clear Trees"))
        {
            myScript.ClearTrees();
        }
        if(GUILayout.Button("Change Leaf Colors"))
        {
            myScript.ChangeLeafColors();
        }
    }
}


[CustomEditor(typeof(LeafColorChanger))]
public class AnotherCustomEditorButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        LeafColorChanger myScript = (LeafColorChanger)target;
        if (GUILayout.Button("Change Leaf Color"))
        {
            myScript.ChangeColor();
        }
    }
}
#endif