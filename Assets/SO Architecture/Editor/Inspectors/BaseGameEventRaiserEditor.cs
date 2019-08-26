using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(BaseGameEventRaiser), true)]
    public class BaseGameEventRaiserEditor : UnityEditor.Editor
    {

        private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };

        private BaseGameEventRaiser Target { get { return (BaseGameEventRaiser)target; } }
        protected SerializedProperty _response;


        protected virtual void OnEnable()
        {
            _response = serializedObject.FindProperty("_response");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.LabelField("General", EditorStyles.boldLabel);

            DrawPropertiesExcluding(serializedObject, _dontIncludeMe);

            EditorGUILayout.EndVertical();

            DrawResponse();
            serializedObject.ApplyModifiedProperties();
        }

        protected void DrawResponse()
        {
            EditorGUILayout.Space();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField("Response", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_response);
            EditorGUILayout.EndVertical();
        }


    }
}