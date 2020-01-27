using System;
using UnityEditor;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class GroupAttribute : Attribute
{
    public GUIContent Label;
    public bool Hidden;
    public Color Tint { get; set; }

    public GroupAttribute(string text)
    {
        Label = new GUIContent(text);
    }

    public GroupAttribute(string text, string iconName = "")
    {
        Label = EditorGUIUtility.IconContent(iconName, text);
        Label.text = text;
    }
}

[AttributeUsage(AttributeTargets.Field)]
public class GroupColorAttribute : Attribute
{
    public Color Color { get; set; }

    public GroupColorAttribute(byte R, byte G, byte B)
    {
        Color = new Color(R, G, B);
    }
}
