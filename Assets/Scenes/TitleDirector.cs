using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            OnClick();
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
