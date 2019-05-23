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

    [OptionsList("")]
    public StageType stageType;

    public GameObject[] prefabs;

    [HideInInspector]
    public string prevFolder;
}