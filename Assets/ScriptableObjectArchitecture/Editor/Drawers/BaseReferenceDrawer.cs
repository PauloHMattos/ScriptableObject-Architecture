using ScriptableObjectArchitecture.References;
using UnityEditor;
using UnityEngine;
using Type = System.Type;

namespace Assets.ScriptableObjectArchitecture.Editor.Drawers
{

    [CustomPropertyDrawer(typeof(BaseReference), true)]
    public sealed class BaseReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private static readonly string[] PopupOptions =
        {
            "Use Constant",
            "Use Variable"
        };

        // Property Names
        private const string VARIABLE_PROPERTY_NAME = "_variable";
        private const string CONSTANT_VALUE_PROPERTY_NAME = "_constantValue";
        private const string USE_CONSTANT_VALUE_PROPERTY_NAME = "_useConstant";

        // Warnings
        private const string COULD_NOT_FIND_VALUE_FIELD_WARNING_FORMAT =
            "Could not find FieldInfo for [{0}] specific property drawer on type [{1}].";

        private Type ValueType => BaseReferenceHelper.GetValueType(fieldInfo);
        private bool SupportsMultiLine => SoArchitectureEditorUtility.SupportsMultiLine(ValueType);

        private SerializedProperty _property;
        private SerializedProperty _useConstant;
        private SerializedProperty _constantValue;
        private SerializedProperty _variable;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Get properties
            this._property = property;
            _useConstant = property.FindPropertyRelative("_useConstant");
            _constantValue = property.FindPropertyRelative("_constantValue");
            _variable = property.FindPropertyRelative("_variable");
                        
            //int oldIndent = ResetIndent();

            var fieldRect = DrawLabel(position, property, label);
            DrawField(position, fieldRect);

            //EndIndent(oldIndent);
            
            property.serializedObject.ApplyModifiedProperties();
        }
        private Rect DrawLabel(Rect position, SerializedProperty property, GUIContent label)
        {
            return EditorGUI.PrefixLabel(position, label);
        }
        private void DrawField(Rect position, Rect fieldRect)
        {
            var buttonRect = GetPopupButtonRect(fieldRect);
            var valueRect = GetValueRect(fieldRect, buttonRect);

            var result = DrawPopupButton(buttonRect, _useConstant.boolValue ? 0 : 1);
            _useConstant.boolValue = result == 0;

            DrawValue(position, valueRect);
        }
        private void DrawValue(Rect position, Rect valueRect)
        {
            if (ShouldDrawMultiLineField())
            {
                valueRect = GetMultiLineFieldRect(position);
                GUI.Box(valueRect, string.Empty);
            }

            if (_useConstant.boolValue)
            {
                DrawGenericPropertyField(valueRect);
            }
            else
            {
                EditorGUI.PropertyField(valueRect, _variable, GUIContent.none);
            }
        }
        private void DrawGenericPropertyField(Rect valueRect)
        {
            if (ValueType != null)
            {
                GenericPropertyDrawer.DrawPropertyDrawer(valueRect, GUIContent.none, ValueType, _constantValue, GUIContent.none);
            }
            else
            {
                Debug.LogWarningFormat(
                    _property.objectReferenceValue,
                    COULD_NOT_FIND_VALUE_FIELD_WARNING_FORMAT,
                    CONSTANT_VALUE_PROPERTY_NAME,
                    ValueType);
            }
        }
        private Rect GetMultiLineFieldRect(Rect position)
        {
            return EditorGUI.IndentedRect(new Rect
            {
                position = new Vector2(position.x, position.y + EditorGUIUtility.singleLineHeight),
                size = new Vector2(position.width, EditorGUI.GetPropertyHeight(_constantValue) + EditorGUIUtility.singleLineHeight)
            });
        }
        private bool ShouldDrawMultiLineField()
        {
            return _useConstant.boolValue && SupportsMultiLine && EditorGUI.GetPropertyHeight(_constantValue) > EditorGUIUtility.singleLineHeight;
        }
        private int ResetIndent()
        {
            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            return indent;
        }
        private void EndIndent(int indent)
        {
            EditorGUI.indentLevel = indent;
        }
        private int DrawPopupButton(Rect rect, int value)
        {
            return EditorGUI.Popup(rect, value, PopupOptions, Styles.PopupStyle);
        }
        private Rect GetValueRect(Rect fieldRect, Rect buttonRect)
        {
            var valueRect = new Rect(fieldRect);
            valueRect.x += buttonRect.width;
            valueRect.width -= buttonRect.width;

            return valueRect;
        }
        private Rect GetPopupButtonRect(Rect fieldrect)
        {
            var buttonRect = new Rect(fieldrect);
            buttonRect.yMin += Styles.PopupStyle.margin.top;
            buttonRect.width = Styles.PopupStyle.fixedWidth + Styles.PopupStyle.margin.right;
            buttonRect.height = Styles.PopupStyle.fixedHeight + Styles.PopupStyle.margin.top;

            return buttonRect;
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (SupportsMultiLine)
            {
                var constantValue = property.FindPropertyRelative(CONSTANT_VALUE_PROPERTY_NAME);
                var useConstant = property.FindPropertyRelative(USE_CONSTANT_VALUE_PROPERTY_NAME);

                var constantPropertyHeight = EditorGUI.GetPropertyHeight(constantValue);
                return !useConstant.boolValue || constantPropertyHeight <= EditorGUIUtility.singleLineHeight
                    ? EditorGUIUtility.singleLineHeight
                    : EditorGUIUtility.singleLineHeight * 2 + constantPropertyHeight;
            }
            else
            {
                return base.GetPropertyHeight(property, label);
            }
        }
        
        static class Styles
        {
            static Styles()
            {
                PopupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"))
                {
                    imagePosition = ImagePosition.ImageOnly,
                };
            }

            public static GUIStyle PopupStyle { get; set; }
        }
    }
}