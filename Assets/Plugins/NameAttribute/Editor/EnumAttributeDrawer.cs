using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumElementUsageAttribute))]
public class EnumAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = attribute as EnumElementUsageAttribute;

        // FieldInfoから各要素のAttributeを取得し名前を得る
        var names = new List<string>();
        foreach (var fi in attr.Type.GetFields())
        {
            if (fi.IsSpecialName)
            {
                // SpecialNameは飛ばす
                continue;
            }
            var elementAttribute = fi.GetCustomAttributes(typeof(EnumElementAttribute), false).FirstOrDefault() as EnumElementAttribute;
            names.Add(elementAttribute == null ? fi.Name : elementAttribute.DisplayName);
        }

        // 各要素の値はEnum.GetValues()で取得する
        var values = System.Enum.GetValues(attr.Type).Cast<int>();

        // 描画
        label.text = attr.label != "" ? attr.label : property.displayName;
        property.intValue = EditorGUI.IntPopup(position, label.text, property.intValue, names.ToArray(), values.ToArray());
    }
}