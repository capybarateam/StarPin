using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PrefabPalette", menuName = "Prefab Palette", order = 201)]
public class PrefabPalette : ScriptableObject
{
    public string title;

    public GameObject[] prefabs;

    [HideInInspector]
    public string prevFolder;
}