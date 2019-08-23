using UnityEditor;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(NumericObserver<,,,>), true)]
    public class NumericObserverEditor2 : NumericObserverEditor
    {
        private SerializedProperty _comparationReference;

        protected override void OnEnable()
        {
            base.OnEnable();
            _comparationReference = serializedObject.FindProperty("_comparationReference");
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();
        }

        protected override void DrawConditions()
        {
            if (!_constrain.boolValue)
                return;

            EditorGUILayout.PropertyField(_comparationReference);
            base.DrawConditions();
        }
    }

    [CustomEditor(typeof(NumericObserver<,,>), true)]
    public class NumericObserverEditor : BaseObserverEditor
    {
        protected SerializedProperty _constrain;
        protected SerializedProperty _sample;

        protected SerializedProperty _equals;
        protected SerializedProperty _smaller;
        protected SerializedProperty _bigger;
        protected SerializedProperty _modifierCurve;

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
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();
            //EditorGUILayout.ObjectField(_event, new GUIContent("Variable", "Variable which will trigger the response"));
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_modifierCurve);
            EditorGUILayout.PropertyField(_sample);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_constrain, new GUIContent("Apply conditions?"));
            DrawConditions();
        }

        protected virtual void DrawConditions()
        { 
            if (!_constrain.boolValue)
                return;

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