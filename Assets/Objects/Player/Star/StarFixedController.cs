using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFixedController : MonoBehaviour
{
    StarController star;
    Transform starFixed;
    Transform starFixedEnergy;

    // Start is called before the first frame update
    void Start()
    {
        star = GetComponent<StarController>();
        starFixed = transform.parent.Find("StarFixed");
        starFixedEnergy = starFixed.transform.Find("Energy");
    }

    // Update is called once per frame
    void Update()
    {
        starFixed.position = transform.position;
        var scale = starFixedEnergy.localScale;
        scale.y = 1 - star.hp;
        starFixedEnergy.localScale = scale;
    }
}
