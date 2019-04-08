using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLightController : MonoBehaviour
{
    Targetter lightTarget;

    // Start is called before the first frame update
    void Start()
    {
        lightTarget = GetComponent<Targetter>();
    }

    // Update is called once per frame
    void Update()
    {
        lightTarget.SetTarget(CameraController.Get().Targetter.Target);
    }
}
