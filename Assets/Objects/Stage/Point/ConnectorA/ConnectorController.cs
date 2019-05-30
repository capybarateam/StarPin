using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConnectorController : ConnectorBase
{
    public float depth;

    public override void HandleAttached(StarController star, GameObject from)
    {
        if (connectionA && connectionB)
            ConnectTo(connectionA, connectionB);
    }

    public void ConnectTo(GameObject connectionA, GameObject connectionB)
    {
        var lineRenderer = GetComponent<LineRenderer>();
        if (connectionA.GetComponent<PointController>().touched && connectionB.GetComponent<PointController>().touched)
        {
            var posA = connectionA.transform.position - transform.position;
            var posB = connectionB.transform.position - transform.position;

            posA.z = posB.z = depth;

            lineRenderer.SetPosition(0, posA);
            lineRenderer.SetPosition(1, posB);
        }
        else
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
