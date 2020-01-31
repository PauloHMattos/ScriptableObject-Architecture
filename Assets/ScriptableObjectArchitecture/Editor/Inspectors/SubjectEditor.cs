using ScriptableObjectArchitecture.Variables;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(Subject), true)]
    public class SubjectEditor : SoArchitectureBaseObjectEditor
    {
        private Subject Target => (Subject)target;

        private SerializedProperty _showObservers;

        protected override void OnEnable()
        {
            base.OnEnable();
            _showObservers = serializedObject.FindProperty("_showObservers");
        }

        protected override void DrawDeveloperDescription()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                var label = new GUIContent("Observers") {image = EditorGUIUtility.IconContent("ViewToolOrbit").image};
                _showObservers.boolValue = EditorGUILayout.Foldout(_showObservers.boolValue, label, Styles.FoldoutHeader);
            }
            if (_showObservers.boolValue)
            {
                DrawObservers();
            }

            EditorGUILayout.EndVertical();
            base.DrawDeveloperDescription();
        }

        protected void DrawObservers()
        {
            EditorGUI.BeginDisabledGroup(true);
            using (new EditorGUI.IndentLevelScope(1))
            {
                EditorGUILayout.IntField("Size", Target.Observers.Count);
                for (var i = 0; i < Target.Observers.Count; i++)
                {
                    var observer = Target.Observers[i];
                    var label = $"Element {i}";
                    if (observer is Object obj)
                    {
                        EditorGUILayout.ObjectField(label, obj, obj.GetType(), true);
                    }
                    else
                    {
                        EditorGUILayout.TextField(label, $"{observer}");
                    }
                }
            }
            EditorGUI.EndDisabledGroup();
        }
    }
}