using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageDisplay : MonoBehaviour, ISelectHandler
{
    public GameObject stageDisplay;
    public GameObject board;

    public Stage stage;

    public Material normalMaterial;
    public Material focusMaterial;

    public void OnSelect(BaseEventData eventData)
    {
    }
}
