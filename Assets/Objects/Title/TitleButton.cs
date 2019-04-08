using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void OnStartGame()
    {
        TitleDirector.Get().StartGame();
    }

    public void OnSelectStage()
    {
        TitleDirector.Get().SelectStage();
    }
}
