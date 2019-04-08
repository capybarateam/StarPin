using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorGenerator : MonoBehaviour
{
    public GameObject stageDisplayPrefab;
    public Stage rootStage;

    public float span;
    public List<Stage> stages = new List<Stage>();
}
