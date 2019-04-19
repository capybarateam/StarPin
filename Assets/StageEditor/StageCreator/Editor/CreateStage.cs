using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class CreateStage : ScriptableWizard
{
    [Header("<名前>.<s番号>")]
    public string stageName = "<名前>.<s番号>";

    [Header("ステージ名")]
    public string stageTitle = "無名のステージ";

    [MenuItem("ステージ作成/ステージを新規作成")]
    static void CreateWizard()
    {
        ScriptableWizard.DisplayWizard<CreateStage>("ステージ作成", "作成");
    }

    void OnWizardCreate()
    {
        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            var dir = AssetDatabase.GUIDToAssetPath(AssetDatabase.CreateFolder("Assets/Stages", stageName));
            var scene = dir + "/" + stageName + ".unity";
            var stage = dir + "/" + stageName + ".asset";
            AssetDatabase.CopyAsset(@"Assets/StageEditor/Template/Template.unity", scene);
            AssetDatabase.CopyAsset(@"Assets/StageEditor/Template/Template.asset", stage);
            Stage stageobj = AssetDatabase.LoadAssetAtPath(stage, typeof(ScriptableObject)) as Stage;
            stageobj.sceneName = stageName;
            stageobj.stageName = stageTitle;
            EditorUtility.SetDirty(stageobj);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorSceneManager.OpenScene(scene, OpenSceneMode.Single);
        }
    }

    void OnWizardUpdate()
    {
        helpString = "ステージ名を入れて作成ボタンを押してください。";
    }
}