using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : ConnectorBase
{
    // 引っ付いたときの処理
    public override void HandleAttached(StarController star, GameObject from)
    {
        if (from == connectionA)
            star.AttachToJoint(connectionB);
    }
}
