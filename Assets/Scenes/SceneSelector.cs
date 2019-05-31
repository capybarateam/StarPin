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

    public class StackData
    {
        public IStage current;
        public IStage next;
        public bool locked;
        public SceneChangeType changeType;
    }

    public Stack<StackData> sdatas = new Stack<StackData>();
    public IStage CurrentScene
    {
        get
        {
            return sdatas.Peek().current;
        }
    }

    public static string GetCurrentSceneName()
    {
        var selector = SceneSelector.Get();
        return selector != null ? selector.CurrentScene.SceneName : SceneManager.GetActiveScene().name;
    }

    void Awake()
    {
        StackData data = new StackData();
        data.current = new SceneStage(SceneManager.GetActiveScene().name);
        sdatas.Push(data);
    }

    public CanvasGroup fadeimage;

    IEnumerator enumerator;

    void Update()
    {
        foreach (var sdata in sdatas)
        {
            if (sdata!=null && !sdata.locked && sdata.next != null)
            {
                switch (sdata.changeType)
                {
                    case SceneChangeType.CHANGE_MOVE:
                        {
                            MoveUpdate(sdata);
                        }
                        break;

                    case SceneChangeType.CHANGE_FADE:
                        {
                            StartCoroutine(FadeUpdate(sdata));
                        }
                        break;
                }
            }
        }
    }

    public void LoadScene(IStage scene, SceneChangeType changeType = SceneChangeType.CHANGE_FADE)
    {
        if (scene == null)
            throw new Exception("Scene is Null");
        StackData data = sdatas.Peek();
        data.next = scene;
        data.changeType = changeType;
    }

    void MoveUpdate(StackData sdata)
    {
        SceneManager.LoadSceneAsync(sdata.next.SceneName, LoadSceneMode.Additive);
        if (sdata.current != null)
        {
            sdata.locked = true;
            var nextScene = sdata.next;
            StageDirector.Get()?.StageChangeEffect(true, nextScene);
            this.Delay(durationMoveEffect, () =>
            {
                StageDirector.Get()?.StageChangeEffect(false, nextScene);
            });
            var currentScene = sdata.current;
            this.Delay(durationMoveEnd, sceneName =>
            {
                SceneManager.UnloadSceneAsync(currentScene.SceneName).completed += e => sdata.locked = false;
            }, sdatas);
            sdata.current = sdata.next;
            sdata.next = null;
        }
    }

    IEnumerator FadeUpdate(StackData sdata)
    {
        sdata.locked = true;
        if (sdata.current != null)
        {
            fadeimage.gameObject.SetActive(true);
            for (float alfa = 0f; alfa < 1f; alfa += Time.deltaTime / durationFadeStart)
            {
                fadeimage.alpha = alfa;
                yield return null;
            }
        }
        if (sdata.current != null)
            SceneManager.UnloadSceneAsync(sdata.current.SceneName);
        SceneManager.LoadScene(sdata.next.SceneName, LoadSceneMode.Additive);
        sdata.current = sdata.next;
        sdata.next = null;
        for (float alfa = 1f; alfa >= 0f; alfa -= Time.deltaTime / durationFadeEnd)
        {
            fadeimage.alpha = alfa;
            yield return null;
        }
        fadeimage.gameObject.SetActive(false);
        sdata.locked = false;
    }

    public void PushScene()
    {
        sdatas.Push(new StackData());
    }

    public void PopScene()
    {
        var del = sdatas.Pop();
        if (del != null)
            SceneManager.UnloadSceneAsync(del.current.SceneName);
    }

    public static SceneSelector Get()
    {
        return BaseDirector.Get()?.GetComponent<SceneSelector>();
    }
}
