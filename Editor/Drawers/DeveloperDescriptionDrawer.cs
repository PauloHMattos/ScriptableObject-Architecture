using ScriptableObjectArchitecture.Utility;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Drawers
{
    [CustomPropertyDrawer(typeof(DeveloperDescription))]
    public class DeveloperDescriptionDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var showDescription = property.FindPropertyRelative("_showDescription");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                var content = new GUIContent("Description");
                content.image = EditorGUIUtility.IconContent("TextAsset Icon").image;
                showDescription.boolValue = EditorGUILayout.Foldout(showDescription.boolValue, content, EditorStyles.foldoutHeader);
            }
            if (showDescription.boolValue)
            {
                DrawTextArea(ref position, property);
            }
            EditorGUILayout.EndVertical();

            property.serializedObject.ApplyModifiedProperties();
        }

        private void DrawTextArea(ref Rect rect, SerializedProperty property)
        {
            var stringValue = property.FindPropertyRelative("_value");
            stringValue.stringValue = EditorGUILayout.TextArea(stringValue.stringValue, Styles.TextAreaStyle);
            HandleInput(rect, property);
        }

        private void HandleInput(Rect textAreaRect, SerializedProperty property)
        {
            var e = Event.current;

            if (e.type == EventType.MouseDown)
            {
                if (!textAreaRect.Contains(e.mousePosition))
                    RemoveFocus(property);
            }
            else if (e.type == EventType.KeyDown || e.type == EventType.KeyUp)
            {
                if (Event.current.keyCode == (KeyCode.Escape))
                {
                    RemoveFocus(property);
                }
            }
        }

        private void RemoveFocus(SerializedProperty property)
        {
            GUI.FocusControl(null);
            Repaint(property);
        }

        private void Repaint(SerializedProperty property)
        {
            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        private static string GetContent(SerializedProperty property)
        {
            return property.FindPropertyRelative("_value").stringValue;
        }
        
        private static class Styles
        {
            public static GUIStyle TextAreaStyle;

            static Styles()
            {
                TextAreaStyle = new GUIStyle(EditorStyles.textArea);
                TextAreaStyle.normal = EditorStyles.label.normal;
            }
        }
    }
}