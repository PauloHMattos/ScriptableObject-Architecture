using ScriptableObjectArchitecture.Variables;
using UnityEditor;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(TimeVariable), true)]
    public class TimeVariableEditor : ReadOnlyFloatVariableEditor
    {
        private TimeVariable Target => (TimeVariable)target;
        private SerializedProperty _timeType;

        protected override void OnEnable()
        {
            base.OnEnable();
            _timeType = serializedObject.FindProperty("_timeType");
        }
        protected override void DrawValue()
        {
            // Call Value.get so the displayed value also gets updated
            var value = Target.Value;
            base.DrawValue();
            EditorGUILayout.PropertyField(_timeType, new GUIContent("Type"));
        }
    }
}