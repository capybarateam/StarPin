using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public float duration = 3;

    string currentScene;
    bool locked;

    public bool LoadScene(string scene)
    {
        if (currentScene != scene && !locked)
        {
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            if (currentScene != null)
            {
                locked = true;
                this.Delay(duration, sceneName =>
                {
                    SceneManager.UnloadSceneAsync(sceneName).completed += e => locked = false;
                }, currentScene);
            }
            currentScene = scene;
            return true;
        }
        return false;
    }

    public static SceneSelector Get()
    {
        return BaseDirector.Get().GetComponent<SceneSelector>();
    }
}
