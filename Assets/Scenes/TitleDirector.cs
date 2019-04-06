using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    public Stage firstStage;

    // Start is called before the first frame update
    void Start()
    {
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
        StageSelector.Get().LoadStage(firstStage);
        this.Delay(3, () =>
        {
            SceneManager.UnloadSceneAsync("TitleScene");
        });
    }

    public static TitleDirector Get()
    {
        return GameObject.Find("GameTitle")?.GetComponent<TitleDirector>();
    }
}
