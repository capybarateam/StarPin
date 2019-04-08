using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageDisplay : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    public GameObject stageDisplay;
    public GameObject stageTitle;
    public GameObject board;

    public Stage stage;

    public Material normalMaterial;
    public Material focusMaterial;

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
