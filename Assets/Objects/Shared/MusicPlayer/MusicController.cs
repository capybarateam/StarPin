using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class MusicController : MonoBehaviour
{
    public StudioEventEmitter TitleBGM;
    public StudioEventEmitter PG1;
    public StudioEventEmitter PG2;
    public StudioEventEmitter PG3;
    public StudioEventEmitter PG4;

    StudioEventEmitter current;

    void Start()
    {
        current = TitleBGM;
    }

    public void ChangeSound(StudioEventEmitter emit)
    {
        if (current != emit)
        {
            if (current != null)
                current.Stop();
            current = emit;
            current.Play();
        }
    }

    public void ApplyParamater(string paramName, float value)
    {
        if (current != null)
        {
            var instance = current.EventInstance;
            instance.getParameterByName(paramName, out float before);
            StartCoroutine(SmoothParamater(paramName, value, before));
        }
    }

    IEnumerator SmoothParamater(string paramName, float value, float before)
    {
        for (float i = 0f; i < 1f; i += .02f)
        {
            var instance = current.EventInstance;
            instance.setParameterByName(paramName, Mathf.Lerp(before, value, i));
            yield return null;
        }
    }

    public static MusicController Get()
    {
        return GameObject.Find("MusicPlayer")?.GetComponent<MusicController>();
    }
}
