using System;
using System.Collections.Generic;
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Events.Listeners;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Game_Events
{
    public abstract class GameEventBase<T> : GameEventBase, IGameEvent<T>, IStackTraceObject
    {
        private readonly List<IGameEventListener<T>> _typedListeners = new List<IGameEventListener<T>>();
        private readonly List<Action<T>> _typedActions = new List<Action<T>>();

        [SerializeField]
        protected T _debugValue = default(T);

        public List<Action<T>> TypedActions
        {
            get => _typedActions;
        }

        public List<IGameEventListener<T>> TypedListeners
        {
            get => _typedListeners;
        }

        public override int GetActionsCount()
        {
            return base.GetActionsCount() + _typedActions.Count;
        }

        public override int GetListenersCount()
        {
            return base.GetListenersCount() + _typedListeners.Count;
        }

        public void Raise(T value)
        {
            if (!_enabled)
                return;

#if UNITY_EDITOR
            AddStackTrace(value);
#endif
            for (int i = _typedListeners.Count - 1; i >= 0; i--)
                _typedListeners[i].OnEventRaised(value);

            for (int i = _typedActions.Count - 1; i >= 0; i--)
                _typedActions[i](value);

            base.CallListeners();
        }

        public void AddListener(IGameEventListener<T> listener)
        {
            if (!_typedListeners.Contains(listener))
            {
                _typedListeners.Add(listener);
            }
        }

        public void RemoveListener(IGameEventListener<T> listener)
        {
            if (_typedListeners.Contains(listener))
            {
                _typedListeners.Remove(listener);
            }
        }

        public void AddListener(Action<T> action)
        {
            if (!_typedActions.Contains(action))
            {
                _typedActions.Add(action);
            }
        }

        public void RemoveListener(Action<T> action)
        {
            if (_typedActions.Contains(action))
            { _typedActions.Remove(action); }
        }

        public override void RemoveAll()
        {
            _typedActions.Clear();
            _typedListeners.Clear();
            base.RemoveAll();
        }

        public override string ToString()
        {
            return "GameEventBase<" + typeof(T) + ">";
        }

        public override Type GetEventType()
        {
            return typeof(T);
        }
    }

    public abstract class GameEventBase : StackTraceObject, IGameEvent, IStackTraceObject
    {
        [HideInInspector] protected readonly List<IGameEventListener> _listeners = new List<IGameEventListener>();
        [HideInInspector] protected readonly List<System.Action> _actions = new List<System.Action>();

        [Group("General")]
        [SerializeField]
        protected bool _enabled = true;

#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private bool _showListeners = false;
#pragma warning restore
#endif

        public virtual int GetListenersCount()
        {
            return _listeners.Count;
        }

        public virtual int GetActionsCount()
        {
            return _actions.Count;
        }
        public List<Action> Actions
        {
            get => _actions;
        }

        public List<IGameEventListener> Listeners
        {
            get => _listeners;
        }

        public bool Enabled
        {
            get => _enabled;
        }

        public void SetEnabled(bool enabled)
        {
            _enabled = enabled;
        }

        public void Raise()
        {
            if (!_enabled)
                return;

#if UNITY_EDITOR
            AddStackTrace();
#endif
            CallListeners();
        }

        protected virtual void CallListeners()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i]();
            }
        }

        public void AddListener(IGameEventListener listener)
        {
            if (!_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void RemoveListener(IGameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
        public void AddListener(Action action)
        {
            if (!_actions.Contains(action))
            {
                _actions.Add(action);
            }
        }

        public void RemoveListener(Action action)
        {
            if (_actions.Contains(action))
            {
                _actions.Remove(action);
            }
        }

        public virtual void RemoveAll()
        {
            _listeners.Clear();
            _actions.Clear();
        }

        public virtual Type GetEventType()
        {
            return typeof(void);
        }
    }
}