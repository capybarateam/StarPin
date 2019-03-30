using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public StarController target;
    public float speedRatio = 0.1f;
    public float range = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetTarget(GameObject obj)
    {
        target = obj.GetComponent<StarController>();
    }

    public void SetTargetImmediately(GameObject obj)
    {
        SetTarget(obj);
        Vector2 pos;
        if (target != null && target.currentJoint != null)
            pos = target.currentJoint.transform.position;
        else
            pos = obj.transform.position;
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null && target.currentJoint != null)
        {
            if (Vector2.Distance(transform.position, target.currentJoint.transform.position) > range)
            {
                var pos = Vector2.Lerp(transform.position, target.currentJoint.transform.position, speedRatio);
                transform.position = new Vector3(pos.x, pos.y, transform.position.z);
            }
        }
    }
}
