using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSelectDirector : MonoBehaviour, ISelectDirector
{
    public Stage selected;

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
    }

    public void SetSelected(GameObject stage)
    {
        CameraController.Get().Targetter.SetTarget(gameObject);
        foreach (var obj in GetComponentsInChildren<Targetter>())
            obj.SetTarget(gameObject);
    }
}
