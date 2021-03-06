﻿using System.Collections.Generic;
using ScriptableObjectArchitecture.Collections;
using ScriptableObjectArchitecture.Variables;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(SceneCollection), true)]
    public class SceneCollectionEditor : CollectionEditor
    {
        private SceneCollection Target => (SceneCollection)target;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set as build Scenes"))
            {
                SetEditorBuildSettingsScenes();
            }
        }

        private void SetEditorBuildSettingsScenes()
        {
            // Find valid Scene paths and make a list of EditorBuildSettingsScene
            var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            foreach (SceneVariable sceneVariable in Target.List)
            {
                var scenePath = AssetDatabase.GetAssetPath(sceneVariable.Value.Scene);
                if (!string.IsNullOrEmpty(scenePath))
                {
                    editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(scenePath, true));
                }
            }

            // Set the Build Settings window Scene list
            EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
        }
    }
}