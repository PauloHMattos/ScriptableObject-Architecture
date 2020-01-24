using UnityEngine;
using System.Collections.Generic;
using System.ComponentModel;

namespace ScriptableObjectArchitecture
{
    public abstract class StackTraceObject : SOArchitectureBaseObject, IStackTraceObject
    {
#if UNITY_EDITOR
        [SerializeField]
        private List<StackTraceEntry> _stackTraces = new List<StackTraceEntry>();
        public List<StackTraceEntry> StackTraces { get { return _stackTraces; } }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddStackTrace()
        {
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create();
                _stackTraces.Insert(0, stackTrace);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void AddStackTrace(object value)
        {
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create(value);
                _stackTraces.Insert(0, stackTrace);
            }
        }
#endif
    }
}