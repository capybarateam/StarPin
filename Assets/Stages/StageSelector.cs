using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public IStage lastWorldMap;

    public Stage selectScene;

    Stage currentStage;

    public Stage Current
    {
        get
        {
            return currentStage;
        }
    }

    public void LoadStage(Stage stage)
    {
        if (currentStage != stage)
        {
            SceneSelector.Get().LoadScene(stage);
            currentStage = stage;
        }
    }

    public void LoadNextStage()
    {
        //if (currentStage && currentStage.nextStage)
        //    LoadStage(currentStage.nextStage);
        //else
        SceneSelector.Get().LoadScene(lastWorldMap != null ? lastWorldMap : selectScene);
    }

    public static StageSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<StageSelector>();
    }
}
