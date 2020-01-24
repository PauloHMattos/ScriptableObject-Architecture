﻿using System;
using UnityEngine;
using System.Collections.Generic;

namespace ScriptableObjectArchitecture
{
    public abstract class GameEventBase<T> : GameEventBase, IGameEvent<T>, IStackTraceObject
    {
        private readonly List<IGameEventListener<T>> _typedListeners = new List<IGameEventListener<T>>();
        private readonly List<System.Action<T>> _typedActions = new List<System.Action<T>>();

        [SerializeField]
        protected T _debugValue = default(T);

        public virtual void Raise(T value)
        {
            if (!Enabled)
                return;

            AddStackTrace(value);

            for (int i = _typedListeners.Count - 1; i >= 0; i--)
                _typedListeners[i].OnEventRaised(value);

            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();

            for (int i = _typedActions.Count - 1; i >= 0; i--)
                _typedActions[i](value);

            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i]();
        }

        public override void RaiseAsObject(object value)
        {
            Raise((T)value);
        }

        public void AddListener(IGameEventListener<T> listener)
        {
            if (!_typedListeners.Contains(listener))
                _typedListeners.Add(listener);
        }
        public void RemoveListener(IGameEventListener<T> listener)
        {
            if (_typedListeners.Contains(listener))
                _typedListeners.Remove(listener);
        }
        public void AddListener(System.Action<T> action)
        {
            if (!_typedActions.Contains(action))
                _typedActions.Add(action);
        }
        public void RemoveListener(System.Action<T> action)
        {
            if (_typedActions.Contains(action))
                _typedActions.Remove(action);
        }
        public override string ToString()
        {
            return "GameEventBase<" + typeof(T) + ">";
        }


        public override System.Type GetEventType()
        {
            return typeof(T);
        }
    }

    public abstract class GameEventBase : SOArchitectureBaseObject, IGameEvent, IStackTraceObject
    {
        [HideInInspector] protected readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        [HideInInspector] protected readonly List<System.Action> _actions = new List<System.Action>();

        [Group("General")]
        [SerializeField]
        protected bool _enabled = true;

        public virtual bool Enabled => _enabled;
        public List<StackTraceEntry> StackTraces { get { return _stackTraces; } }

#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private bool _showListners = false;
        //[SerializeField]
        //private bool _showStackTrace = false;
#pragma warning restore
#endif

        public List<IGameEventListener> Listeners
        {
            get => _listeners;
        }

        private List<StackTraceEntry> _stackTraces = new List<StackTraceEntry>();

        public void AddStackTrace()
        {
#if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create();
                _stackTraces.Insert(0, stackTrace);
                //Debug.Log(stackTrace);
            }
#endif
        }
        public void AddStackTrace(object value)
        {
#if UNITY_EDITOR
            if (SOArchitecturePreferences.IsDebugEnabled)
            {
                var stackTrace = StackTraceEntry.Create(value);
                _stackTraces.Insert(0, stackTrace);
                //Debug.Log(stackTrace);
            }
#endif
        }

        public void Raise()
        {
            if (!Enabled)
                return;

            AddStackTrace();

            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();

            for (int i = _actions.Count - 1; i >= 0; i--)
                _actions[i]();
        }

        public virtual void RaiseAsObject(object value)
        {
            Raise();
        }

        public void AddListener(IGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void RemoveListener(IGameEventListener listener)
        {
            if (_listeners.Contains(listener))
                _listeners.Remove(listener);
        }
        public void AddListener(System.Action action)
        {
            if (!_actions.Contains(action))
                _actions.Add(action);
        }
        public void RemoveListener(System.Action action)
        {
            if (_actions.Contains(action))
                _actions.Remove(action);
        }
        public void RemoveAll()
        {
            _listeners.RemoveRange(0, _listeners.Count);
            _actions.RemoveRange(0, _listeners.Count);
        }

        public virtual System.Type GetEventType()
        {
            return null;
        }
    } 
}