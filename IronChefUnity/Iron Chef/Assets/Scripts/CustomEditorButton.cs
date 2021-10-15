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
    }
}
#endif