using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{
    public void OnStartGame()
    {
        var selected = SelectDirector.Get(transform).GetSelected();
        if (selected)
            SelectDirector.Get(transform).StartGame(selected);
    }

    public void OnBackToTitle()
    {
        SelectDirector.Get(transform).BackToTitle();
    }

    public void OnBackToSelect()
    {
        SelectDirector.Get(transform).BackToSelect();
    }
}
