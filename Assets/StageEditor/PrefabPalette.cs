using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartsPalette", menuName = "ステージ作成/パーツパレット", order = 201)]
public class PrefabPalette : ScriptableObject
{
    public string title;

    public enum StageType
    {
        [EnumElement("通常のステージ")]
        NORMAL_STAGE,
        [EnumElement("ワールドマップ")]
        WORLD_MAP,
    }

    [EnumElementUsage(typeof(StageType), "ステージの種類")]
    public StageType stageType = StageType.NORMAL_STAGE;

    public GameObject[] prefabs;

    [HideInInspector]
    public string prevFolder;
}