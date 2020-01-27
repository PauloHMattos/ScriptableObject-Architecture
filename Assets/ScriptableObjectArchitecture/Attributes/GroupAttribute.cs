using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
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
}