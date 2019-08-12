using System.Reflection;
using UnityEditor;
using UnityEngine;
using Type = System.Type;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(BaseObserver<,>), true)]
    public class BaseObserverEditor : BaseListnerEditor
    {
        protected MethodInfo _raiseMethod;

        protected override void OnEnable()
        {
            base.OnEnable();
            _raiseMethod = target.GetType().BaseType.GetMethod("OnVariableChanged");
            _event = serializedObject.FindProperty("_variable");
        }
        
        protected override void CallMethod(object value)
        {
            _raiseMethod.Invoke(target, new object[1] { value });
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        }
    }

}