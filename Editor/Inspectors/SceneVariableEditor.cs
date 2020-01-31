using ScriptableObjectArchitecture.Variables;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(SceneVariable))]
    internal sealed class SceneVariableEditor : SoArchitectureBaseObjectEditor
    {
        // UI
        private const string SCENE_NOT_ASSIGNED_WARNING = "Please assign a scene as the current serialized values for " +
                                             "the scene do not resolve to an asset in the project.";
        private const string SCENE_NOT_IN_BUILD_SETTINGS_WARNING =
            "Scene assigned is not currently in the Build Settings";
        private const string SCENE_NOT_ENABLED_IN_BUILD_SETTINGS_WARNING =
            "Scene assigned is present in build settings, but not enabled.";

        // Serialized Properties
        private const string SCENE_INFO_PROPERTY = "_value";

        protected override void DrawCustomFields()
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                var label = new GUIContent("Scene Info");
                label.image = EditorGUIUtility.IconContent("SceneAsset Icon").image;
                _showGroups.boolValue = EditorGUILayout.Foldout(_showGroups.boolValue, label, Styles.FoldoutHeader);
                if (_showGroups.boolValue)
                {
                    DrawSceneInfo();
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void DrawSceneInfo()
        {
            var sceneVariable = (SceneVariable)target;
            var sceneInfoProperty = serializedObject.FindProperty(SCENE_INFO_PROPERTY);
            if (sceneVariable.Value.Scene == null)
            {
                EditorGUILayout.HelpBox(SCENE_NOT_ASSIGNED_WARNING, MessageType.Warning);
            }
            else if (!sceneVariable.Value.IsSceneInBuildSettings)
            {
                EditorGUILayout.HelpBox(SCENE_NOT_IN_BUILD_SETTINGS_WARNING, MessageType.Warning);
            }
            else if (!sceneVariable.Value.IsSceneEnabled)
            {
                EditorGUILayout.HelpBox(SCENE_NOT_ENABLED_IN_BUILD_SETTINGS_WARNING, MessageType.Warning);
            }
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(sceneInfoProperty);
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
        }

        public override bool RequiresConstantRepaint()
        {
            return true;
        }
    }
}