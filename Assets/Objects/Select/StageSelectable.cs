using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectable : MonoBehaviour, ISelectHandler, ISubmitHandler, IConnectorPoint
{
    public static StageSelectable lastSelected;

    public Stage stage;
    public bool interactable;

    public void OnSelect(BaseEventData eventData)
    {
        if (stage != null)
            StageAchievement.SetLastStage(SceneSelector.Get().CurrentScene.SceneName, stage);
        SelectDirector.Get(transform).SetSelected(stage);
        SelectDirector.Get(transform).SetSelected(gameObject);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        if (stage != null)
            SelectDirector.Get(transform).StartGame(stage);
    }
}
