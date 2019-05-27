using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectButton : MonoBehaviour
{

    public void OnStartGame()
    {
        SelectDirector.Get().StartGame();
    }

    public void OnBackToTitle()
    {
        SelectDirector.Get().BackToTitle();
    }
}
