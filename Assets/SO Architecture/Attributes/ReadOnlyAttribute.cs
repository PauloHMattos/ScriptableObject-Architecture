using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ReadOnlyAttribute : MultiPropertyAttribute
{
    public ReadOnlyAttribute()
    {
    }

#if UNITY_EDITOR
    internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(true);
    }

    internal override void OnPostGUI(Rect position, SerializedProperty property)
    {
        EditorGUI.EndDisabledGroup();
    }
#endif
}
