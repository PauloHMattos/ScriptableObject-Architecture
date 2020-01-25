using System;
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
        protected SerializedProperty _showButttons;
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _showGroups = serializedObject.FindProperty("_showGroups");
            _showButttons = serializedObject.FindProperty("_showButttons");
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
        }

        public override void OnInspectorGUI()
        {
            DrawCustomFields();

            DrawDeveloperDescription();

            DrawHelperBox();

            DrawButtonsGroup();

            serializedObject.ApplyModifiedProperties();
        }
        private Dictionary<string, List<FieldInfo>> GetCustomFields()
        {
            var dict = new Dictionary<string, List<FieldInfo>>();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            string currentHeader = "";

            var targetType = target.GetType();
            var fields = targetType.GetFields(bindingFlags);
            var hideBase = targetType.GetCustomAttribute<HideBaseFieldsAttribute>() != null;

            foreach (var field in fields)
            {
                if (!IsDrawable(field, hideBase, out bool isPrivate))
                {
                    continue;
                }

                if (isPrivate)
                {
                    var serializable = field.GetCustomAttribute<SerializableAttribute>(true) != null;
                    if (!serializable)
                    {
                        continue;
                    }
                }

                var header = field.GetCustomAttribute<GroupAttribute>(true);
                if (header != null)
                {
                    currentHeader = header.header;
                    if (header.hidden)
                        continue;
                }

                if (!dict.ContainsKey(currentHeader))
                {
                    dict.Add(currentHeader, new List<FieldInfo>());
                }
                dict[currentHeader].Add(field);
            }
            return dict;
        }

        private bool IsDrawable(MemberInfo memberInfo, bool hideBase, out bool isPrivate)
        {
            if (hideBase)
            {
                if (memberInfo.DeclaringType != target.GetType())
                {
                    isPrivate = false;
                    return false;
                }
            }

            var hide = memberInfo.GetCustomAttribute<HideInInspector>(true) != null;

            isPrivate = false;
            if (memberInfo is FieldInfo field)
            {
                isPrivate = field.IsPrivate;
            }
            else if (memberInfo is MethodInfo method)
            {
                isPrivate = method.IsPrivate;
            }
            return !hide;
        }

        private void DrawButtonsGroup()
        {
            var _headerStyle = EditorStyles.foldout;
            _headerStyle.font = EditorStyles.boldFont;

            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var targetType = target.GetType();
            var methods = targetType.GetMethods(bindingFlags);
            var hideBase = targetType.GetCustomAttribute<HideBaseFieldsAttribute>() != null;

            foreach (var method in methods)
            {
                if (method.GetParameters().Length > 0)
                {
                    continue;
                }

                if (!IsDrawable(method, hideBase, out bool isPrivate))
                {
                    continue;
                }

                var buttonAttribute = method.GetCustomAttribute<ButtonAttribute>(true);
                if (buttonAttribute == null)
                {
                    continue;
                }

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                using (new EditorGUI.IndentLevelScope())
                {
                    _showButttons.boolValue = EditorGUILayout.Foldout(_showButttons.boolValue, new GUIContent("Buttons"), _headerStyle);
                    if (_showButttons.boolValue)
                    {
                        var buttonText = string.IsNullOrEmpty(buttonAttribute.Text) ? method.Name : buttonAttribute.Text;
                        DrawButton(buttonText, method);
                    }
                }
                EditorGUILayout.EndVertical();
            }
        }

        protected virtual void DrawButton(string buttonText, MethodInfo method)
        {
            if (GUILayout.Button(buttonText))
            {
                method.Invoke(target, null);
            }
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
            var groups = GetCustomFields();
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

        protected virtual void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
        {
            foreach (var fieldInfo in fields)
            {
                var property = serializedObject.FindProperty(fieldInfo.Name);
                if (property != null)
                {
                    EditorGUI.BeginDisabledGroup(fieldInfo.GetCustomAttribute<ReadOnlyAttribute>() != null);
                    EditorGUILayout.PropertyField(property);
                    EditorGUI.EndDisabledGroup();

                    bool required = property.propertyType == SerializedPropertyType.ObjectReference && property.objectReferenceValue == null;
                    if (required)
                    {
                        EditorGUILayout.HelpBox("Required field", MessageType.Error);
                    }
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