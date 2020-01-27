using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class ColorAttribute : MultiPropertyAttribute
    {
        public Color Color { get; set; }
        public bool Background { get; set; } = true;
        private Color _prevColor;

        public ColorAttribute(byte r, byte g, byte b, byte a = 255)
        {
            Color = new Color(r, g, b, a);
        }

#if UNITY_EDITOR
        internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Background)
            {
                _prevColor = GUI.backgroundColor;
                GUI.backgroundColor = Color;
            }
            else
            {

                _prevColor = GUI.color;
                GUI.color = Color;
            }
        }

        internal override void OnPostGUI(Rect position, SerializedProperty property)
        {
            if (Background)
            {
                GUI.backgroundColor = _prevColor;
            }
            else
            {
                GUI.color = _prevColor;
            }
        }
#endif
    }
}