using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class StarModelController : MonoBehaviour
{
    StarController star;

    public List<MeshRenderer> meshes;
    List<KeyValuePair<MeshRenderer, Material>> meshMaterials;

    public PointColor colorPalette;
    public List<MeshRenderer> colorMeshes;
    List<KeyValuePair<MeshRenderer, Material>> colorMeshMaterials;

    // Start is called before the first frame update
    void Start()
    {
        star = GetComponentInParent<StarController>();
        meshMaterials = meshes.Select(e => new KeyValuePair<MeshRenderer, Material>(e, e.sharedMaterial)).ToList();
        colorMeshMaterials = colorMeshes.Select(e => new KeyValuePair<MeshRenderer, Material>(e, e.sharedMaterial)).ToList();
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
            //color.a *= easeOutCubic(star.hp);
            mat.color = color;

            mat.SetColor("_EmissionColor", smat.GetColor("_EmissionColor") * star.hp);
        }

        foreach (var meshMaterial in colorMeshMaterials)
        {
            var mesh = meshMaterial.Key;
            var mats = mesh.materials;
            foreach (var mat in mats)
            {
                var smat = meshMaterial.Value;

                if (star.colorIndex < colorPalette.colors.Count && colorPalette.colors[star.colorIndex] != null)
                {
                    mat.color = colorPalette.colors[star.colorIndex];
                    mat.SetColor("_EmissionColor", colorPalette.colors[star.colorIndex] * 4f);
                }
            }
        }
    }
}
