using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class RequiredAttribute : MultiPropertyAttribute
{
    public RequiredAttribute()
    {
    }

    internal override void OnPostGUI(Rect position, SerializedProperty property)
    {
        base.OnPostGUI(position, property);

        if (property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null)
        {
            EditorGUILayout.HelpBox("Required field", MessageType.Error);
        }
    }
}
