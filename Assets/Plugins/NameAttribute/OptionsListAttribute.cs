using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
public class OptionsListAttribute : PropertyAttribute
{
    public string[] Options { get; private set; }
    public string label;

    public OptionsListAttribute(string[] selfOptions, string selfLabel = "")
    {
        Options = selfOptions;
        label = selfLabel;
    }
}