using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SelectDirector : MonoBehaviour, ISelectDirector
{
    public Stage selected;
    public GameObject selectedObj;

    public GameObject paper;

    // Start is called before the first frame update
    void Start()
    {
        SelectEffect(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Stage GetSelected()
    {
        return selected;
    }

    public void StartGame(Stage stage)
    {
        if (StageSelector.Get().LoadStage(stage))
            SelectEffect(false);
    }

    public void BackToTitle()
    {
        if (SceneSelector.Get().LoadScene("TitleScene"))
            SelectEffect(false);
    }

    void SelectEffect(bool starting)
    {
        GetComponentInChildren<ButtonManager>().SetVisible(starting);
    }

    public void SetSelected(Stage stage)
    {
        if (stage)
            ShowPaper(stage.description);
    }

    public void SetSelected(GameObject stage)
    {
        CameraController.Get()?.Targetter?.SetTarget(stage);
        foreach (var obj in GetComponentsInChildren<Targetter>())
            obj.SetTarget(stage);
        selectedObj = stage;
    }

    public bool IsSelected(GameObject stage)
    {
        return selectedObj == stage;
    }

    void ShowPaper(string text)
    {
        if (!paper)
            return;
        var papertext = paper.GetComponentInChildren<TMP_Text>();
        var b = papertext.text != "" && text != papertext.text;
        if (b)
            paper.GetComponent<Animator>().SetBool("Enabled", false);
        this.Delay(.5f, () =>
        {
            papertext.text = text;
            if (b)
                paper.GetComponent<Animator>().SetBool("Enabled", true);
        });
    }

    public static ISelectDirector Get()
    {
        return GameObject.Find("GameSelect")?.GetComponent<ISelectDirector>();
    }
}
