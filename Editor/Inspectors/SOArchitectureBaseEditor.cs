using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    public class SOArchitectureBaseEditor : UnityEditor.Editor
    {
        protected SerializedProperty _showGroups;
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _showGroups = serializedObject.FindProperty("_showGroups");
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
        }

        public override void OnInspectorGUI()
        {
            DrawCustomFields();

            DrawDeveloperDescription();

            DrawHelperBox();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawHelperBox()
        {
            var helperBox = target.GetType().GetCustomAttribute<HelpBoxAttribute>();
            if (helperBox == null)
                return;

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(helperBox.Message, (MessageType)helperBox.Type);
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
                        DrawCustomFields(fields, serializedObject, group.Key);
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

            var targetType = target.GetType();
            var fields = targetType.GetFields(bindingFlags);
            var hideBase = targetType.GetCustomAttribute<HideBaseFieldsAttribute>() != null;

            foreach (var f in fields)
            {
                if (hideBase)
                {
                    if (f.DeclaringType != targetType)
                        continue;
                }

                var hide = f.GetCustomAttribute<HideInInspector>(true) != null;
                var seralizeField = f.GetCustomAttribute<SerializeField>(true) != null;
                var header = f.GetCustomAttribute<GroupAttribute>(true);

                if (header != null)
                {
                    currentHeader = header.header;
                    if (header.hidden)
                        continue;
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

        protected virtual void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
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