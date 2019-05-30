using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectable : MonoBehaviour, ISelectHandler, ISubmitHandler, IConnectorPoint
{
    public static StageSelectable lastSelected;

    public Stage stage;
    public bool interactable;
    public bool clickable = true;

    public void OnSelect(BaseEventData eventData)
    {
        var sname = SceneSelector.GetCurrentSceneName();
        if (stage != null && !(sname.Contains("World") && stage.SceneName.Contains("World")))
        {
            StageAchievement.SetLastStage(sname, stage);
        }
        SelectDirector.Get(transform).SetSelected(stage);
        SelectDirector.Get(transform).SetSelected(gameObject);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        if (stage != null && clickable)
            SelectDirector.Get(transform).StartGame(stage);
    }
}
