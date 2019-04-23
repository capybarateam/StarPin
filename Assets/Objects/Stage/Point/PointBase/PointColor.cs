using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(
  fileName = "PointColor",
  menuName = "ステージ作成/カラー設定",
  order = 100)
]
[Serializable]
public class PointColor : ScriptableObject
{
    public List<Color> colors;
}
