using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using TMPro;

[CustomEditor(typeof(SelectorGenerator))]
public class SelectorGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SelectorGenerator gen = (SelectorGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            Undo.RegisterCompleteObjectUndo(gen.gameObject, "Generate Stage List");

            gen.stages.Clear();

            {
                var tempArray = new GameObject[gen.transform.childCount];
                for (int i = 0; i < tempArray.Length; i++)
                    tempArray[i] = gen.transform.GetChild(i).gameObject;
                foreach (var child in tempArray)
                    DestroyImmediate(child);
            }

            GenerateStageMap(gen, gen.rootStage);

            Undo.FlushUndoRecordObjects();
        }
    }

    void GenerateStageMap(SelectorGenerator gen, Stage stage)
    {
        if (stage)
        {
            GenerateStageDisplay(gen, stage);
            GenerateStageMap(gen, stage.nextStage);
            foreach (var rstage in stage.rewardStages)
            {
                if (!gen.stages.Contains(rstage))
                    GenerateStageMap(gen, rstage);
            }
        }
    }

    void GenerateStageDisplay(SelectorGenerator gen, Stage stage)
    {
        var obj = PrefabUtility.InstantiatePrefab(gen.stageDisplayPrefab, gen.transform) as GameObject;

        var pos = gen.transform.position;
        var count = gen.stages.Count;
        pos.y -= count * gen.span;
        obj.name = $"Stage {stage.stageName} ({count + 1})";
        obj.transform.position = pos;

        var ctrl = obj.GetComponent<StageDisplay>();
        ctrl.stage = stage;
        ctrl.stageTitle.GetComponent<TMP_Text>().text = stage.stageName;
        var g = ctrl.stageDisplay.GetComponent<Renderer>();
        var material = new Material(g.sharedMaterial);
        if (stage.thumbnail != null)
        {
            material.SetTexture("_BaseColorMap", stage.thumbnail);
            material.SetTexture("_EmissiveColorMap", stage.thumbnail);
            material.SetTexture("_MainTex", stage.thumbnail);
            material.SetTexture("_EmissionMap", stage.thumbnail);
        }
        g.sharedMaterial = material;
        g.UpdateGIMaterials();
        g.sharedMaterial.SetColor("_BaseColor", Color.white);
        g.sharedMaterial.SetColor("_EmissiveColor", Color.gray);
        g.sharedMaterial.SetColor("_Color", Color.white);
        g.sharedMaterial.SetColor("_EmissionColor", Color.gray);

        gen.stages.Add(stage);
    }
}
