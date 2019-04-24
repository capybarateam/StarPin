using UnityEngine;

public class LabelOverride : PropertyAttribute
{
    public string label;

    public LabelOverride(string label)
    {
        this.label = label;
    }
}