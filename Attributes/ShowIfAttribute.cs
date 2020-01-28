using System;
using System.Reflection;
using UnityEditor;

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
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
                var prop = eventOwnerType.GetProperty(FieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                if (prop == null)
                {
                    return true;
                }
                return prop.GetValue(owner).Equals(_value);
            }
            return field.GetValue(owner).Equals(_value);
        }
#endif
    }
}