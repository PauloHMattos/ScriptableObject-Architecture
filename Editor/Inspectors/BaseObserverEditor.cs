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
        private SerializedProperty _delay;

        protected override void OnEnable()
        {
            base.OnEnable();
            _raiseMethod = target.GetType().BaseType.GetMethod("OnVariableChanged");
            _event = serializedObject.FindProperty("_variable");
            _listnerOption = serializedObject.FindProperty("_listnerOption");
            _delay = serializedObject.FindProperty("_delay");
        }
        
        protected override void CallMethod(object value)
        {
            _raiseMethod.Invoke(target, new object[1] { value });
        }

        protected override void DrawGameEventField()
        {
            base.DrawGameEventField();

            using (var scope = new EditorGUI.ChangeCheckScope())
            {
                EditorGUILayout.PropertyField(_listnerOption);

                if (scope.changed)
                {
                    if (_listnerOption.enumValueIndex == (int)BaseObserver.ListnerOption.OnChanged)
                    {
                        Target.Register();
                    }
                    else
                    {
                        Target.Unregister();
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