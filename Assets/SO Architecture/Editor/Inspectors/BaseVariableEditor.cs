using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

namespace ScriptableObjectArchitecture.Editor
{

    [CustomEditor(typeof(BaseVariable<>), true)]
    public class BaseVariableEditor : UnityEditor.Editor
    {
        private BaseVariable Target { get { return (BaseVariable)target; } }
        protected bool IsClampable { get { return Target.Clampable; } }
        protected bool IsClamped { get { return Target.IsClamped; } }

        private SerializedProperty _defaultValueProperty;
        private SerializedProperty _resetProperty;
        protected SerializedProperty _valueProperty;
        private SerializedProperty _developerDescription;
        private SerializedProperty _readOnly;
        private SerializedProperty _raiseWarning;
        private SerializedProperty _isClamped;
        private SerializedProperty _minValueProperty;
        private SerializedProperty _maxValueProperty;
        private AnimBool _raiseWarningAnimation;
        private AnimBool _resetOnStartAnimation;
        private AnimBool _isClampedVariableAnimation;

        private const string READONLY_TOOLTIP = "Should this value be changable during runtime? Will still be editable in the inspector regardless";

        protected virtual void OnEnable()
        {
            _defaultValueProperty = serializedObject.FindProperty("_defaultValue");
            _resetProperty = serializedObject.FindProperty("_resetWhenStart");
            _valueProperty = serializedObject.FindProperty("_value");
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
            _readOnly = serializedObject.FindProperty("_readOnly");
            _raiseWarning = serializedObject.FindProperty("_raiseWarning");
            _isClamped = serializedObject.FindProperty("_isClamped");
            _minValueProperty = serializedObject.FindProperty("_minClampedValue");
            _maxValueProperty = serializedObject.FindProperty("_maxClampedValue");

            _resetOnStartAnimation = new AnimBool(_resetProperty.boolValue);
            _resetOnStartAnimation.valueChanged.AddListener(Repaint);

            _raiseWarningAnimation = new AnimBool(_readOnly.boolValue);
            _raiseWarningAnimation.valueChanged.AddListener(Repaint);

            _isClampedVariableAnimation = new AnimBool(_isClamped.boolValue);
            _isClampedVariableAnimation.valueChanged.AddListener(Repaint);
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            DrawValue();
            DrawReadonlyField();
            EditorGUILayout.EndVertical();

            DrawClampedFields();

            DrawDeveloperDescription();
        }

        protected virtual void DrawValue()
        {
            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);

            string content = "Cannot display value. No PropertyDrawer for (" + Target.Type + ") [" + Target.ToString() + "]";

            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Value"), _valueProperty, new GUIContent(content, content));

                if (scope.changed)
                {
                    // Value changed, raise events
                    serializedObject.ApplyModifiedProperties();
                    if (Application.isPlaying)
                    {
                        Target.Raise();
                    }
                }
            }

            EditorGUILayout.PropertyField(_resetProperty);
            _resetOnStartAnimation.target = _resetProperty.boolValue;

            using (var anim = new EditorGUILayout.FadeGroupScope(_resetOnStartAnimation.faded))
            {
                if (anim.visible)
                {
                    using (new EditorGUI.IndentLevelScope())
                    {
                        GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Default Value"),
                            _defaultValueProperty, new GUIContent(content, content));
                    }
                }
            }
        }
        protected virtual void DrawClampedFields()
        {
            if (!IsClampable)
                return;

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            _isClamped.boolValue = EditorGUILayout.BeginToggleGroup("Clamp Value", _isClamped.boolValue);
            EditorGUILayout.EndToggleGroup();

            _isClampedVariableAnimation.target = _isClamped.boolValue;

            using (var anim = new EditorGUILayout.FadeGroupScope(_isClampedVariableAnimation.faded))
            {
                if (anim.visible)
                {
                    EditorGUILayout.PropertyField(_minValueProperty, new GUIContent("Min Value"));
                    EditorGUILayout.PropertyField(_maxValueProperty, new GUIContent("Max Value"));
                }
            }
            EditorGUILayout.EndVertical();
        }
        protected virtual void DrawReadonlyField()
        {
            EditorGUI.BeginDisabledGroup(_isClamped.boolValue);

            EditorGUILayout.PropertyField(_readOnly, new GUIContent("Read Only", READONLY_TOOLTIP));

            _raiseWarningAnimation.target = _readOnly.boolValue;
            using (var fadeGroup = new EditorGUILayout.FadeGroupScope(_raiseWarningAnimation.faded))
            {
                if (fadeGroup.visible)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_raiseWarning);
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUI.EndDisabledGroup();
        }
        protected void DrawDeveloperDescription()
        {
            EditorGUILayout.PropertyField(_developerDescription);
        }
    }
}