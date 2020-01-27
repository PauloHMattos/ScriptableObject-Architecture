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

        public ColorAttribute(Color color)
        {
            Color = color;
        }
        public ColorAttribute(byte R, byte G, byte B, byte A = 255)
        {
            Color = new Color(R, G, B, A);
        }

#if UNITY_ENGINE
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