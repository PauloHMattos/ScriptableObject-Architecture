using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(AxisVariable), true)]
    public class AxisVariableEditor : ReadOnlyFloatVariableEditor
    {
        private AxisVariable Target { get { return (AxisVariable)target; } }
        protected SerializedProperty _axisName;
        private SerializedProperty _raw;

        protected override void OnEnable()
        {
            base.OnEnable();
            _axisName = serializedObject.FindProperty("_axisName");
            _raw = serializedObject.FindProperty("_raw");
        }
        protected override void DrawValue()
        {
            base.DrawValue();
            EditorGUILayout.PropertyField(_raw);
            EditorGUILayout.PropertyField(_axisName);
        }
    }
}