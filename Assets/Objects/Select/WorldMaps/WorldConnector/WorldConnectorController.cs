using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class WorldConnectorController : ConnectorBase
{
    void Start()
    {
        if (connectionA && connectionB)
            ConnectTo(connectionA, connectionB);
    }

    public void ConnectTo(GameObject connectionA, GameObject connectionB)
    {
        var lineRenderer = GetComponent<LineRenderer>();

        var posA = connectionA.transform.position - transform.position;
        var posB = connectionB.transform.position - transform.position;

        lineRenderer.SetPosition(0, posA);
        lineRenderer.SetPosition(1, posB);
    }
}
