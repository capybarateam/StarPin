using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(StudioEventEmitter))]
public class MusicController : MonoBehaviour
{
    StudioEventEmitter eventEmitter;
    private void Awake()
    {
        eventEmitter = GetComponent<StudioEventEmitter>();
    }

    public void ApplyParamater()
    {
        var instance = eventEmitter.EventInstance;
        foreach (var p in eventEmitter.Params)
        {
            //instance.setParameterValue(p.Name, p.Value);
        }
    }

    public void ChangeParameter(string paramName, float value)
    {
        //eventEmitter.SetParameter(paramName, value);
    }
}
