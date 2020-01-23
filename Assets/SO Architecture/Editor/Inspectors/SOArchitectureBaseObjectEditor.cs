using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(SOArchitectureBaseObject), true)]
    public class SOArchitectureBaseObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty _showCustomFields;
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
            _showCustomFields = serializedObject.FindProperty("_showCustomFields");
        }

        public override void OnInspectorGUI()
        {
            DrawCustomFields();

            DrawDeveloperDescription();

            serializedObject.ApplyModifiedProperties();
        }

        protected virtual void DrawDeveloperDescription()
        {
            EditorGUILayout.PropertyField(_developerDescription);
        }

        protected virtual void DrawCustomFields()
        {
            var fields = GetCustomFields(target);
            if (fields.Count == 0)
            {
                return;
            }

            var _headerStyle = EditorStyles.foldout;
            _headerStyle.font = EditorStyles.boldFont;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                _showCustomFields.boolValue =
                    EditorGUILayout.Foldout(_showCustomFields.boolValue, new GUIContent("Fields"), _headerStyle);
                if (_showCustomFields.boolValue)
                {
                    DrawCustomFields(fields, serializedObject);
                }
            }
            EditorGUILayout.EndVertical();
        }

        public static List<FieldInfo> GetCustomFields(UnityEngine.Object target)
        {
            List<FieldInfo> fields = new List<FieldInfo>();

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public;
            fields.AddRange(target.GetType().GetFields(bindingFlags).Where(f => f.GetCustomAttribute<HideInInspector>(true) == null));

            bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            fields.AddRange(target.GetType().GetFields(bindingFlags).Where(f => {
                var hide = f.GetCustomAttribute<HideInInspector>(true) != null;
                var seralizeField = f.GetCustomAttribute<SerializeField>(true) != null;
                return !hide && seralizeField;
            }));
            return fields;
        }

        public static void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject)
        {
            foreach (var fieldInfo in fields)
            {
                var property = serializedObject.FindProperty(fieldInfo.Name);
                if (property != null)
                {
                    EditorGUILayout.PropertyField(property);
                }
            }
        }
    }
}