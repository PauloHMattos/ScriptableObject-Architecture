using System;
using System.Collections;
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
        private SerializedProperty _showGroups;
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
            _showGroups = serializedObject.FindProperty("_showGroups");
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
            var groups = GetCustomFields(target);
            if (groups.Count == 0)
            {
                return;
            }

            var _headerStyle = EditorStyles.foldout;
            _headerStyle.font = EditorStyles.boldFont;
            int showFlags = _showGroups.intValue;

            for (int i = 0; i < groups.Count; i++)
            {
                var group = groups.ElementAt(i);
                bool show = IsBitSet(showFlags, i);
                
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                using (new EditorGUI.IndentLevelScope())
                {
                    show = EditorGUILayout.Foldout(show, new GUIContent(group.Key), _headerStyle);
                    if (show)
                    {
                        var fields = group.Value;
                        DrawCustomFields(fields, serializedObject);
                    }
                }
                EditorGUILayout.EndVertical();

                SetBit(ref showFlags, i, show);
            }
            _showGroups.intValue = showFlags;
        }

        public static Dictionary<string, List<FieldInfo>> GetCustomFields(UnityEngine.Object target)
        {
            var dict = new Dictionary<string, List<FieldInfo>>();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;


            string currentHeader = "";
            var fields = target.GetType().GetFields(bindingFlags);

            foreach(var f in fields)
            {
                var hide = f.GetCustomAttribute<HideInInspector>(true) != null;
                var seralizeField = f.GetCustomAttribute<SerializeField>(true) != null;
                var header = f.GetCustomAttribute<GroupAttribute>(true);

                if (header != null)
                {
                    currentHeader = header.header;
                }

                bool show = (f.IsPrivate) ? !hide && seralizeField : !hide;
                if (show)
                {
                    if (!dict.ContainsKey(currentHeader))
                    {
                        dict.Add(currentHeader, new List<FieldInfo>());
                    }
                    dict[currentHeader].Add(f);
                }
            }
            return dict;
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

        private static bool IsBitSet(int b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        private static void SetBit(ref int intValue, int bitNumber, bool value)
        {
            if (value)
            {
                intValue |= 1 << bitNumber;
            }
            else
            {
                intValue &= ~(1 << bitNumber);
            }
        }
    }
}