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

    public void LoadStage(Stage stage, SceneSelector.SceneChangeType changeType = SceneSelector.SceneChangeType.CHANGE_FADE)
    {
        if (currentStage != stage)
        {
            SceneSelector.Get().LoadScene(stage, changeType);
            currentStage = stage;
        }
    }

    public void LoadNextStage(SceneSelector.SceneChangeType changeType = SceneSelector.SceneChangeType.CHANGE_FADE)
    {
        //if (currentStage && currentStage.nextStage)
        //    LoadStage(currentStage.nextStage);
        //else
        SceneSelector.Get().LoadScene(lastWorldMap != null ? lastWorldMap : selectScene, changeType);
    }

    public static StageSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<StageSelector>();
    }
}
