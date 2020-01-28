using System.Collections.Generic;
using System.ComponentModel;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    public abstract class StackTraceObject : SoArchitectureBaseObject, IStackTraceObject
    {
#if UNITY_EDITOR
        [SerializeField]
        private List<StackTraceEntry> _stackTraces = new List<StackTraceEntry>();
        public List<StackTraceEntry> StackTraces => _stackTraces;

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddStackTrace()
        {
            if (SoArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create();
                _stackTraces.Insert(0, stackTrace);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddStackTrace(object value)
        {
            if (SoArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create(value);
                _stackTraces.Insert(0, stackTrace);
            }
        }
#endif
    }
}