using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BaseGameEventRaiser), true)]
    public class BaseGameEventRaiserEditor : UnityEditor.Editor
    {
        private BaseGameEventRaiser Target { get { return (BaseGameEventRaiser)target; } }
        protected SerializedProperty _response;


        protected virtual void OnEnable()
        {
            _response = serializedObject.FindProperty("_response");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            base.OnInspectorGUI();
            DrawResponse();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawResponse()
        {
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(_response);
        }
    }
}