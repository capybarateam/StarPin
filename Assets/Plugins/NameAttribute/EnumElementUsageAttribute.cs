using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum, AllowMultiple = false)]
public class EnumElementUsageAttribute : PropertyAttribute
{
    public System.Type Type { get; private set; }
    public string label;

    public EnumElementUsageAttribute(System.Type selfType, string selfLabel = "")
    {
        Type = selfType;
        label = selfLabel;
    }
}