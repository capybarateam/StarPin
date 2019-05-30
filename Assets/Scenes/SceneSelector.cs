using System.Collections;
using System.Collections.Generic;
using System;
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

    float durationMoveEffect = 2;
    float durationMoveEnd = 3;
    float durationFadeStart = 1.5f;
    float durationFadeEnd = 1f;

    public Stack<IStage> currentScene = new Stack<IStage>();
    public IStage CurrentScene
    {
        get
        {
            return currentScene.Peek();
        }
    }

    public static string GetCurrentSceneName()
    {
        var selector = SceneSelector.Get();
        return selector != null ? selector.CurrentScene.SceneName : SceneManager.GetActiveScene().name;
    }

    void Awake()
    {
        currentScene.Push(new SceneStage(SceneManager.GetActiveScene().name));
    }

    readonly Queue<Func<bool>> queue = new Queue<Func<bool>>();

    bool locked;

    public CanvasGroup fadeimage;

    IEnumerator enumerator;

    void Update()
    {
        if (!locked && queue.Count > 0)
        {
            var task = queue.Peek();
            if (task != null && task())
                queue.Dequeue();
        }
    }

    public void LoadScene(IStage scene, SceneChangeType changeType = SceneChangeType.CHANGE_FADE)
    {
        if (scene == null)
            throw new Exception("Scene is Null");
        queue.Enqueue(() =>
        {
            switch (changeType)
            {
                case SceneChangeType.CHANGE_MOVE:
                    {
                        if (!locked)
                        {
                            MoveUpdate(scene);
                            return true;
                        }
                    }
                    break;

                case SceneChangeType.CHANGE_FADE:
                    {
                        if (!locked)
                        {
                            StartCoroutine(FadeUpdate(scene));
                            return true;
                        }
                    }
                    break;
            }
            return false;
        });
    }

    void MoveUpdate(IStage scene)
    {
        SceneManager.LoadSceneAsync(scene.SceneName, LoadSceneMode.Additive);
        var cscene = currentScene.Peek();
        if (cscene != null)
        {
            locked = true;
            StageDirector.Get()?.StageChangeEffect(true, scene);
            this.Delay(durationMoveEffect, () =>
            {
                StageDirector.Get()?.StageChangeEffect(false, scene);
            });
            this.Delay(durationMoveEnd, sceneName =>
            {
                SceneManager.UnloadSceneAsync(cscene.SceneName).completed += e => locked = false;
            }, currentScene);
        }
        currentScene.Pop();
        currentScene.Push(scene);
    }

    IEnumerator FadeUpdate(IStage scene)
    {
        var current = currentScene.Peek();
        if (current != null)
        {
            fadeimage.gameObject.SetActive(true);
            for (float alfa = 0f; alfa < 1f; alfa += Time.deltaTime / durationFadeStart)
            {
                fadeimage.alpha = alfa;
                yield return null;
            }
        }
        locked = true;
        if (current != null)
            SceneManager.UnloadSceneAsync(current.SceneName);
        SceneManager.LoadScene(scene.SceneName, LoadSceneMode.Additive);
        currentScene.Pop();
        currentScene.Push(scene);
        locked = false;
        for (float alfa = 1f; alfa >= 0f; alfa -= Time.deltaTime / durationFadeEnd)
        {
            fadeimage.alpha = alfa;
            yield return null;
        }
        fadeimage.gameObject.SetActive(false);
    }

    public void PushScene()
    {
        queue.Enqueue(() =>
        {
            currentScene.Push(null);
            return true;
        });
    }

    public void PopScene()
    {
        queue.Enqueue(() =>
        {
            var del = currentScene.Pop();
            if (del != null)
                SceneManager.UnloadSceneAsync(del.SceneName);
            return true;
        });
    }

    public static SceneSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<SceneSelector>();
    }
}
