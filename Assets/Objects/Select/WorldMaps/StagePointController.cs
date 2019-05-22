using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePointController : MonoBehaviour
{
    public bool defaultCleared;
    bool _cleared;

    public GameObject backgroundStarCleared;

    public bool cleared {
        get
        {
            return _cleared;
        }
        set
        {
            bool changed = _cleared != value;
            _cleared = value;
            if (changed)
                SetCleared(value);
        }
    }

    void SetCleared(bool cleared)
    {
        var lines = GetComponentsInChildren<ConnectorBase>();
        foreach (var line in lines)
            line.GetComponent<LineRenderer>().enabled = cleared;
        var selectables = GetComponentsInChildren<Selectable>();
        foreach (var selectable in selectables)
            selectable.interactable = cleared;
        backgroundStarCleared.SetActive(cleared);
    }

    // Start is called before the first frame update
    void Start()
    {
        var stageSelectable = GetComponentInChildren<StageSelectable>();
        if (stageSelectable != null && stageSelectable.stage != null)
            cleared = StageAchievement.GetCleared(stageSelectable.stage, defaultCleared);
        else
            cleared = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
