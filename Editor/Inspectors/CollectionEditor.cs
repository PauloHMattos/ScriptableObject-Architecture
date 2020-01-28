using ScriptableObjectArchitecture.Collections;
using ScriptableObjectArchitecture.Editor.Drawers;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(BaseCollection), true)]
    public class CollectionEditor : SoArchitectureBaseObjectEditor
    {
        private BaseCollection Target => (BaseCollection)target;

        private SerializedProperty CollectionItemsProperty => serializedObject.FindProperty(LIST_PROPERTY_NAME);

        private ReorderableList _reorderableList;

        // UI
        private const bool DISABLE_ELEMENTS = false;
        private const bool ELEMENT_DRAGGABLE = true;
        private const bool LIST_DISPLAY_HEADER = true;
        private const bool LIST_DISPLAY_ADD_BUTTON = true;
        private const bool LIST_DISPLAY_REMOVE_BUTTON = true;

        private SerializedProperty _showCollectionItems;

        private GUIContent _titleGuiContent;
        private GUIContent _noPropertyDrawerWarningGuiContent;

        private const string TITLE_FORMAT = "List ({0})";
        private const string NO_PROPERTY_WARNING_FORMAT = "No PropertyDrawer for type [{0}]";

        // Property Names
        private const string LIST_PROPERTY_NAME = "_list";

        protected override void OnEnable()
        {
            base.OnEnable();
            _titleGuiContent = new GUIContent(string.Format(TITLE_FORMAT, Target.Type));
            _noPropertyDrawerWarningGuiContent = new GUIContent(string.Format(NO_PROPERTY_WARNING_FORMAT, Target.Type));

            _reorderableList = new ReorderableList(
                serializedObject,
                CollectionItemsProperty,
                ELEMENT_DRAGGABLE,
                LIST_DISPLAY_HEADER,
                LIST_DISPLAY_ADD_BUTTON,
                LIST_DISPLAY_REMOVE_BUTTON)
            {
                drawHeaderCallback = DrawHeader,
                drawElementCallback = DrawElement,
            };

            _showCollectionItems = serializedObject.FindProperty("_showCollectionItems");
        }
        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            using (new EditorGUI.IndentLevelScope())
            {
                var label = new GUIContent("Items");
                label.image = EditorGUIUtility.IconContent("LightProbes Icon").image;
                _showCollectionItems.boolValue = EditorGUILayout.Foldout(_showCollectionItems.boolValue, label, EditorStyles.foldoutHeader);
            }
            if (_showCollectionItems.boolValue)
            {
                _reorderableList.DoLayoutList();
            }
            EditorGUILayout.EndVertical();

            base.OnInspectorGUI();
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
            }
        }
        private void DrawHeader(Rect rect)
        {
            EditorGUI.LabelField(rect, _titleGuiContent);
        }
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            rect = SoArchitectureEditorUtility.GetReorderableListElementFieldRect(rect);
            var property = CollectionItemsProperty.GetArrayElementAtIndex(index);

            EditorGUI.BeginDisabledGroup(DISABLE_ELEMENTS);

            GenericPropertyDrawer.DrawPropertyDrawer(rect, new GUIContent("Element " + index), Target.Type, property, _noPropertyDrawerWarningGuiContent);

            EditorGUI.EndDisabledGroup();
        }
    }
}