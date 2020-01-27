using System;
using System.Collections.Generic;
using System.Reflection;
using ScriptableObjectArchitecture.Events.Listeners;
using ScriptableObjectArchitecture.Observers;
using ScriptableObjectArchitecture.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(BaseObserver), true)]
    public class BaseObserverEditor : DebuggableGameEventListenerEditor
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            _raiseMethod = target.GetType().GetMethod("RaiseResponse", BindingFlags.NonPublic);
        }
    }

    public class DebuggableGameEventListenerEditor : SoArchitectureBaseMonoBehaviourEditor
    {
        private SerializedProperty _debugColor;
        private SerializedProperty _enableDebug;

        private IStackTraceObject Target => (IStackTraceObject)target;
        private StackTrace _stackTrace;

        protected MethodInfo _raiseMethod;

        protected override void OnEnable()
        {
            base.OnEnable();
            _debugColor = serializedObject.FindProperty("_debugColor");
            _enableDebug = serializedObject.FindProperty("_enableGizmoDebugging");

            _stackTrace = new StackTrace(Target, true);
            _stackTrace.OnRepaint.AddListener(Repaint);

            _raiseMethod = target.GetType().BaseType.GetMethod(nameof(IGameEventListener.OnEventRaised));
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }

        protected override void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
        {
            if (groupName.Equals("Debug"))
            {
                DrawDebugging();
            }
            else
            {
                base.DrawCustomFields(fields, serializedObject, groupName);
            }
        }

        protected virtual void DrawDebugging()
        {
            EditorGUILayout.LabelField("Gizmo Debugging", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_enableDebug, new GUIContent("Enable Gizmo"));

                using (new EditorGUI.DisabledGroupScope(!_enableDebug.boolValue))
                {
                    EditorGUILayout.PropertyField(_debugColor,
                        new GUIContent("Debug Color", "Color used to draw debug gizmos in the scene"));
                }
            }
            DrawRaiseButton();
        }

        protected virtual void DrawRaiseButton()
        {
            EditorGUILayout.LabelField("Callback Debugging", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                var property = serializedObject.FindProperty("_debugValue");
                if (property != null)
                {
                    EditorGUILayout.PropertyField(property);
                }

                if (GUILayout.Button("Raise"))
                {
                    CallMethod(GetDebugValue(property));
                }
                DrawStackTrace();
            }
        }

        protected virtual void DrawStackTrace()
        {
            _stackTrace.Draw();
        }

        protected virtual object GetDebugValue(SerializedProperty property)
        {
            if (property == null)
                return null;

            var targetType = property.serializedObject.targetObject.GetType();
            var targetField = targetType.GetField("_debugValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return targetField.GetValue(property.serializedObject.targetObject);
        }

        protected virtual void CallMethod(object value)
        {
            if (value == null)
            {
                _raiseMethod.Invoke(target, null);
                return;
            }
            _raiseMethod.Invoke(target, new object[1] { value });
        }
    }
}