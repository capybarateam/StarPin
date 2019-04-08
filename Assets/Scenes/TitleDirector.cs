using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDirector : MonoBehaviour
{
    public Stage firstStage;

    // Start is called before the first frame update
    void Start()
    {
        TitleEffect(true);

        var targetter = CameraController.Get().Targetter;
        targetter.SetTarget(StarController.latestStar);
        targetter.MoveImmediately();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGame()
    {
        TitleEffect(false);

        StageSelector.Get().LoadStage(firstStage);
    }

    public void SelectStage()
    {
        TitleEffect(false);

        SceneSelector.Get().LoadScene("SelectScene");
    }

    public void TitleEffect(bool starting)
    {
        GetComponentInChildren<ButtonManager>().SetVisible(starting);
    }

    public static TitleDirector Get()
    {
        return GameObject.Find("GameTitle")?.GetComponent<TitleDirector>();
    }
}
