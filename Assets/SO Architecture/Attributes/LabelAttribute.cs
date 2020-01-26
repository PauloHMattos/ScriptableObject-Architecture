using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class LabelAttribute : MultiPropertyAttribute
{
    public GUIContent Label { get; }

    public LabelAttribute(string text)
    {
        Label = new GUIContent(text);
    }

    public LabelAttribute(string text, string iconName)
    {
        Label = EditorGUIUtility.IconContent(iconName, text);
        Label.text = text;
    }

#if UNITY_EDITOR
    internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        label.text = Label.text;
        label.image = Label.image;
    }
#endif
}