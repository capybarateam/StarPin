using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectCurrent : MonoBehaviour
{
    [SerializeField]
    StageSelectable current;
    GameObject lastSelectable;

    // Start is called before the first frame update
    void Start()
    {
        var laststage = StageAchievement.GetLastStageSceneName();
        foreach (Transform worldstage in transform)
        {
            var stageSelectable = worldstage.GetComponentInChildren<StageSelectable>();
            var stage = stageSelectable?.stage;
            if (stage != null)
            {
                if (laststage == null || laststage == stage.sceneName)
                {
                    current = stageSelectable;
                    current?.GetComponent<Selectable>()?.Select();
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var selectedObject = EventSystem.current.currentSelectedGameObject;
        bool changed = lastSelectable != selectedObject;
        lastSelectable = selectedObject;

        if (changed)
        {
            var stageSelectable = selectedObject.GetComponentInChildren<StageSelectable>();
            if (stageSelectable.interactable)
                current = stageSelectable;
            else
                current?.GetComponent<Selectable>()?.Select();
        }
    }
}
