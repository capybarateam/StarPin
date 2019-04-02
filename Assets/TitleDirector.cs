using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleDirector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var cam = GameObject.Find("Main Camera");
        var star = GameObject.Find("StarObjectTitle");
        cam.GetComponent<CameraController>().SetTargetImmediately(star);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("TitleScene");
    }
}
