using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldSelectable : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    public Stage stage;

    public void OnSelect(BaseEventData eventData)
    {
        SelectDirector.Get().SetSelected(stage);
        SelectDirector.Get().SetSelected(gameObject);
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
