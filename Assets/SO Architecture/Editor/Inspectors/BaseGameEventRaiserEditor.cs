using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BaseGameEventRaiser), true)]
    public class BaseGameEventRaiserEditor : SOArchitectureBaseMonoBehaviourEditor
    {

        private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };

        private BaseGameEventRaiser Target { get { return (BaseGameEventRaiser)target; } }
        protected SerializedProperty _events;
        protected SerializedProperty _showGeneral;
        protected SerializedProperty _showEvents;


        protected override void OnEnable()
        {
            base.OnEnable();
            _events = serializedObject.FindProperty("_events");
            _showGeneral = serializedObject.FindProperty("_showGeneral");
            _showEvents = serializedObject.FindProperty("_showEvents");
        }

        public override void OnInspectorGUI()
        {
            var headerStyle = EditorStyles.foldout;
            headerStyle.font = EditorStyles.boldFont;
            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showGeneral.boolValue =
                    EditorGUILayout.Foldout(_showGeneral.boolValue, new GUIContent("General"), headerStyle);
            }
            if (_showGeneral.boolValue)
            {
                DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
            }
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showEvents.boolValue =
                    EditorGUILayout.Foldout(_showEvents.boolValue, new GUIContent("Events"), headerStyle);
            }
            if (_showEvents.boolValue)
            {
                DrawEvents();
            }
            EditorGUILayout.EndVertical();
            base.OnInspectorGUI();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawEvents()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Response", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_events);
            EditorGUILayout.EndVertical();
        }


    }
}