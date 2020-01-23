using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using Type = System.Type;

namespace ScriptableObjectArchitecture.Editor
{
    [CustomEditor(typeof(GameEventBase<>), true)]
    public class TypedGameEventEditor : BaseGameEventEditor
    {
        private MethodInfo _raiseMethod;

        private GameEventBase Event { get { return (GameEventBase)target; } }

        protected override void OnEnable()
        {
            base.OnEnable();

            _raiseMethod = target.GetType().BaseType.GetMethod(nameof(GameEventBase.Raise), BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
        }

        protected virtual void DrawDebugValue(SerializedProperty property)
        {
            EditorGUILayout.PropertyField(property);
        }

        protected override void DrawRaiseButton()
        {
            SerializedProperty property = serializedObject.FindProperty("_debugValue");
            DrawDebugValue(property);


            if (GUILayout.Button("Raise"))
            {
                CallMethod(GetDebugValue(property));
            }
        }
        protected object GetDebugValue(SerializedProperty property)
        {
            Type targetType = property.serializedObject.targetObject.GetType();
            FieldInfo targetField = targetType.GetField("_debugValue", BindingFlags.Instance | BindingFlags.NonPublic);
            return targetField.GetValue(property.serializedObject.targetObject);
        }
        protected virtual void CallMethod(object value)
        {
            _raiseMethod.Invoke(target, new object[1] { value });
        }

        //protected override void DrawListners()
        //{
        //    var count = Event.Count + Event.Actions.Count;
        //    var listeners = Event.Listeners;

        //    EditorGUILayout.LabelField("Typed Listeners");
        //    EditorGUILayout.IntField("Size", count);
        //    for (int i = 0; i < Event.Listeners.Count; i++)
        //    {
        //        var listener = listeners[i];
        //        string label = $"Element {i}";
        //        if (listener is Object)
        //        {
        //            EditorGUILayout.ObjectField(label, listener as Object, listener.GetType(), true);
        //        }
        //        else
        //        {
        //            EditorGUILayout.TextField(label, $"{listener.ToString()}");
        //        }
        //    }

        //    var actions = Event.Actions;
        //    for (int i = 0; i < Event.Actions.Count; i++)
        //    {
        //        var action = actions[i];
        //        string label = $"Element {Event.Listeners.Count + i}";
        //        string value = $"{action.Method.ReflectedType.Name}.{action.Method.Name}";
        //        EditorGUILayout.TextField(label, value);
        //    }

        //    EditorGUILayout.LabelField("Untyped Listeners");
        //    base.DrawListners();
        //}
    }
}