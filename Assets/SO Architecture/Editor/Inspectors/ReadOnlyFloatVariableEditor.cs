using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{

    [CustomEditor(typeof(ReadOnlyFloatVariable), true)]
    public class ReadOnlyFloatVariableEditor : BaseVariableEditor
    {

        private ReadOnlyFloatVariable Target { get { return (ReadOnlyFloatVariable)target; } }

        protected override void DrawValue()
        {
            string content = "Cannot display value. No PropertyDrawer for (" + Target.Type + ") [" + Target.ToString() + "]";

            using (var scope = new EditorGUI.DisabledGroupScope(true))
            {
                GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Value"), _valueProperty, new GUIContent(content, content));
            }
        }

        protected override void DrawReadonlyField()
        {

        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("ReadOnly variables does not trigger changed events", MessageType.Info);
        }
    }

}