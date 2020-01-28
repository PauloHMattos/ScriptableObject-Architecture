using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class Indent : MultiPropertyAttribute
    {
        private int _level;

        public Indent(int level = 1)
        {
            _level = level;
        }

#if UNITY_EDITOR
        internal override void OnPreGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.indentLevel+=_level;
        }
        internal override void OnPostGUI(Rect position, SerializedProperty property)
        {
            EditorGUI.indentLevel-=_level;
        }
#endif
    }
}