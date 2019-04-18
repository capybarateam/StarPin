using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ExportStage : ScriptableWizard
{
    [Header("<名前>.<s番号>")]
    public string stageName = "<名前>.<s番号>";

    [MenuItem("ステージ作成/ステージをエクスポート")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<ExportStage>("ステージをエクスポート", "エクスポート");
    }

    void OnWizardCreate()
    {
        var guids = AssetDatabase.FindAssets("", new string[]{
            "Assets/Stages/" + stageName
        });

        var assets = new string[guids.Length];
        for (int i = 0; i < guids.Length; ++i)
            assets[i] = AssetDatabase.GUIDToAssetPath(guids[i]);

        var file = EditorUtility.SaveFilePanel("ステージをエクスポート", "Assets/../..", stageName, "unitypackage");
        if (!string.IsNullOrEmpty(file))
            AssetDatabase.ExportPackage(assets, file, ExportPackageOptions.Recurse);
    }

    void OnWizardUpdate()
    {
        helpString = "エクスポートしたいステージ名を入れてエクスポートボタンを押してください。";
    }
}
