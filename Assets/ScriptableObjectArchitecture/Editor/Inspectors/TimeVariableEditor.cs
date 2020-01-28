using ScriptableObjectArchitecture.Variables;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor.Inspectors
{
    [CustomEditor(typeof(TimeVariable), true)]
    public class TimeVariableEditor : SubjectEditor
    {
        private TimeVariable Target => (TimeVariable)target;

        public override void OnInspectorGUI()
        {
            _ = Target.Value;
            base.OnInspectorGUI();
        }
    }
}