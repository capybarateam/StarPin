using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointController : MonoBehaviour, IAttachable, IConnectorPoint
{
    [Header("ピン設定")]
    [LabelOverride("星座の要素である")]
    public bool important = false;
    [LabelOverride("つかむことができる")]
    public bool grabbable = true;

    public enum TransferType
    {
        [EnumElement("吸収専用")]
        SendOnly,
        [EnumElement("放出専用")]
        RecieveOnly,
        [EnumElement("吸収・放出をトグル")]
        ToggleSendRecieve,
        [EnumElement("ピンが空の場合のみ吸収")]
        SendIfEmpty,
        [EnumElement("ピンが満杯の場合のみ放出")]
        RecieveIfFull,
    }
    [Header("エネルギー設定")]
    [EnumElementUsage(typeof(TransferType), "タイプ")]
    public TransferType transferType = TransferType.SendOnly;

    public enum TransferVolume
    {
        [EnumElement("すべて送れるようになるまで送らない")]
        DontTransferUntilPossible,
        [EnumElement("できる限り送る")]
        TransferAsMuchAsPossible,
        [EnumElement("一個ずつ送る")]
        TransferOne,
    }
    [EnumElementUsage(typeof(TransferVolume), "まとめて送る量")]
    public TransferVolume transferVolume = TransferVolume.DontTransferUntilPossible;

    public enum Condition
    {
        [EnumElement("常時")]
        Always,
        [EnumElement("送れるときのみ")]
        WhenTransfer,
        [EnumElement("吸収又はピンが満杯のとき")]
        IfSendOrFull,
        [EnumElement("放出又はピンが空のとき")]
        IfRecieveOrEmpty,
    }
    [EnumElementUsage(typeof(Condition), "つかめる条件")]
    public Condition grabbableCondition = Condition.Always;

    [LabelOverride("最大エネルギー保有量")]
    public int maxPoint = 1;
    [LabelOverride("エネルギー保有量")]
    public int currentPoint = 0;
    [LabelOverride("次エネルギーを送るか (トグルのときのみ使用)")]
    public bool isSendMode = true;

    public enum ColorTransferType
    {
        [EnumElement("何もしない")]
        None,
        [EnumElement("色をピンへ")]
        SendColor,
        [EnumElement("ピンから色を")]
        RecieveColor,
    }
    [Header("カラー設定")]
    [EnumElementUsage(typeof(ColorTransferType), "タイプ")]
    public ColorTransferType colorTransferType = ColorTransferType.None;

    public enum ColorCondition
    {
        [EnumElement("フリー")]
        Free,
        [EnumElement("同じ色のみ")]
        SameColor,
        [EnumElement("違う色のみ")]
        NonSameColor,
    }
    [EnumElementUsage(typeof(ColorCondition), "つかめる条件")]
    public ColorCondition colorCondition = ColorCondition.Free;

    [EnumElementUsage(typeof(Condition), "エネルギー関連の条件")]
    public Condition colorTransferCondition = Condition.Always;

    [OptionsListAttribute(new[] { "デフォルトカラー", "カラーA (赤)", "カラーB (緑)", "カラーC (青)" }, "色")]
    public int colorIndex;

    public bool rawTouched;

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

        if (grabbableCondition != Condition.Always)
        {
            bool condition = CheckCondition(out bool isSend);

            switch (grabbableCondition)
            {
                case Condition.WhenTransfer:
                    if (!condition)
                        cancel = true;
                    break;
                case Condition.IfSendOrFull:
                    if (!((condition && isSend) || currentPoint >= maxPoint))
                        cancel = true;
                    break;
                case Condition.IfRecieveOrEmpty:
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
            bool condition = CheckCondition(out bool isSend);
            if (condition)
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
                rawTouched = true;
                isSendMode = !isSendMode;
            }

            if (colorTransferCondition == Condition.Always ||
                (colorTransferCondition == Condition.WhenTransfer && condition) ||
                (colorTransferCondition == Condition.IfSendOrFull && ((condition && isSend) || currentPoint >= maxPoint)) ||
                (colorTransferCondition == Condition.IfRecieveOrEmpty && ((condition && !isSend) || currentPoint <= 0)))
            {
                if (colorTransferType == ColorTransferType.SendColor)
                    colorIndex = manager.colorIndex;
                if (colorTransferType == ColorTransferType.RecieveColor)
                    manager.colorIndex = colorIndex;
            }
        }
    }

    void Start()
    {
        GameDirector.Get(transform)?.pointManager.RegisterPoint(this, important);
    }
}
