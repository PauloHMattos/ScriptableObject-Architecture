using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    public abstract class BaseListnerEditor : UnityEditor.Editor
    {
        private SerializedProperty DeveloperDescription { get { return serializedObject.FindProperty("DeveloperDescription"); } }
        
        protected SerializedProperty _event;
        private SerializedProperty _debugColor;
        private SerializedProperty _response;
        private SerializedProperty _enableDebug;
        private SerializedProperty _showDebugFields;

        protected virtual void OnEnable()
        {
            _event = serializedObject.FindProperty("_event");
            _debugColor = serializedObject.FindProperty("_debugColor");
            _response = serializedObject.FindProperty("_response");
            _enableDebug = serializedObject.FindProperty("_enableGizmoDebugging");
            _showDebugFields = serializedObject.FindProperty("_showDebugFields");
        }



        public override void OnInspectorGUI()
        {
            DrawGameEventField();
            DrawResponseField();

            DrawDebugging();

            DrawDeveloperDescription();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawGameEventField()
        {
            EditorGUILayout.ObjectField(_event, new GUIContent("Event", "Event which will trigger the response"));
        }

        protected virtual void DrawResponseField()
        {
            EditorGUILayout.PropertyField(_response, new GUIContent("Response"));
        }

        protected virtual void DrawDeveloperDescription()
        {
            EditorGUILayout.PropertyField(DeveloperDescription);
        }

        private void DrawDebugging()
        {
            _showDebugFields.boolValue = EditorGUILayout.Foldout(_showDebugFields.boolValue, new GUIContent("Show Debug Fields"));
            if (!_showDebugFields.boolValue)
            {
                return;
            }

            EditorGUILayout.LabelField("Callback Debugging", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                DrawRaiseButton();
            }


            EditorGUILayout.Space();
            EditorGUILayout.Space();


            EditorGUILayout.LabelField("Gizmo Debugging", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                EditorGUILayout.PropertyField(_enableDebug, new GUIContent("Enable Gizmo Debugging"));

                using (new EditorGUI.DisabledGroupScope(!_enableDebug.boolValue))
                {
                    EditorGUILayout.PropertyField(_debugColor, new GUIContent("Debug Color", "Color used to draw debug gizmos in the scene"));
                }
            }
            EditorGUILayout.Space();
        }

        protected virtual void DrawRaiseButton()
        {
            SerializedProperty property = serializedObject.FindProperty("_debugValue");

            EditorGUILayout.PropertyField(property);

            if (GUILayout.Button("Raise"))
            {
                CallMethod(GetDebugValue(property));
            }
        }

       protected virtual object GetDebugValue(SerializedProperty property)
        {
            Type targetType = property.serializedObject.targetObject.GetType();
            FieldInfo targetField = targetType.GetField("_debugValue", BindingFlags.Instance | BindingFlags.NonPublic);

            return targetField.GetValue(property.serializedObject.targetObject);
        }

        protected abstract void CallMethod(object value);
    }

    public abstract class BaseGameEventListenerEditor : BaseListnerEditor
    {
        private IStackTraceObject Target { get { return (IStackTraceObject)target; } }
        
        private StackTrace _stackTrace;


        protected override void OnEnable()
        {
            _stackTrace = new StackTrace(Target, true);
            _stackTrace.OnRepaint.AddListener(Repaint);

            base.OnEnable();
        }


        protected override void DrawRaiseButton()
        {
            base.DrawRaiseButton();
            DrawStackTrace();
        }

        protected virtual void DrawStackTrace()
        {
            _stackTrace.Draw();
        }
    } 
}