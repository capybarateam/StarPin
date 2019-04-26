using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PartsPalette", menuName = "ステージ作成/パーツパレット", order = 201)]
public class PrefabPalette : ScriptableObject
{
    public string title;

    public GameObject[] prefabs;

    [HideInInspector]
    public string prevFolder;
}