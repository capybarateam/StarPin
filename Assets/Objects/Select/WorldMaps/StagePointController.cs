using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePointController : MonoBehaviour
{
    public int defaultClearLevels;
    int? _clearLevel;

    public GameObject[] visibleWhenCleared;
    public GameObject[] invisibleWhenCleared;
    public GameObject[] stageNameWhenClearedA;
    public GameObject[] stageNameWhenClearedB;

    public HashSet<StagePointController> connection;

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
        foreach (var obj in visibleWhenCleared)
            obj.SetActive(clearLevel >= 2);
        foreach (var obj in invisibleWhenCleared)
            obj.SetActive(clearLevel <= 0);
        foreach (var obj in stageNameWhenClearedA)
            obj.GetComponent<TMPro.TMP_Text>().text = clearLevel >= 2 ? (GetComponentInChildren<StageSelectable>()?.stage?.answer ?? "") : "???座";
        foreach (var obj in stageNameWhenClearedB)
            obj.GetComponent<TMPro.TMP_Text>().text = clearLevel >= 2 ? (GetComponentInChildren<StageSelectable>()?.stage?.answer ?? "") : "";
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

    void Handshake()
    {
        foreach (var line in GetComponentsInChildren<ConnectorBase>())
        {
            if (line.connectionB != null)
            {
                var linesel = line.connectionB.GetComponentInParent<StageSelectable>();
                if (linesel != null)
                {
                    if (linesel.interactable && linesel.stage != null)
                    {
                        var ptctrl = linesel.GetComponentInParent<StagePointController>();
                        if (ptctrl != null)
                        {
                            connection.Add(ptctrl);
                            ptctrl.connection.Add(this);
                        }
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var stageSelectable = GetComponentInChildren<StageSelectable>();
        if (stageSelectable != null)
        {
            Handshake();

            if (stageSelectable.stage != null)
                clearLevel = StageAchievement.GetCleared(stageSelectable.stage, defaultClearLevels);
            else if (stageSelectable.GetComponentInParent<StagePointController>()?.defaultClearLevels > 0)
                SetLineCleared(stageSelectable, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
