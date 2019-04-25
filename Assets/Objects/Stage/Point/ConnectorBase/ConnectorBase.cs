using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class ConnectorBase : MonoBehaviour
{
    public GameObject connectionA;
    public GameObject connectionB;

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (connectionA != null && connectionB != null)
            Handles.DrawLine(connectionA.transform.position, connectionB.transform.position);
    }
#endif
}
