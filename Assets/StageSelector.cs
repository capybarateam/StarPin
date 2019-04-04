using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    Stage currentStage;

    public void LoadStage(Stage stage)
    {
        if (currentStage)
            SceneManager.UnloadSceneAsync(currentStage.sceneName);
        SceneManager.LoadSceneAsync(stage.sceneName, LoadSceneMode.Additive);
    }

    public void LoadNextStage()
    {
        if (currentStage && currentStage.nextStage)
            LoadStage(currentStage.nextStage);
    }

    public static StageSelector Get()
    {
        return GameObject.Find("StageSelector").GetComponent<StageSelector>();
    }
}
