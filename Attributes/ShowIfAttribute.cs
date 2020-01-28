using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ShowIfAttribute : MultiPropertyAttribute
    {
        private readonly object _value;
        public string FieldName { get; }

        public ShowIfAttribute(string fieldName, object value)
        {
            FieldName = fieldName;
            _value = value;
        }

#if UNITY_EDITOR
        
        internal override bool IsVisible(SerializedProperty property)
        {
            var owner = property.serializedObject.targetObject;
            var eventOwnerType = owner.GetType();

            var field = eventOwnerType.GetField(FieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
            if (field == null)
            {
                return true;
            }
            return field.GetValue(owner).Equals(_value);
        }
#endif
    }
}