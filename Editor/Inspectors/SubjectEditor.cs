using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(SOArchitectureBaseObject), true)]
    public class SOArchitectureBaseObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
        }

        public override void OnInspectorGUI()
        {
            DrawDeveloperDescription();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawDeveloperDescription()
        {
            EditorGUILayout.PropertyField(_developerDescription);
        }
    }

    [CustomEditor(typeof(Subject), true)]
    public class SubjectEditor : SOArchitectureBaseObjectEditor
    {
        private Subject Target { get { return (Subject)target; } }

        private SerializedProperty _showObservers;

        protected override void OnEnable()
        {
            base.OnEnable();
            _showObservers = serializedObject.FindProperty("_showObservers");
        }

        public override void OnInspectorGUI()
        {
            var headerStyle = EditorStyles.foldout;
            headerStyle.font = EditorStyles.boldFont;

            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showObservers.boolValue =
                    EditorGUILayout.Foldout(_showObservers.boolValue, new GUIContent("Observers"), headerStyle);
            }
            if (_showObservers.boolValue)
            {
                DrawObservers();
            }

            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();
        }

        protected void DrawObservers()
        {
            EditorGUI.BeginDisabledGroup(true);
            using (new EditorGUI.IndentLevelScope(1))
            {
                EditorGUILayout.IntField("Size", Target.Observers.Count);
                for (int i = 0; i < Target.Observers.Count; i++)
                {
                    var observer = Target.Observers[i];
                    string label = $"Element {i}";
                    if (observer is Object)
                    {
                        EditorGUILayout.ObjectField(label, observer as Object, observer.GetType(), true);
                    }
                    else
                    {
                        EditorGUILayout.TextField(label, $"{observer.ToString()}");
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}