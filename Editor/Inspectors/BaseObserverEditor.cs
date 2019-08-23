using System.Reflection;
using UnityEditor;
using UnityEngine;
using Type = System.Type;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(BaseObserver<,>), true)]
    public class BaseObserverEditor : BaseListnerEditor
    {
        private BaseObserver Target { get { return (BaseObserver)target; } }

        protected MethodInfo _raiseMethod;
        private SerializedProperty _listnerOption;
        private SerializedProperty _gameEvent;
        private SerializedProperty _delay;
        private SerializedProperty _raiseOnStart;

        protected override void OnEnable()
        {
            base.OnEnable();
            _raiseMethod = target.GetType().BaseType.GetMethod("OnVariableChanged");
            _event = serializedObject.FindProperty("_variable");
            _listnerOption = serializedObject.FindProperty("_listnerOption");
            _gameEvent = serializedObject.FindProperty("_gameEvent");
            _delay = serializedObject.FindProperty("_delay");
            _raiseOnStart = serializedObject.FindProperty("_raiseOnStart");
        }
        
        protected override void CallMethod(object value)
        {
            _raiseMethod.Invoke(target, new object[1] { value });
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();
            EditorGUILayout.PropertyField(_raiseOnStart);

            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(_listnerOption);


                if (_listnerOption.enumValueIndex == (int)BaseObserver.ListnerOption.OnEvent)
                {
                    EditorGUI.indentLevel = 1;
                    EditorGUILayout.PropertyField(_gameEvent);
                    EditorGUI.indentLevel = 0;
                }

                if (scope.changed)
                {
                    if (Application.isPlaying)
                    {
                        if (_listnerOption.enumValueIndex == (int) BaseObserver.ListnerOption.OnChanged)
                        {
                            Target.Register();
                        }
                        else
                        {
                            Target.Unregister();
                        }
                    }
                }

                if (_listnerOption.enumValueIndex == (int)BaseObserver.ListnerOption.OnTimeInterval)
                {
                    EditorGUILayout.PropertyField(_delay);
                }

                if (scope.changed)
                {
                    // Value changed, raise events
                    serializedObject.ApplyModifiedProperties();
                }
            }
        }
    }
}