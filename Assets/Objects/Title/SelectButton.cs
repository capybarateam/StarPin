using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    public void OnStartGame()
    {
        var selected = SelectDirector.Get().GetSelected();
        if (selected)
            SelectDirector.Get().StartGame(selected);
    }

    public void OnBackToTitle()
    {
        SelectDirector.Get().BackToTitle();
    }
}
