using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public class ColorAttribute : MultiPropertyAttribute
{
    public Color Color { get; set; }
    private Color _prevColor;

    public ColorAttribute(Color color)
    {
        Color = color;
    }
    public ColorAttribute(byte R, byte G, byte B, byte A = 255)
    {
        Color = new Color(R, G, B, A);
    }

    internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        _prevColor = GUI.color;
        GUI.color = Color;
    }

    internal override void OnPostGUI(Rect position, SerializedProperty property)
    {
        GUI.color = _prevColor;
    }
}