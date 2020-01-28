//using System.Collections.Generic;
//using System.Reflection;
//using ScriptableObjectArchitecture.Editor.Drawers;
//using ScriptableObjectArchitecture.Variables;
//using UnityEditor;
//using UnityEditor.AnimatedValues;
//using UnityEngine;

//namespace ScriptableObjectArchitecture.Editor.Inspectors
//{

//    //[CustomEditor(typeof(BaseVariable<>), true)]
//    public class BaseVariableEditor : SubjectEditor
//    {
//        private BaseVariable Target => (BaseVariable)target;
//        protected bool IsClampable => Target.Clampable;
//        protected bool IsClamped => Target.IsClamped;

//        private SerializedProperty _defaultValueProperty;
//        private SerializedProperty _resetProperty;
//        protected SerializedProperty _valueProperty;
//        private SerializedProperty _readOnly;
//        private SerializedProperty _raiseWarning;
//        private SerializedProperty _isClamped;
//        private SerializedProperty _minValueProperty;
//        private SerializedProperty _maxValueProperty;

//        private AnimBool _raiseWarningAnimation;
//        private AnimBool _resetOnStartAnimation;
//        private AnimBool _isClampedVariableAnimation;

//        private SerializedProperty _showGeneral;
//        private const string READONLY_TOOLTIP = "Should this value be changable during runtime? Will still be editable in the inspector regardless";

//        protected override void OnEnable()
//        {
//            base.OnEnable();
//            _defaultValueProperty = serializedObject.FindProperty("_defaultValue");
//            _resetProperty = serializedObject.FindProperty("_resetWhenStart");
//            _valueProperty = serializedObject.FindProperty("_value");
//            _readOnly = serializedObject.FindProperty("_readOnly");
//            _raiseWarning = serializedObject.FindProperty("_raiseWarning");
//            _isClamped = serializedObject.FindProperty("_isClamped");
//            _minValueProperty = serializedObject.FindProperty("_minClampedValue");
//            _maxValueProperty = serializedObject.FindProperty("_maxClampedValue");

//            _resetOnStartAnimation = new AnimBool(_resetProperty.boolValue);
//            _resetOnStartAnimation.valueChanged.AddListener(Repaint);

//            _raiseWarningAnimation = new AnimBool(_readOnly.boolValue);
//            _raiseWarningAnimation.valueChanged.AddListener(Repaint);

//            _isClampedVariableAnimation = new AnimBool(_isClamped.boolValue);
//            _isClampedVariableAnimation.valueChanged.AddListener(Repaint);

//            _showGeneral = serializedObject.FindProperty("_showGeneral");
//        }

//        protected override void DrawCustomFields(List<FieldInfo> fields, SerializedObject serializedObject, string groupName)
//        {
//            if (groupName.Equals("General"))
//            {
//                DrawGeneral(List<FieldInfo> fields, SerializedObject serializedObject, string groupName);
//            }
//            base.DrawCustomFields(fields, serializedObject, groupName);
//        }

//        protected virtual void DrawGeneral()
//        {
//            DrawValue();
//            DrawReadonlyField();
//            DrawClampedFields(_readOnly.boolValue);
//        }

//        protected virtual void DrawValue()
//        {
//            var content = "Cannot display value. No PropertyDrawer for (" + Target.Type + ") [" + Target.ToString() + "]";

//            using (var scope = new EditorGUI.ChangeCheckScope())
//            {
//                GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Value"), _valueProperty, new GUIContent(content, content));

//                if (scope.changed)
//                {
//                    // Value changed, raise events
//                    serializedObject.ApplyModifiedProperties();
//                    if (Application.isPlaying)
//                    {
//                        Target.Raise();
//                    }
//                }
//            }

//            _resetProperty.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Reset on Start"), _resetProperty.boolValue);
//            EditorGUILayout.EndToggleGroup();
//            _resetOnStartAnimation.target = _resetProperty.boolValue;

//            using (var anim = new EditorGUILayout.FadeGroupScope(_resetOnStartAnimation.faded))
//            {
//                if (anim.visible)
//                {
//                    using (new EditorGUI.IndentLevelScope())
//                    {
//                        GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Default Value"),
//                            _defaultValueProperty, new GUIContent(content, content));
//                    }
//                }
//            }
//        }

//        protected virtual void DrawClampedFields(bool disableWithReadOnly)
//        {
//            if (!IsClampable)
//                return;

//            EditorGUI.BeginDisabledGroup(disableWithReadOnly);
//            _isClamped.boolValue = EditorGUILayout.BeginToggleGroup("Clamp Value", _isClamped.boolValue);
//            EditorGUILayout.EndToggleGroup();
//            _isClampedVariableAnimation.target = _isClamped.boolValue;

//            using (var anim = new EditorGUILayout.FadeGroupScope(_isClampedVariableAnimation.faded))
//            {
//                if (anim.visible)
//                {
//                    using (new EditorGUI.IndentLevelScope())
//                    {
//                        EditorGUILayout.PropertyField(_minValueProperty, new GUIContent("Min Value"));
//                        EditorGUILayout.PropertyField(_maxValueProperty, new GUIContent("Max Value"));
//                    }
//                }
//            }
//            EditorGUI.EndDisabledGroup();
//        }

//        protected virtual void DrawReadonlyField()
//        {
//            _readOnly.boolValue = EditorGUILayout.BeginToggleGroup(new GUIContent("Read Only", READONLY_TOOLTIP), _readOnly.boolValue);
//            EditorGUILayout.EndToggleGroup();
//            _raiseWarningAnimation.target = _readOnly.boolValue;

//            using (var anim = new EditorGUILayout.FadeGroupScope(_raiseWarningAnimation.faded))
//            {
//                if (anim.visible)
//                {
//                    using (new EditorGUI.IndentLevelScope())
//                    {
//                        EditorGUILayout.PropertyField(_raiseWarning);
//                    }
//                    _isClamped.boolValue = false;
//                }
//            }
//        }

//    }
//}