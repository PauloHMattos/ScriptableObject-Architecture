using System;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ScriptableObjectArchitecture.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Field)]
    public class HelpBoxAttribute : MultiPropertyAttribute
    {
        public string Message;
        public HelpBoxType Type;

        public HelpBoxAttribute(string message, HelpBoxType type)
        {
            Message = message;
            Type = type;
        }

#if UNITY_ENGINE
        internal override void OnPostGUI(Rect position, SerializedProperty property)
        {
            EditorGUILayout.HelpBox(Message, (MessageType) Type);
        }
#endif
    }


    public enum HelpBoxType
    {
        //
        // Resumo:
        //     Neutral message.
        None = 0,

        //
        // Resumo:
        //     Info message.
        Info = 1,

        //
        // Resumo:
        //     Warning message.
        Warning = 2,

        //
        // Resumo:
        //     Error message.
        Error = 3
    }
}