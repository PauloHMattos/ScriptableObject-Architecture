using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    public abstract class BaseListenerEditor : SOArchitectureBaseMonoBehaviourEditor
    {
        protected SerializedProperty _event;
        private SerializedProperty _debugColor;
        private SerializedProperty _response;
        private SerializedProperty _enableDebug;
        private SerializedProperty _showGeneralFields;
        private SerializedProperty _showResponseFields;
        private SerializedProperty _showDebugFields;
        private GUIStyle _headerStyle;

        protected override void OnEnable()
        {
            base.OnEnable();
            _event = serializedObject.FindProperty("_event");
            _debugColor = serializedObject.FindProperty("_debugColor");
            _response = serializedObject.FindProperty("_response");
            _enableDebug = serializedObject.FindProperty("_enableGizmoDebugging");
            _showGeneralFields = serializedObject.FindProperty("_showGeneralFields");
            _showResponseFields = serializedObject.FindProperty("_showResponseFields");
            _showDebugFields = serializedObject.FindProperty("_showDebugFields");
        }

        public override void OnInspectorGUI()
        {
            _headerStyle = EditorStyles.foldout;
            _headerStyle.font = EditorStyles.boldFont;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            using (new EditorGUI.IndentLevelScope())
            {
                _showGeneralFields.boolValue =
                    EditorGUILayout.Foldout(_showGeneralFields.boolValue, new GUIContent("General"), _headerStyle);
                if (_showGeneralFields.boolValue)
                {
                    DrawGameEventField();
                }
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();

            serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawDeveloperDescription()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showResponseFields.boolValue =
                    EditorGUILayout.Foldout(_showResponseFields.boolValue, new GUIContent("Response"), _headerStyle);
            }
            if (_showResponseFields.boolValue)
            {
                DrawResponseField();
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawDebugging();
            EditorGUILayout.EndVertical();
            base.DrawDeveloperDescription();
        }

        protected virtual void DrawGameEventField()
        {
            EditorGUILayout.ObjectField(_event, new GUIContent("Event", "Event which will trigger the response"));
        }

        protected virtual void DrawResponseField()
        {
            EditorGUILayout.PropertyField(_response, new GUIContent("Response"));
        }

        protected virtual void DrawDebugging()
        {
            using (new EditorGUI.IndentLevelScope())
            {
                var style = EditorStyles.foldout;
                style.font = EditorStyles.boldFont;
                _showDebugFields.boolValue =
                    EditorGUILayout.Foldout(_showDebugFields.boolValue, new GUIContent("Debug"), style);
                if (!_showDebugFields.boolValue)
                {
                    return;
                }

                DrawRaiseButton();
                

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
            }
        }

        protected virtual void DrawRaiseButton()
        {
            EditorGUILayout.LabelField("Callback Debugging", EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope())
            {
                SerializedProperty property = serializedObject.FindProperty("_debugValue");
                if (property != null)
                {
                    EditorGUILayout.PropertyField(property);
                }

                if (GUILayout.Button("Raise"))
                {
                    CallMethod(GetDebugValue(property));
                }
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

       protected virtual object GetDebugValue(SerializedProperty property)
        {
            Type targetType = property.serializedObject.targetObject.GetType();
            FieldInfo targetField = targetType.GetField("_debugValue", BindingFlags.Instance | BindingFlags.NonPublic);

            return targetField.GetValue(property.serializedObject.targetObject);
        }

        protected abstract void CallMethod(object value);
    }

    public abstract class BaseGameEventListenerEditor : BaseListenerEditor
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