using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorJoint : MonoBehaviour, IAttachable
{
    public ConnectorBase controller;

    public void CheckAttachable(StarController star, ref bool cancel)
    {
    }

    public void OnAttached(StarController star)
    {
        controller.HandleAttached(star, gameObject);
    }
}
