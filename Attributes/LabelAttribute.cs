using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class LabelAttribute : MultiPropertyAttribute
    {
        public GUIContent Label { get; }

        public LabelAttribute(string text)
        {
            Label = new GUIContent(text);
        }

        public LabelAttribute(string text, string iconName) : this(text)
        {
#if UNITY_EDITOR
            Label.image = EditorGUIUtility.IconContent(iconName, text).image;
#endif
        }

#if UNITY_EDITOR
        internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label.text = Label.text;
            label.image = Label.image;
        }
#endif
    }
}