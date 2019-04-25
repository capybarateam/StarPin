﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
  fileName = "Stage",
  menuName = "ステージ作成/ステージマニフェスト",
  order = 100)
]
[Serializable]
public class Stage : ScriptableObject
{
    public string stageName;
    public Stage nextStage;
    public string sceneName;
    public Stage[] rewardStages;
    public Texture thumbnail;
    [TextArea(10, 30)]
    public string description;
    public string answer;
}