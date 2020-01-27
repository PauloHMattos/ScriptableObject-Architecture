using System;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public abstract class MultiPropertyAttribute : PropertyAttribute
    {
        public IOrderedEnumerable<object> stored = null;

#if UNITY_EDITOR
        public virtual void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
        }

        internal virtual void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
        {
        }

        internal virtual void OnPostGUI(Rect position, SerializedProperty property)
        {
        }

        internal virtual bool IsVisible(SerializedProperty property)
        {
            return true;
        }

        internal virtual float? GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return null;
        }
#endif
    }
}