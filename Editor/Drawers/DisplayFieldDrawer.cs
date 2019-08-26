using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomPropertyDrawer(typeof(DisplayFieldAttribute))]
    internal sealed class DisplayFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect propertyRect, SerializedProperty property, GUIContent label)
        {
            DisplayFieldAttribute display = attribute as DisplayFieldAttribute;

            if (!string.IsNullOrEmpty(display.Label))
            {
                label = new GUIContent(display.Label);
            }
            EditorGUILayout.PropertyField(property, label);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }

        public static void DrawCustomFields(UnityEngine.Object target, SerializedObject serializedObject)
        {
            var bindingFlags = BindingFlags.Instance |
                               BindingFlags.NonPublic |
                               BindingFlags.Public;
            var fields = target.GetType().GetFields(bindingFlags).Where(f => f.GetCustomAttribute<DisplayFieldAttribute>(true) != null).ToList();
            if (fields.Count == 0)
            {
                return;
            }

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Fields", EditorStyles.boldLabel);
            foreach (var fieldInfo in fields)
            {
                var attribute = fieldInfo.GetCustomAttribute(typeof(DisplayFieldAttribute), true);
                if (attribute != null)
                {
                    var property = serializedObject.FindProperty(fieldInfo.Name);
                    if (property != null)
                    {
                        EditorGUILayout.PropertyField(property);
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}