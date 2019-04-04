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
        var star = GameObject.Find("StarObjectTitle");
        CameraController.Get().SetTargetImmediately(star);
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
        SceneManager.UnloadSceneAsync("TitleScene");
    }
}
