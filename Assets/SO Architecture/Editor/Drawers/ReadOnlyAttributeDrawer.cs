using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            ReadOnlyAttribute attr = (ReadOnlyAttribute)attribute;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.PropertyField(rect, property, label, true);
            EditorGUI.EndDisabledGroup();
        }

        //public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        //{
        //    return EditorGUIUtility.singleLineHeight;
        //}
    }
}