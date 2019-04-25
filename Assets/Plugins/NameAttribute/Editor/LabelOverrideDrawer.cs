//CREDITS: https://answers.unity.com/questions/603882/serializedproperty-isnt-being-detected-as-an-array.html
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LabelOverride))]
public class LabelOverrideDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        try
        {
            var propertyAttribute = this.attribute as LabelOverride;
            if (IsItBloodyArrayTho(property) == false)
            {
                label.text = propertyAttribute.label;

            }
            else
            {
                Debug.LogWarningFormat(
                    "{0}(\"{1}\") doesn't support arrays ",
                    typeof(LabelOverride).Name,
                    propertyAttribute.label
                );
            }
            EditorGUI.PropertyField(position, property, label);
        }
        catch (System.Exception ex) { Debug.LogException(ex); }
    }

    bool IsItBloodyArrayTho(SerializedProperty property)
    {
        string path = property.propertyPath;
        int idot = path.IndexOf('.');
        if (idot == -1) return false;
        string propName = path.Substring(0, idot);
        SerializedProperty p = property.serializedObject.FindProperty(propName);
        return p.isArray;
    }
}
