using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public Material normalMaterial;
    public Material vibrantMaterial;

    bool _touched;
    public bool touched {
        get
        {
            return _touched;
        }

        set
        {
            var render = GetComponentInChildren<Renderer>();
            render.material = value ? vibrantMaterial : normalMaterial;
            render.UpdateGIMaterials();
            //DynamicGI.SetEmissive(render, (value ? vibrantMaterial : normalMaterial).GetColor("_EmissionColor"));
            _touched = value;
        }
    }

    void OnAttached()
    {
        touched = true;
    }
}
