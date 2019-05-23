using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePointController : MonoBehaviour
{
    public int defaultClearLevel;
    int? _clearLevel;

    public GameObject backgroundStarCleared;

    public int clearLevel
    {
        get
        {
            return _clearLevel.GetValueOrDefault(3);
        }
        set
        {
            bool changed = !_clearLevel.HasValue || _clearLevel.Value != value;
            _clearLevel = value;
            if (changed)
                SetCleared(value);
        }
    }

    void SetCleared(int clearLevel)
    {
        var selectable = GetComponentInChildren<StageSelectable>();
        if (clearLevel > 0)
            selectable.interactable = true;
        SetLineCleared(selectable, clearLevel);
        foreach (var star in GetComponentsInChildren<LevelStar>())
            star.SetLevel(clearLevel);
        backgroundStarCleared.SetActive(clearLevel > 0);
    }

    void SetLineCleared(StageSelectable selectable, int clearLevel)
    {
        foreach (var line in selectable.GetComponentsInChildren<ConnectorBase>())
        {
            line.GetComponent<LineRenderer>().enabled = clearLevel > 0;
            if (line.connectionB != null && clearLevel > 0)
            {
                var linesel = line.connectionB.GetComponentInParent<StageSelectable>();
                if (linesel != null)
                {
                    linesel.interactable = true;
                    if (linesel.stage == null)
                        SetLineCleared(linesel, clearLevel);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var stageSelectable = GetComponentInChildren<StageSelectable>();
        if (stageSelectable != null && stageSelectable.stage != null)
            clearLevel = StageAchievement.GetCleared(stageSelectable.stage, defaultClearLevel);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
