using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(SOArchitectureBaseObject), true)]
    public class SOArchitectureBaseObjectEditor : UnityEditor.Editor
    {
        private SerializedProperty _developerDescription;

        protected virtual void OnEnable()
        {
            _developerDescription = serializedObject.FindProperty("DeveloperDescription");
        }

        public override void OnInspectorGUI()
        {
            DrawDeveloperDescription();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawDeveloperDescription()
        {
            EditorGUILayout.PropertyField(_developerDescription);
        }
    }
}