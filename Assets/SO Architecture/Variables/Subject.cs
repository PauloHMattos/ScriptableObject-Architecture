using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class Subject : SOArchitectureBaseObject, ISubject
    {
        public List<IVariableObserver> Observers { get { return _observers; } }
        private List<IVariableObserver> _observers = new List<IVariableObserver>();

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
        }

        public virtual void AddObserver(IVariableObserver observer)
        {
            if (!_observers.Contains(observer)) _observers.Add(observer);
        }

        public virtual void RemoveObserver(IVariableObserver observer)
        {
            if (_observers.Contains(observer)) _observers.Remove(observer);
        }
    }
}