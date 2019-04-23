using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointModelController : MonoBehaviour
{
    public Material normalMaterial;
    public Material vibrantMaterial;

    public PointColor colorPalette;

    PointController point;

    bool _lightenabled;
    int _colorIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        point = GetComponentInParent<PointController>();
    }

    Color MixColor(Color hColor, Color svColor)
    {
        Color.RGBToHSV(hColor, out float h, out _, out _);
        Color.RGBToHSV(svColor, out _, out float s, out float v);
        return Color.HSVToRGB(h, s, v);
    }

    // Update is called once per frame
    void Update()
    {
        if (point.touched != _lightenabled || point.colorIndex != _colorIndex)
        {
            var render = GetComponentInChildren<Renderer>();

            render.material = point.touched ? vibrantMaterial : normalMaterial;

            if (point.colorIndex < colorPalette.colors.Count && colorPalette.colors[point.colorIndex] != null)
            {
                render.material.color = colorPalette.colors[point.colorIndex];
                render.material.SetColor("_EmissionColor", colorPalette.colors[point.colorIndex] * 4f);
            }

            render.UpdateGIMaterials();
            //DynamicGI.SetEmissive(render, (value ? vibrantMaterial : normalMaterial).GetColor("_EmissionColor"));

            _lightenabled = point.touched;
            _colorIndex = point.colorIndex;
        }
    }

    /*
    [ExecuteInEditMode]
    private void Awake()
    {
        var render = GetComponentInChildren<Renderer>();

        var mat = new Material(render.sharedMaterial);
        mat = point.touched ? vibrantMaterial : normalMaterial;

        if (point.colorIndex < colorPalette.colors.Count && colorPalette.colors[point.colorIndex] != null)
        {
            mat.color = colorPalette.colors[point.colorIndex];
            mat.SetColor("_EmissionColor", colorPalette.colors[point.colorIndex] * 4f);
        }

        render.sharedMaterial = mat;

        render.UpdateGIMaterials();
    }
    */
}
