using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field | System.AttributeTargets.Enum, AllowMultiple = false)]
public class EnumElementAttribute : System.Attribute
{
    public string DisplayName { get; private set; }

    public EnumElementAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}