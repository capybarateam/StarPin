using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(OptionsListAttribute))]
public class OptionsListAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as OptionsListAttribute;

        // 描画
        label.text = attr.label != "" ? attr.label : property.displayName;
        property.intValue = EditorGUI.Popup(position, label.text, property.intValue, attr.Options);
    }
}