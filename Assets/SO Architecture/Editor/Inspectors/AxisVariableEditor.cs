using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(AxisVariable), true)]
    public class AxisVariableEditor : ReadOnlyFloatVariableEditor
    {
        private AxisVariable Target { get { return (AxisVariable)target; } }
        private SerializedProperty _axisName;
        private SerializedProperty _raw;

        protected override void OnEnable()
        {
            base.OnEnable();
            _axisName = serializedObject.FindProperty("_axisName");
            _raw = serializedObject.FindProperty("_raw");
        }
        protected override void DrawValue()
        {
            // Call Value.get so the displayed value also gets updated
            var value = Target.Value;
            base.DrawValue();
            EditorGUILayout.PropertyField(_axisName);
            EditorGUILayout.PropertyField(_raw);
        }
    }
}