using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IAttachable : IEventSystemHandler
{
    void CheckAttachable(StarController star, ref bool cancel);

    void OnAttached(StarController star);
}