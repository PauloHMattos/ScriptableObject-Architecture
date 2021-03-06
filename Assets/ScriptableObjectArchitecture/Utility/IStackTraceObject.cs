﻿using System.Collections.Generic;
using ScriptableObjectArchitecture.Events.Game_Events;

namespace ScriptableObjectArchitecture.Utility
{
    public interface IStackTraceObject
    {
#if UNITY_EDITOR
        List<StackTraceEntry> StackTraces { get; }

        void AddStackTrace();
        void AddStackTrace(object value);
#endif
    } 
}