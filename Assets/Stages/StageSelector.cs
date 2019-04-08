using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelector : MonoBehaviour
{
    Stage currentStage;

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

    public bool LoadStage(Stage stage)
    {
        if (currentStage != stage)
        {
            SceneSelector.Get().LoadScene(stage.sceneName);
            currentStage = stage;

            BaseDirector.Get()?.StageChangeEffect(true);
            this.Delay(2, () =>
            {
                BaseDirector.Get()?.StageChangeEffect(false);
            });

            return true;
        }
        return false;
    }

    public void LoadNextStage()
    {
        if (currentStage && currentStage.nextStage)
            LoadStage(currentStage.nextStage);
        else
            SceneSelector.Get().LoadScene("SelectScene");
    }

    public static StageSelector Get()
    {
        return BaseDirector.Get().GetComponent<StageSelector>();
    }
}
