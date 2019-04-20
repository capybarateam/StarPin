using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarModelController : MonoBehaviour
{
    StarController star;
    public List<MeshRenderer> meshes;
    List<KeyValuePair<MeshRenderer, Material>> meshMaterials;

    // Start is called before the first frame update
    void Start()
    {
        star = GetComponentInParent<StarController>();
        meshMaterials = meshes.Select(e => new KeyValuePair<MeshRenderer, Material>(e, e.sharedMaterial)).ToList();
    }

    float easeOutCubic(float t) { return (--t) * t * t + 1; }

    // Update is called once per frame
    void Update()
    {
        foreach (var meshMaterial in meshMaterials)
        {
            var mesh = meshMaterial.Key;
            var mat = mesh.material;
            var smat = meshMaterial.Value;

            var color = smat.color;
            color.a *= easeOutCubic(star.hp);
            mat.color = color;

            mat.SetColor("_EmissionColor", smat.GetColor("_EmissionColor") * star.hp);
        }
    }
}
