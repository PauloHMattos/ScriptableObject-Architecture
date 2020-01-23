using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    public abstract class BaseGameEventEditor : SOArchitectureBaseObjectEditor
    {
        private IStackTraceObject Target { get { return (IStackTraceObject)target; } }
        private GameEventBase Event { get { return (GameEventBase)target; } }

        private StackTrace _stackTrace;
        private SerializedProperty _enabledProperty;
        private SerializedProperty _showListners;
        //private SerializedProperty _showStackTrace;

        protected abstract void DrawRaiseButton();

        protected override void OnEnable()
        {
            base.OnEnable();
            _enabledProperty = serializedObject.FindProperty("_enabled");
            _showListners = serializedObject.FindProperty("_showListners");
            //_showStackTrace = serializedObject.FindProperty("_showStackTrace");
            
            _stackTrace = new StackTrace(Target);
            _stackTrace.OnRepaint.AddListener(Repaint);
        }

        protected override void DrawCustomFields()
        {
            DrawRaiseButton();
            base.DrawCustomFields();
        }

        protected override void DrawDeveloperDescription()
        {
            var headerStyle = EditorStyles.foldout;
            headerStyle.font = EditorStyles.boldFont;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showListners.boolValue =
                    EditorGUILayout.Foldout(_showListners.boolValue, new GUIContent("Listers"), headerStyle);
            }
            if (_showListners.boolValue)
            {
                DrawListners();
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Stack Trace", EditorStyles.boldLabel);

            if (!SOArchitecturePreferences.IsDebugEnabled)
                EditorGUILayout.HelpBox("Debug mode disabled\nStack traces will not be filed on raise!", MessageType.Warning);

            _stackTrace.Draw();

            EditorGUILayout.EndVertical();
            base.DrawDeveloperDescription();
        }


        protected void DrawListners()
        {
            EditorGUI.BeginDisabledGroup(true);
            using (new EditorGUI.IndentLevelScope(1))
            {
                EditorGUILayout.IntField("Size", Event.Listeners.Count);
                for (int i = 0; i < Event.Listeners.Count; i++)
                {
                    var listener = Event.Listeners[i];
                    string label = $"Element {i}";
                    if (listener is Object)
                    {
                        EditorGUILayout.ObjectField(label, listener as Object, listener.GetType(), true);
                    }
                    else
                    {
                        EditorGUILayout.TextField(label, $"{listener.ToString()}");
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}