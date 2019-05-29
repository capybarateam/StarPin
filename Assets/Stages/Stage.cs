using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStage
{
    string SceneName
    {
        get;
    }
}

[CreateAssetMenu(
  fileName = "Stage",
  menuName = "ステージ作成/ステージマニフェスト",
  order = 100)
]
[Serializable]
public class Stage : ScriptableObject, IStage
{
    public string stageName;
    public Stage nextStage;
    public string sceneName;
    public Stage[] rewardStages;
    public Texture thumbnail;
    [TextArea(10, 30)]
    public string description;
    public string answer;
    public int bgmId = -1;

    public string SceneName
    {
        get
        {
            return sceneName;
        }
    }
}

public class SceneStage : IStage
{
    public SceneStage(string sceneName)
    {
        this.SceneName = sceneName;
    }

    public string SceneName { get; }
}