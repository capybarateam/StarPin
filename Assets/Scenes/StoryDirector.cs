using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryDirector : MonoBehaviour
{
    public GameObject fixedPin;

    // Start is called before the first frame update
    void Start()
    {
        CameraController.Get().Targetter.SetTarget(fixedPin);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Click"))
            StartGame();
    }

    public void StartGame()
    {
        var sel = SceneSelector.Get();
        if (sel != null)
        {
            if (sel.CurrentScene is Stage)
            {
                var stage = (Stage)sel.CurrentScene;
                sel.LoadScene(stage.afterStory, SceneSelector.SceneChangeType.CHANGE_FADE);
            }
        }
        StoryEffect(false);
    }

    public void StoryEffect(bool starting)
    {
        GetComponentInChildren<ButtonManager>().SetVisible(starting);
    }


    public static StoryDirector Get()
    {
        return GameObject.Find("GameStory")?.GetComponent<StoryDirector>();
    }
}
