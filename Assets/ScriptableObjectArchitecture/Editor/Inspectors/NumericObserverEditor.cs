using System.Collections.Generic;
using System.Reflection;
using ScriptableObjectArchitecture.Observers;
using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(NumericObserver<,,>), true)]
    public class NumericObserverEditor : BaseObserverEditor
    {
        protected SerializedProperty _constrain;

        protected SerializedProperty _equals;
        protected SerializedProperty _smaller;
        protected SerializedProperty _bigger;
        private SerializedProperty _comparisonReference;

        private static readonly string[] DifferentLabels =
        {
            "<",
            "!=",
            ">",
        };
        private static readonly string[] EqualLabels =
        {
            "<=",
            "==",
            ">=",
        };

        protected override void OnEnable()
        {
            base.OnEnable();
            _constrain = serializedObject.FindProperty("_constrain");
            _equals = serializedObject.FindProperty("_equals");
            _smaller = serializedObject.FindProperty("_smaller");
            _bigger = serializedObject.FindProperty("_bigger");
            _comparisonReference = serializedObject.FindProperty("_comparisonReference");
        }

        protected override void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
        {
            if (groupName.Equals("Conditions"))
            {
                _constrain.boolValue = EditorGUILayout.Toggle("Enabled", _constrain.boolValue);
                if (_constrain.boolValue)
                {
                    DrawConditions();
                }
                return;
            }
            base.DrawCustomFields(fields, serializedObject, groupName);
        }


        protected virtual void DrawConditions()
        {
            if (_comparisonReference != null)
            {
                EditorGUILayout.PropertyField(_comparisonReference, new GUIContent("Comp. Reference"));
            }

            var equalValue = _equals.boolValue ? 0 : 1;
            var newEquals = GUILayout.Toolbar(equalValue, new[] { "==", "!=" });
            _equals.boolValue = newEquals == 0;

            if (equalValue != newEquals)
            {
                _smaller.boolValue = false;
                _bigger.boolValue = false;
            }

            var labels = _equals.boolValue ? EqualLabels : DifferentLabels;

            var selected = -1;
            if ((!_smaller.boolValue && !_bigger.boolValue) || (_smaller.boolValue && _bigger.boolValue))
            {
                selected = 1;
            }
            else if (_smaller.boolValue)
            {
                selected = 0;
            }
            else if (_bigger.boolValue)
            {
                selected = 2;
            }

            selected = GUILayout.Toolbar(selected, labels);

            switch (selected)
            {
                case 0: // < ou <=
                    _smaller.boolValue = true;
                    _bigger.boolValue = false;
                    break;

                case 1: // != ou ==
                    _smaller.boolValue = false;
                    _bigger.boolValue = false;
                    break;

                case 2: // > ou >=
                    _smaller.boolValue = false;
                    _bigger.boolValue = true;
                    break;
            }
        }
    }
}