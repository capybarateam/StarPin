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

    public Stack<string> currentScene = new Stack<string>();
    public string CurrentScene
    {
        get
        {
            return currentScene.Peek();
        }
    }

    void Awake()
    {
        currentScene.Push(SceneManager.GetActiveScene().name);
        PushScene();
    }

    bool locked;

    public CanvasGroup fadeimage;

    IEnumerator enumerator;

    public bool LoadScene(string scene, SceneChangeType changeType = SceneChangeType.CHANGE_MOVE)
    {
        switch (changeType)
        {
            case SceneChangeType.CHANGE_MOVE:
                {
                    if (currentScene.Peek() != scene && !locked)
                    {
                        MoveUpdate(scene);
                        return true;
                    }
                }
                break;

            case SceneChangeType.CHANGE_FADE:
                {
                    if (currentScene.Peek() != scene && !locked)
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
        var cscene = currentScene.Peek();
        if (cscene != null)
        {
            locked = true;
            BaseDirector.Get()?.StageChangeEffect(true);
            this.Delay(2, () =>
            {
                BaseDirector.Get()?.StageChangeEffect(false);
            });
            this.Delay(duration, sceneName =>
            {
                SceneManager.UnloadSceneAsync(cscene).completed += e => locked = false;
            }, currentScene);
        }
        currentScene.Pop();
        currentScene.Push(scene);
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
        SceneManager.UnloadSceneAsync(currentScene.Peek());
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        currentScene.Pop();
        currentScene.Push(scene);
        for (float alfa = 1f; alfa >= 0f; alfa -= Time.deltaTime / (duration / 2))
        {
            fadeimage.alpha = alfa;
            yield return null;
        }
        fadeimage.gameObject.SetActive(false);
        locked = false;
    }

    public void PushScene()
    {
        currentScene.Push(null);
    }

    public void PopScene()
    {
        var del = currentScene.Pop();
        if (del != null)
            SceneManager.UnloadSceneAsync(del);
    }

    public static SceneSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<SceneSelector>();
    }
}
