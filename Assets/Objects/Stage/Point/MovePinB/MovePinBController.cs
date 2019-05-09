using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePinBController : MonoBehaviour, IAttachable, IDetachable
{
    public Transform target;
    public float percentage = .1f;

    Vector3 posA;
    Vector3 posB;
    bool attached;

    public void CheckAttachable(StarController star, ref bool cancel)
    {
    }

    public void OnAttached(StarController star)
    {
        attached = true;
    }

    public void OnDetached(StarController star)
    {
        attached = false;
    }

    void Start()
    {
        posA = transform.position;
        posB = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var point = attached ? posB : posA;
        transform.position = Vector3.Lerp(transform.position, point, percentage);
    }
}
