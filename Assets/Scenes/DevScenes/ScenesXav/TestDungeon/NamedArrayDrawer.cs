using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Properly configure height for expanded contents.
        return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Replace label with enum name if possible.
        try
        {
            var config = attribute as NamedArrayAttribute;
            var enum_names = System.Enum.GetNames(config.TargetEnum);

            var match = Regex.Match(property.propertyPath, "\\[(\\d)\\]", RegexOptions.RightToLeft);
            int pos = int.Parse(match.Groups[1].Value);

            // Make names nicer to read (but won't exactly match enum definition).
            var enum_label = ObjectNames.NicifyVariableName(enum_names[pos].ToLower());
            label = new GUIContent(enum_label);
        }
        catch
        {
            // keep default label
        }
        EditorGUI.PropertyField(position, property, label, property.isExpanded);
    }
}