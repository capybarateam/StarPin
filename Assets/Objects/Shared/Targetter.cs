using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;

    StarController target;
    public GameObject Target { get; private set; }

    public void SetTarget(GameObject obj)
    {
        if (Target != obj)
        {
            bool noTarget = Target == null;
            Target = obj;
            target = obj?.GetComponent<StarController>() ?? null;
            if (noTarget)
                MoveImmediately();
        }
    }

    public Vector3? GetTargetPosition()
    {
        if (target)
            return target.currentJoint != null ? target.currentJoint.transform.position : target.transform.position;
        else if (Target)
            return Target.transform.position;
        else
            return null;
    }

    public void MoveImmediately()
    {
        var pos = GetTargetPosition();
        if (pos.HasValue)
            transform.position = new Vector3(pos.Value.x, pos.Value.y, transform.position.z);
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = GetTargetPosition();
        if (pos.HasValue)
        {
            if (Vector2.Distance(transform.position, pos.Value) > range)
            {
                Vector2 tpos = Vector2.Lerp(transform.position, pos.Value, speedRatio * 60 * Time.deltaTime);
                transform.position = new Vector3(tpos.x, tpos.y, transform.position.z);
            }
        }
    }
}
