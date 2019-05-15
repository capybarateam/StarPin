using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageSelectable : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    public Stage stage;

    public void OnSelect(BaseEventData eventData)
    {
        CameraController.Get().Targetter.SetTarget(gameObject);
        SelectDirector.Get().selected = stage;
        if (stage)
            SelectDirector.Get().ShowPaper(stage.description);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        OnClick();
    }

    public void OnClick()
    {
        SelectDirector.Get().StartGame(stage);
    }
}
