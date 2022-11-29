using System;
using UnityEngine;
using UnityEditor;


#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(NamesElementsArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer {
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
    }
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        try {
            var config = attribute as NamesElementsArrayAttribute;
            var enum_names = Enum.GetNames(config.TargetEnum);
            int pos = int.Parse(property.propertyPath.Split('[', ']')[1]);
            var enum_label = enum_names.GetValue(pos) as string;
            // Make names nicer to read (but won't exactly match enum definition).
            enum_label = ObjectNames.NicifyVariableName(enum_label.ToLower());
            label = new GUIContent(enum_label);
        } catch {
            label = new GUIContent("Unknown");
        }
        EditorGUI.PropertyField(position, property, label, property.isExpanded);
    }
}
#endif