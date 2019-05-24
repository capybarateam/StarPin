using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLightController : MonoBehaviour
{
    public GameObject limitParent;
    Targetter lightTarget;

    // Start is called before the first frame update
    void Start()
    {
        lightTarget = GetComponent<Targetter>();
    }

    // Update is called once per frame
    void Update()
    {
        var target = CameraController.Get().Targetter.Target;
        if (target && target?.transform.parent == limitParent?.transform)
            lightTarget.SetTarget(target);
    }
}
