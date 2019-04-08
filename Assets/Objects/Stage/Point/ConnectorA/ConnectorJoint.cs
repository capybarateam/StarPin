using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorJoint : MonoBehaviour
{
    public ConnectorController controller;

    void OnAttached()
    {
        controller.HandleAttached();
    }
}
