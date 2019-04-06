using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlurShaderProperty : MonoBehaviour
{
    public float radius;

    Graphic g;

    private void Start()
    {
        g = GetComponent<Image>();

        g.material = new Material(g.material);
    }

    private void Update()
    {
        g.material.SetFloat("_Radius", radius);
    }
}
