//using UnityEditor;
//using UnityEngine;

//namespace ScriptableObjectArchitecture.Editor
//{
//    [CanEditMultipleObjects]
//    [CustomEditor(typeof(BaseGameEventRaiser), true)]
//    public class BaseGameEventRaiserEditor : SOArchitectureBaseMonoBehaviourEditor
//    {
//        private BaseGameEventRaiser Target { get { return (BaseGameEventRaiser)target; } }
//        protected SerializedProperty _events;
//        protected SerializedProperty _showGeneral;
//        protected SerializedProperty _showEvents;


//        protected override void OnEnable()
//        {
//            base.OnEnable();
//            _events = serializedObject.FindProperty("_events");
//            _showGeneral = serializedObject.FindProperty("_showGeneral");
//            _showEvents = serializedObject.FindProperty("_showEvents");
//        }

//        protected void DrawEvents()
//        {
//            EditorGUILayout.PropertyField(_events);
//        }

//        protected override void DrawCustomFields()
//        {
//            base.DrawCustomFields();

//            var headerStyle = EditorStyles.foldout;
//            headerStyle.font = EditorStyles.boldFont;
//            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
//            using (new EditorGUI.IndentLevelScope())
//            {
//                _showEvents.boolValue =
//                    EditorGUILayout.Foldout(_showEvents.boolValue, new GUIContent("Events"), headerStyle);
//            }
//            if (_showEvents.boolValue)
//            {
//                DrawEvents();
//            }
//            EditorGUILayout.EndVertical();
//        }

//    }
//}