using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(Axis2DVariable), true)]
    public class Axis2DVariableEditor : AxisVariableEditor
    {
        private Axis2DVariable Target { get { return (Axis2DVariable)target; } }

        private SerializedProperty _yAxisName;

        protected override void OnEnable()
        {
            base.OnEnable();
            _axisName = serializedObject.FindProperty("_xAxisName");
            _yAxisName = serializedObject.FindProperty("_yAxisName");
        }

        protected override void DrawValue()
        {
            base.DrawValue();
            EditorGUILayout.PropertyField(_yAxisName);
        }
    }
}