using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class SelectCurrent : MonoBehaviour
{
    public StageSelectable current;
    GameObject lastSelectable;

    // Start is called before the first frame update
    void Start()
    {
        var sname = SceneSelector.GetCurrentSceneName();
        if (sname != null)
        {
            var laststage = StageAchievement.GetLastStageSceneName(sname);
            {
                if (laststage != null)
                {
                    foreach (Transform worldstage in transform)
                    {
                        var stageSelectable = worldstage.GetComponentInChildren<StageSelectable>();
                        var stage = stageSelectable?.stage;
                        if (stage != null)
                        {
                            if (laststage == stage.sceneName)
                            {
                                current = stageSelectable;
                                current?.GetComponent<Selectable>()?.Select();
                                return;
                            }
                        }
                    }
                }
                if (transform.childCount > 0)
                {
                    var stageSelectable = transform.GetChild(0).GetComponentInChildren<StageSelectable>();
                    current = stageSelectable;
                    current?.GetComponent<Selectable>()?.Select();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var selectedObject = EventSystem.current.currentSelectedGameObject;
        if (selectedObject?.GetComponentInParent<SelectCurrent>() != this)
            return;
        bool changed = lastSelectable != selectedObject;
        lastSelectable = selectedObject;

        if (changed)
        {
            var stageSelectable = selectedObject?.GetComponentInChildren<StageSelectable>();
            if (stageSelectable != null)
                if (stageSelectable.interactable)
                    current = stageSelectable;
                else
                    current?.GetComponent<Selectable>()?.Select();
        }
    }
}
