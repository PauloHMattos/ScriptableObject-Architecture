using ScriptableObjectArchitecture;
using ScriptableObjectArchitecture.Events.GameEvents;
using ScriptableObjectArchitecture.Utility;
using UnityEditor;
using UnityEngine;

namespace Assets.ScriptableObjectArchitecture.Editor.Inspectors
{
    public abstract class BaseGameEventEditor : SoArchitectureBaseObjectEditor
    {
        private IStackTraceObject Target => (IStackTraceObject)target;
        private GameEventBase Event => (GameEventBase)target;

        private StackTrace _stackTrace;
        private SerializedProperty _enabledProperty;
        private SerializedProperty _showListeners;

        protected abstract void DrawRaiseButton();

        protected override void OnEnable()
        {
            base.OnEnable();
            _enabledProperty = serializedObject.FindProperty("_enabled");
            _showListeners = serializedObject.FindProperty("_showListeners");

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
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showListeners.boolValue =
                    EditorGUILayout.Foldout(_showListeners.boolValue, new GUIContent("Listeners"));
            }
            if (_showListeners.boolValue)
            {
                using (new EditorGUI.IndentLevelScope(1))
                {
                    EditorGUI.BeginDisabledGroup(true);
                    DrawListners();
                    EditorGUI.EndDisabledGroup();
                }
            }

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Stack Trace", EditorStyles.boldLabel);

            if (!SoArchitecturePreferences.IsDebugEnabled)
                EditorGUILayout.HelpBox("Debug mode disabled\nStack traces will not be filed on raise!", MessageType.Warning);

            _stackTrace.Draw();

            EditorGUILayout.EndVertical();
            base.DrawDeveloperDescription();
        }

        protected virtual void DrawListners()
        {
            var count = Event.Listeners.Count + Event.Actions.Count;
            var listeners = Event.Listeners;

            EditorGUILayout.IntField("Size", count);
            for (var i = 0; i < Event.Listeners.Count; i++)
            {
                var listener = listeners[i];
                var label = $"Element {i}";
                if (listener is Object)
                {
                    EditorGUILayout.ObjectField(label, listener as Object, listener.GetType(), true);
                }
                else
                {
                    EditorGUILayout.TextField(label, $"{listener.ToString()}");
                }
            }

            var actions = Event.Actions;
            for (var i = 0; i < Event.Actions.Count; i++)
            {
                var action = actions[i];
                var label = $"Element {Event.Listeners.Count + i}";
                var value = $"{action.Method.ReflectedType.Name}.{action.Method.Name}";
                EditorGUILayout.TextField(label, value);
            }
        }
    }
}