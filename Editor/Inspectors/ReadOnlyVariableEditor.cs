using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{

    [CustomEditor(typeof(ReadOnlyVariable<>), true)]
    public class ReadOnlyVariableEditor : BaseVariableEditor
    {

        private BaseVariable Target { get { return (BaseVariable)target; } }
        
        protected override void DrawValue()
        {
            string content = "Cannot display value. No PropertyDrawer for (" + Target.Type + ") [" + Target.ToString() + "]";

            using (var scope = new EditorGUI.DisabledGroupScope(true))
            {
                GenericPropertyDrawer.DrawPropertyDrawerLayout(Target.Type, new GUIContent("Value"), _valueProperty, new GUIContent(content, content));
            }

            var obj = Target.BaseValue;
        }

        protected override void DrawReadonlyField()
        {

        }
    }
}