using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class Subject : SOArchitectureBaseObject, ISubject
    {
        public List<IVariableObserver> Observers { get { return _observers; } }
        private List<IVariableObserver> _observers = new List<IVariableObserver>();

        public List<Action> Actions { get { return _actions; } }
        private List<Action> _actions = new List<Action>();

#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private bool _showObservers = false;
#pragma warning restore
#endif

        public virtual void Raise()
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                _observers[i].OnVariableChanged();
            }
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                _actions[i].Invoke();
            }
        }

        public virtual void AddObserver(IVariableObserver observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
        }

        public virtual void RemoveObserver(IVariableObserver observer)
        {
            if (_observers.Contains(observer)) _observers.Remove(observer);
        }

        public void AddObserver(Action action)
        {
            if (!_actions.Contains(action)) _actions.Add(action);
        }

        public void RemoveObserver(Action action)
        {
            if (_actions.Contains(action)) _actions.Remove(action);
        }

        public void RemoveAllObservers()
        {
            _observers.Clear();
            _actions.Clear();
        }
    }
}