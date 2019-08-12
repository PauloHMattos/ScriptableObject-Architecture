using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(BaseGameEventListener<,>), true)]
    public class GameEventListenerEditor : BaseGameEventListenerEditor
    {
        private MethodInfo _raiseMethod;

        protected override void OnEnable()
        {
            base.OnEnable();

            _raiseMethod = target.GetType().BaseType.GetMethod("OnEventRaised");
        }

        protected override object GetDebugValue(SerializedProperty property)
        {
            return null;
        }

        protected override void CallMethod(object value)
        {
            _raiseMethod.Invoke(target, null);
        }
    } 
}