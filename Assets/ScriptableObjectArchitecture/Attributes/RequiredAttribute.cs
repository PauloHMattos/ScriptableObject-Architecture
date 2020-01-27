using System;
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredAttribute : MultiPropertyAttribute
    {
        public RequiredAttribute()
        {
        }

#if UNITY_EDITOR
        internal override void OnPostGUI(Rect position, SerializedProperty property)
        {
            base.OnPostGUI(position, property);

            if (property.propertyType == SerializedPropertyType.ObjectReference &&
                property.objectReferenceValue == null)
            {
                EditorGUILayout.HelpBox("Required field", MessageType.Error);
            }
        }
#endif
    }
}