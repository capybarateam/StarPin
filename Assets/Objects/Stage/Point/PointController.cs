using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour, IAttachable
{
    public bool important = false;
    public bool grabbable = true;

    public enum TransferType
    {
        SendOnly,
        RecieveOnly,
        ToggleSendRecieve,
        SendIfEmpty,
        RecieveIfFull,
    }
    public TransferType transferType = TransferType.SendOnly;

    public enum TransferVolume
    {
        DontTransferUntilPossible,
        TransferAsMuchAsPossible,
        TransferOne,
    }
    public TransferVolume transferVolume = TransferVolume.DontTransferUntilPossible;

    public enum GrabbableCondition
    {
        AlwaysGrabbable,
        GrabbableWhenTransfer,
        GrabbableIfSendOrFull,
        GrabbableIfRecieveOrEmpty,
    }
    public GrabbableCondition grabbableCondition = GrabbableCondition.AlwaysGrabbable;

    public int maxPoint = 1;
    public int currentPoint = 0;
    public bool isSendMode = true;

    public enum ColorTransferType
    {
        None,
        SendColor,
        RecieveColor,
    }
    public ColorTransferType colorTransferType = ColorTransferType.None;

    public enum ColorCondition
    {
        Free,
        SameColor,
        NonSameColor,
    }
    public ColorCondition colorCondition = ColorCondition.Free;

    public int colorIndex;

    public bool touched
    {
        get
        {
            return currentPoint > 0;
        }

        set
        {
            if (!value)
                currentPoint = 0;
            else
                currentPoint = Mathf.Max(1, currentPoint);
        }
    }

    bool CheckCondition(out bool isSend)
    {
        isSend = false;

        if (!grabbable)
            return false;

        var manager = GameDirector.Get(transform)?.pointManager;
        if (manager != null)
        {
            if (transferType == TransferType.SendOnly ||
                (transferType == TransferType.ToggleSendRecieve && isSendMode) ||
                (transferType == TransferType.SendIfEmpty && currentPoint <= 0) ||
                (transferType == TransferType.RecieveIfFull && currentPoint < maxPoint))
            {
                isSend = true;
                return ((transferVolume == TransferVolume.DontTransferUntilPossible && manager.health >= maxPoint - currentPoint && maxPoint - currentPoint > 0) ||
                    (transferVolume == TransferVolume.TransferAsMuchAsPossible && manager.health > 0 && currentPoint < maxPoint) ||
                    (transferVolume == TransferVolume.TransferOne && manager.health > 0 && maxPoint - currentPoint > 0));
            }
            if (transferType == TransferType.RecieveOnly ||
                (transferType == TransferType.ToggleSendRecieve && !isSendMode) ||
                (transferType == TransferType.SendIfEmpty && currentPoint > 0) ||
                (transferType == TransferType.RecieveIfFull && currentPoint >= maxPoint))
            {
                isSend = false;
                return ((transferVolume == TransferVolume.DontTransferUntilPossible && currentPoint <= manager.maxHealth - manager.health && currentPoint > 0) ||
                    (transferVolume == TransferVolume.TransferAsMuchAsPossible && manager.health < manager.maxHealth && currentPoint > 0) ||
                    (transferVolume == TransferVolume.TransferOne && manager.maxHealth - manager.health > 0 && currentPoint > 0));
            }
        }

        return false;
    }

    public void CheckAttachable(StarController star, ref bool cancel)
    {
        if (!grabbable)
        {
            cancel = true;
            return;
        }

        var manager = GameDirector.Get(transform)?.pointManager;
        if (manager != null)
        {
            if ((colorCondition == ColorCondition.SameColor && colorIndex != manager.colorIndex) ||
                (colorCondition == ColorCondition.NonSameColor && colorIndex == manager.colorIndex))
            {
                cancel = true;
                return;
            }
        }

            if (grabbableCondition != GrabbableCondition.AlwaysGrabbable)
        {
            bool isSend;
            bool condition = CheckCondition(out isSend);

            switch (grabbableCondition)
            {
                case GrabbableCondition.GrabbableWhenTransfer:
                    if (!condition)
                        cancel = true;
                    break;
                case GrabbableCondition.GrabbableIfSendOrFull:
                    if (!((condition && isSend) || currentPoint >= maxPoint))
                        cancel = true;
                    break;
                case GrabbableCondition.GrabbableIfRecieveOrEmpty:
                    if (!((condition && !isSend) || currentPoint <= 0))
                        cancel = true;
                    break;
            }
        }
    }

    public void OnAttached(StarController star)
    {
        if (!grabbable)
            return;

        var manager = GameDirector.Get(transform)?.pointManager;
        if (manager != null)
        {
            if (colorTransferType == ColorTransferType.SendColor)
                colorIndex = manager.colorIndex;
            if (colorTransferType == ColorTransferType.RecieveColor)
                manager.colorIndex = colorIndex;

            bool isSend;
            if (CheckCondition(out isSend))
            {
                int transfer = 0;
                if (isSend)
                {
                    switch (transferVolume)
                    {
                        case TransferVolume.DontTransferUntilPossible:
                            transfer = -(maxPoint - currentPoint);
                            break;
                        case TransferVolume.TransferAsMuchAsPossible:
                            transfer = -Mathf.Min(maxPoint - currentPoint, manager.health);
                            break;
                        case TransferVolume.TransferOne:
                            transfer = -1;
                            break;
                    }
                }
                else
                {
                    switch (transferVolume)
                    {
                        case TransferVolume.DontTransferUntilPossible:
                            transfer = currentPoint;
                            break;
                        case TransferVolume.TransferAsMuchAsPossible:
                            transfer = Mathf.Min(currentPoint, manager.maxHealth - manager.health);
                            break;
                        case TransferVolume.TransferOne:
                            transfer = 1;
                            break;
                    }
                }
                currentPoint -= transfer;
                manager.health += transfer;
                touched = currentPoint > 0;
                isSendMode = !isSendMode;
            }
        }
    }

    void Start()
    {
        GameDirector.Get(transform)?.pointManager.RegisterPoint(this, important);
    }
}
