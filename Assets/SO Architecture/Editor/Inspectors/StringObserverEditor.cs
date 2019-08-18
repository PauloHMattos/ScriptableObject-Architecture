using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(StringObserver), true)]
    public class StringObserverEditor : BaseObserverEditor
    {
        private StringObserver Target { get { return (StringObserver)target; } }
        private SerializedProperty _format;

        protected override void OnEnable()
        {
            base.OnEnable();
            _format = serializedObject.FindProperty("_format");
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();
            EditorGUILayout.PropertyField(_format);
        }
    }
}