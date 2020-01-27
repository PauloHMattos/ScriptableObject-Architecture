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

        public GroupAttribute(string text)
        {
            Label = new GUIContent(text);
        }

        public GroupAttribute(string text, string iconName = "") : this(text)
        {
#if UNITY_EDITOR
            Label.image = EditorGUIUtility.IconContent(iconName, text).image;
#endif
        }
    }
}