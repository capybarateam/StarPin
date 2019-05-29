using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Targetter : MonoBehaviour
{
    public float speedRatio = 0.1f;
    public float range = 1f;
    public bool zLock = false;

    public bool changeIfWorldMap = false;

    public Vector3 offset = Vector3.forward * -10;
    public Vector3 offsetWorldMap = Vector3.forward * -10;

    public Vector3 rotation = Vector3.zero;
    public Vector3 rotationWorldMap = Vector3.zero;

    Quaternion rotationQuat;
    Quaternion rotationQuatWorldMap;

    void Start()
    {
        rotationQuat = transform.rotation * Quaternion.Euler(rotation);
        rotationQuatWorldMap = transform.rotation * Quaternion.Euler(rotationWorldMap);
    }

    bool IsWorldMap
    {
        get
        {
            if (!changeIfWorldMap)
                return false;
            return SceneSelector.GetCurrentSceneName().Contains("World");
        }
    }

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
        {
            var ppos = pos.Value + (IsWorldMap ? offsetWorldMap : offset);
            var prot = (IsWorldMap ? rotationQuatWorldMap : rotationQuat);
            var tpos = ppos;
            var trot = prot;
            transform.position = new Vector3(tpos.x, tpos.y, zLock ? transform.position.z : tpos.z);
            transform.rotation = trot;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var pos = GetTargetPosition();
        if (pos.HasValue)
        {
            var ppos = pos.Value + (IsWorldMap ? offsetWorldMap : offset);
            if (Vector2.Distance(transform.position, ppos) > range)
            {
                var prot = (IsWorldMap ? rotationQuatWorldMap : rotationQuat);
                var tpos = Vector3.Lerp(transform.position, ppos, speedRatio * 60 * Time.deltaTime);
                var trot = Quaternion.Lerp(transform.rotation, prot, speedRatio * 60 * Time.deltaTime);
                transform.position = new Vector3(tpos.x, tpos.y, zLock ? transform.position.z : tpos.z);
                transform.rotation = trot;
            }
        }
    }
}
