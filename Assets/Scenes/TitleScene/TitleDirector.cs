using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleDirector : MonoBehaviour
{
    public Stage firstStage;
    public Stage selectScene;

    public GameObject fixedPin;

    // Start is called before the first frame update
    void Start()
    {
        var sel = SceneSelector.Get();
        var sta = StageSelector.Get();
        if (sel != null && sta != null)
            sta.lastWorldMap = sel.CurrentScene;
        var music = MusicController.Get();
        if (music != null)
        {
            music.ChangeSound(music.TitleBGM);
            music.ApplyParamater("Scene", 0f / 2);
        }

        TitleEffect(true);

        CameraController.Get().Targetter.SetTarget(fixedPin);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartGameStory()
    {
        StageAchievement.isCreativeMode = false;
        if (!StageAchievement.IsCleared(firstStage, 0))
        {
            StageSelector.Get().LoadStage(firstStage);
            StageSelector.Get().lastWorldMap = new SceneStage("World1");
        }
        else
        {
            StageSelector.Get().LoadStage(selectScene, SceneSelector.SceneChangeType.CHANGE_MOVE);
        }
        TitleEffect(false);
    }

    public void StartGameCustom()
    {
        StageAchievement.isCreativeMode = true;
        StageSelector.Get().LoadStage(selectScene, SceneSelector.SceneChangeType.CHANGE_MOVE);
        TitleEffect(false);
    }

    public void SelectStage()
    {
        SceneSelector.Get().LoadScene(selectScene, SceneSelector.SceneChangeType.CHANGE_MOVE);
        TitleEffect(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
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
