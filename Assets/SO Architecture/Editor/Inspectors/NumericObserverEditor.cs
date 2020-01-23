using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(NumericObserver<,,>), true)]
    public class NumericObserverEditor : BaseObserverEditor
    {
        protected SerializedProperty _constrain;
        protected SerializedProperty _sample;

        protected SerializedProperty _equals;
        protected SerializedProperty _smaller;
        protected SerializedProperty _bigger;
        protected SerializedProperty _modifierCurve;
        private SerializedProperty _comparationReference;

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
            _modifierCurve = serializedObject.FindProperty("_modifierCurve");
            _sample = serializedObject.FindProperty("_sample");
            _comparationReference = serializedObject.FindProperty("_comparationReference");
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();
            //EditorGUI.indentLevel++;
            //EditorGUILayout.PropertyField(_modifierCurve, new GUIContent("Mod. curve"));
            //EditorGUILayout.PropertyField(_sample, new GUIContent("Evaluate"));
            //EditorGUI.indentLevel--;
        }

        protected override void DrawCustomFields()
        {
            base.DrawCustomFields();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            _constrain.boolValue = EditorGUILayout.BeginToggleGroup("Conditions", _constrain.boolValue);
            DrawConditions();
            EditorGUILayout.EndToggleGroup();

            EditorGUILayout.EndVertical();
        }


        protected virtual void DrawConditions()
        { 
            if (!_constrain.boolValue)
                return;

            if (_comparationReference != null)
            {
                EditorGUILayout.PropertyField(_comparationReference, new GUIContent("Comp. Reference"));
            }

            var equalValue = _equals.boolValue ? 0 : 1;
            var newEquals = GUILayout.Toolbar(equalValue, new[] {"==", "!="});
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