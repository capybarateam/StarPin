using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EasingHelper))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EasingHelper myScript = (EasingHelper)target;
        if (GUILayout.Button("Generate Curve"))
        {
            myScript.GenerateCurve();
        }
    }
}
