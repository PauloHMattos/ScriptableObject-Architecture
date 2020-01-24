//using System.Linq;
//using System.Reflection;
//using UnityEditor;
//using UnityEngine;

//namespace ScriptableObjectArchitecture.Editor
//{
//    [CustomEditor(typeof(BaseObserver<,>), true)]
//    public class BaseObserverEditor : BaseListenerEditor
//    {
//        private BaseObserver Target { get { return (BaseObserver)target; } }

//        protected MethodInfo _raiseMethod;
//        private SerializedProperty _listenerOption;
//        private SerializedProperty _gameEvent;
//        private SerializedProperty _delay;
//        private SerializedProperty _raiseOnStart;

//        protected override void OnEnable()
//        {
//            base.OnEnable();
//            _raiseMethod = target.GetType().BaseType.GetMethod("OnVariableChanged");
//            _event = serializedObject.FindProperty("_variable");
//            _listenerOption = serializedObject.FindProperty("_listenerOption");
//            _gameEvent = serializedObject.FindProperty("_gameEvent");
//            _delay = serializedObject.FindProperty("_delay");
//            _raiseOnStart = serializedObject.FindProperty("_raiseOnStart");
//        }

//        protected override void CallMethod(object value)
//        {
//            _raiseMethod.Invoke(target, new object[1] { value });
//        }

//        protected override void DrawGameEventField()
//        {
//            base.DrawGameEventField();
//            EditorGUILayout.PropertyField(_raiseOnStart);

//            using (var scope = new EditorGUI.ChangeCheckScope())
//            {
//                EditorGUILayout.PropertyField(_listenerOption);


//                if (_listenerOption.enumValueIndex == (int)BaseObserver.ListenerOption.OnEvent)
//                {
//                    EditorGUI.indentLevel++;
//                    EditorGUILayout.PropertyField(_gameEvent);
//                    EditorGUI.indentLevel--;
//                }

//                if (scope.changed)
//                {
//                    if (Application.isPlaying)
//                    {
//                        if (_listenerOption.enumValueIndex == (int)BaseObserver.ListenerOption.OnChanged)
//                        {
//                            Target.Register();
//                        }
//                        else
//                        {
//                            Target.Unregister();
//                        }
//                    }
//                }

//                if (_listenerOption.enumValueIndex == (int)BaseObserver.ListenerOption.OnTimeInterval)
//                {
//                    EditorGUILayout.PropertyField(_delay);
//                }

//                if (scope.changed)
//                {
//                    // Value changed, raise events
//                    serializedObject.ApplyModifiedProperties();
//                }
//            }
//        }
//    }
//}