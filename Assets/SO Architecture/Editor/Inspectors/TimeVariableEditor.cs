﻿using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(TimeVariable), true)]
    public class TimeVariableEditor : ReadOnlyFloatVariableEditor
    {

        private TimeVariable Target { get { return (TimeVariable)target; } }
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