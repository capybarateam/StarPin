using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    Stage currentStage;
    bool locked;

    public Stage Current
    {
        get
        {
            return currentStage;
        }
    }
    public Stage Next
    {
        get
        {
            if (!currentStage)
                return null;
            return currentStage.nextStage;
        }
    }

    public void LoadStage(Stage stage)
    {
        if (currentStage != stage && !locked)
        {
            SceneManager.LoadSceneAsync(stage.sceneName, LoadSceneMode.Additive);
            if (currentStage)
            {
                locked = true;
                this.Delay(3f, sceneName =>
                 {
                     SceneManager.UnloadSceneAsync(sceneName).completed += e => locked = false;
                 }, currentStage.sceneName);
            }
            currentStage = stage;
        }
    }

    public void LoadNextStage()
    {
        if (currentStage && currentStage.nextStage)
            LoadStage(currentStage.nextStage);
    }

    public static StageSelector Get()
    {
        return BaseDirector.Get().GetComponent<StageSelector>();
    }
}
