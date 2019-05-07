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

    // Start is called before the first frame update
    void Start()
    {
        if (connectionA && connectionB)
        {
            connectionA.AddComponent<ConnectorJoint>().controller = this;
            connectionB.AddComponent<ConnectorJoint>().controller = this;
        }
    }

    public virtual void HandleAttached(StarController star, GameObject from)
    {
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (connectionA != null && connectionB != null)
            Handles.DrawLine(connectionA.transform.position, connectionB.transform.position);
    }
#endif
}
