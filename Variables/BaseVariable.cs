using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class Subject : BaseVariable
    {
        public List<IVariableObserver> Observers { get { return _observers; } }
        private List<IVariableObserver> _observers = new List<IVariableObserver>();

        public override void Raise()
        {
            ClampValue();
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


    public abstract class BaseVariable : SOArchitectureBaseObject
    {
        public abstract bool IsClamped { get; }
        public abstract bool Clampable { get; }
        public abstract bool ReadOnly { get; }
        public abstract System.Type Type { get; }
        public abstract object BaseValue { get; set; }

        public abstract void Raise();

        protected abstract void ClampValue();

        public virtual void Awake()
        {
        }

        public virtual void OnEnable()
        {
        }

        public virtual void OnDisable()
        {
        }
    }
    public abstract class BaseVariable<T> : Subject
    {
        public virtual T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ClampValue();
                Raise();
            }
        }
        public virtual T MinClampValue
        {
            get
            {
                if(Clampable)
                {
                    return _minClampedValue;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public virtual T MaxClampValue
        {
            get
            {
                if(Clampable)
                {
                    return _maxClampedValue;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public override bool Clampable { get { return false; } }
        public override bool ReadOnly { get { return _readOnly; } }
        public override bool IsClamped { get { return _isClamped; } }
        public override System.Type Type { get { return typeof(T); } }
        public override object BaseValue
        {
            get
            {
                return Value;
            }
            set
            {
                Value = (T)value;
            }
        }

        [SerializeField]
        private bool _resetWhenStart = true;
        [SerializeField]
        protected T _defaultValue;
        [SerializeField]
        protected T _value = default(T);
        [SerializeField]
        private bool _readOnly = false;
        [SerializeField]
        private bool _raiseWarning = true;
        [SerializeField]
        protected bool _isClamped = false;
        [SerializeField]
        protected T _minClampedValue = default(T);
        [SerializeField]
        protected T _maxClampedValue = default(T);

        public override void OnEnable()
        {
            base.OnEnable();
            if (_resetWhenStart/* && Application.isPlaying*/)
            {
                Value = _defaultValue;
            }
        }

        public virtual T SetValue(T value)
        {
            if (_readOnly)
            {
                RaiseReadonlyWarning();
                return _value;
            }
            else if (Clampable && IsClamped)
            {
                value = ClampValue(value);
            }

            _value = value;
            return value;
        }

        public virtual T SetValue(BaseVariable<T> value)
        {
            return SetValue(value.Value);
        }
        protected virtual T ClampValue(T value)
        {
            return value;
        }
        private void RaiseReadonlyWarning()
        {
            if (!_readOnly || !_raiseWarning)
                return;

            Debug.LogWarning("Tried to set value on " + name + ", but value is readonly!", this);
        }

        protected override void ClampValue()
        {
            _value = SetValue(_value);
        }

        public override string ToString()
        {
            return _value == null ? "null" : _value.ToString();
        }
        public static implicit operator T(BaseVariable<T> variable)
        {
            return variable.Value;
        }
    } 
}