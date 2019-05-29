using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    public IStage lastWorldMap;
    public Stage selectScene;

    public void LoadStage(Stage stage, SceneSelector.SceneChangeType changeType = SceneSelector.SceneChangeType.CHANGE_FADE)
    {
        SceneSelector.Get().LoadScene(stage, changeType);
    }

    public void LoadNextStage(SceneSelector.SceneChangeType changeType = SceneSelector.SceneChangeType.CHANGE_FADE)
    {
        SceneSelector.Get().LoadScene(lastWorldMap != null ? lastWorldMap : selectScene, changeType);
    }

    public static StageSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<StageSelector>();
    }
}
