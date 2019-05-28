using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void OnStartGame()
    {
        TitleDirector.Get().StartGameCustom();
    }

    public void OnSelectStage()
    {
        TitleDirector.Get().SelectStage();
    }
}
