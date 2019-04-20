using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour
{
    public Material normalMaterial;
    public Material vibrantMaterial;

    public bool important;

    bool _touched;
    public bool touched
    {
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
        if (!touched)
        {
            var manager = GameDirector.Get(transform)?.pointManager;
            if (manager != null && manager.health > 0)
            {
                manager.health--;
                touched = true;
            }
            else if (manager == null)
            {
                touched = true;
            }
        }
    }

    private void Start()
    {
        GameDirector.Get(transform)?.pointManager.RegisterPoint(this, important);
    }
}
