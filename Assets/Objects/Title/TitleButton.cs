using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void OnStartStage()
    {
        TitleDirector.Get().StartGame();
    }
    

    public void OnSelectStage()
    {
        TitleDirector.Get().SelectStage();
    }
}
