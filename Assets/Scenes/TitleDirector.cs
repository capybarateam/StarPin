using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    public Stage firstStage;

    public GameObject startButton;
    public GameObject theTitle;

    // Start is called before the first frame update
    void Start()
    {
        TitleEffect(true);

        CameraController.Get().SetTarget(StarController.latestStar);
        CameraController.Get().MoveImmediately();

        this.Delay(.1f, () =>
        {
            startButton.GetComponentInChildren<Selectable>().Select();
        });
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClick()
    {
        TitleEffect(false);

        StageSelector.Get().LoadStage(firstStage);
        this.Delay(3, () =>
        {
            SceneManager.UnloadSceneAsync("TitleScene");
        });
    }

    public void TitleEffect(bool starting)
    {
        if (startButton)
            startButton.GetComponent<Animator>().SetBool("Enabled", starting);
        if (theTitle)
            theTitle.GetComponent<Animator>().SetBool("Enabled", starting);
    }

    public static TitleDirector Get()
    {
        return GameObject.Find("GameTitle")?.GetComponent<TitleDirector>();
    }
}
