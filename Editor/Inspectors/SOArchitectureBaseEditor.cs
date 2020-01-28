using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ScriptableObjectArchitecture.Attributes;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    internal class FieldGroup
    {
        public GUIContent GuiContent;
        public List<FieldInfo> Fields = new List<FieldInfo>();
        public Color? Color;
    }

    public class SoArchitectureBaseEditor : UnityEditor.Editor
    {
        protected SerializedProperty _showGroups;
        protected SerializedProperty _showButtons;
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _showGroups = serializedObject.FindProperty("_showGroups");
            _showButtons = serializedObject.FindProperty("_showButtons");
            _developerDescription = serializedObject.FindProperty("_developerDescription");
        }

        public override void OnInspectorGUI()
        {
            DrawCustomFields();

            DrawDeveloperDescription();

            DrawHelperBox();

            DrawButtonsGroup();

            serializedObject.ApplyModifiedProperties();
        }
        private Dictionary<string, FieldGroup> GetCustomFields()
        {
            var dict = new Dictionary<string, FieldGroup>();
            var bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var currentHeader = new GUIContent();

            var targetType = target.GetType();
            var fields = targetType.GetFields(bindingFlags);
            var hideBase = targetType.GetCustomAttribute<HideBaseFieldsAttribute>() != null;

            foreach (var field in fields)
            {
                if (!IsDrawable(field, hideBase, out var isPrivate))
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
                    currentHeader = header.Label;
                    if (header.Hidden)
                        continue;
                }

                if (!dict.ContainsKey(currentHeader.text))
                {
                    var group = new FieldGroup();
                    group.GuiContent = currentHeader;

                    var colorAttr = field.GetCustomAttributes<ColorAttribute>().Where(c => c.Background).FirstOrDefault();
                    if (colorAttr != null && colorAttr.Background)
                    {
                        group.Color = colorAttr.Color;
                    }
                    dict.Add(currentHeader.text, group);
                }
                dict[currentHeader.text].Fields.Add(field);
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

                if (!IsDrawable(method, hideBase, out var isPrivate))
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
                    _showButtons.boolValue = EditorGUILayout.Foldout(_showButtons.boolValue, new GUIContent("Buttons"), EditorStyles.foldoutHeader);
                    if (_showButtons.boolValue)
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

            var showFlags = _showGroups.intValue;

            for (var i = 0; i < groups.Count; i++)
            {
                var group = groups.ElementAt(i);
                var show = IsBitSet(showFlags, i);

                var prevColor = GUI.backgroundColor;
                if (group.Value.Color.HasValue)
                {
                    GUI.backgroundColor = group.Value.Color.Value;
                }

                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                using (new EditorGUI.IndentLevelScope())
                {
                    show = EditorGUILayout.Foldout(show, group.Value.GuiContent, EditorStyles.foldoutHeader);
                    if (show)
                    {
                        var fields = group.Value.Fields;
                        DrawCustomFields(fields, serializedObject, group.Key);
                    }
                }
                EditorGUILayout.EndVertical();
                GUI.backgroundColor = prevColor;

                SetBit(ref showFlags, i, show);
            }
            _showGroups.intValue = showFlags;
        }

        protected virtual void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
        {
            foreach (var fieldInfo in fields)
            {
                var property = serializedObject.FindProperty(fieldInfo.Name);
                if (property == null)
                {
                    continue;
                }
                EditorGUILayout.PropertyField(property);
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