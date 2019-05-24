using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneSelector : MonoBehaviour
{
    public enum SceneChangeType
    {
        CHANGE_MOVE,
        CHANGE_FADE,
    }

    public float duration = 3;

    string currentScene;
    bool locked;

    public CanvasGroup fadeimage;

    IEnumerator enumerator;

    public bool LoadScene(string scene, SceneChangeType changeType = SceneChangeType.CHANGE_MOVE)
    {
        switch (changeType)
        {
            case SceneChangeType.CHANGE_MOVE:
                {
                    if (currentScene != scene && !locked)
                    {
                        MoveUpdate(scene);
                        return true;
                    }
                }
                break;

            case SceneChangeType.CHANGE_FADE:
                {
                    if (currentScene != scene && !locked)
                    {
                        StartCoroutine(FadeUpdate(scene));
                        return true;
                    }
                }
                break;
        }
        return false;
    }

    void MoveUpdate(string scene)
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
    }

    IEnumerator FadeUpdate(string scene)
    {
        locked = true;
        fadeimage.gameObject.SetActive(true);
        for (float alfa = 0f; alfa < 1f; alfa += Time.deltaTime / (duration / 2))
        {
            fadeimage.alpha = alfa;
            yield return null;
        }
        SceneManager.UnloadSceneAsync(currentScene);
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        currentScene = scene;
        for (float alfa = 1f; alfa >= 0f; alfa -= Time.deltaTime / (duration / 2))
        {
            fadeimage.alpha = alfa;
            yield return null;
        }
        fadeimage.gameObject.SetActive(false);
        locked = false;
    }

    public static SceneSelector Get()
    {
        return BaseDirector.Get().GetComponent<SceneSelector>();
    }
}
