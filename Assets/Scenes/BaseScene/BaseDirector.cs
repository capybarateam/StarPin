using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BaseDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sceneSelector = SceneSelector.Get();
        sceneSelector.PushScene();
        sceneSelector.LoadScene(new SceneStage("StageScene"));
        sceneSelector.PushScene();
        sceneSelector.LoadScene(new SceneStage("TitleScene"));
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static BaseDirector Get()
    {
        return GameObject.Find("GameBase")?.GetComponent<BaseDirector>();
    }
}
