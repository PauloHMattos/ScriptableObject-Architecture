using ScriptableObjectArchitecture.Attributes;
using UnityEngine;

namespace ScriptableObjectArchitecture.Variables
{
    public abstract class BaseVariable : Subject
    {
        public abstract bool IsClamped { get; set; }
        public abstract bool Clampable { get; }
        public abstract bool ReadOnly { get; set; }
        public abstract System.Type Type { get; }

#if UNITY_EDITOR
#pragma warning disable 0414
        [SerializeField]
        private bool _showGeneral = false;
#pragma warning restore
#endif

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
    public abstract class BaseVariable<T> : BaseVariable
    {
        public virtual T Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }
        public virtual T MinClampValue
        {
            get
            {
                return Clampable ? _minClampedValue : default;
            }
            set
            {
                if (Clampable)
                {
                    _minClampedValue = value;
                }
            }
        }
        public virtual T MaxClampValue
        {
            get
            {
                return Clampable ? _maxClampedValue : default;
            }
            set
            {
                if (Clampable)
                {
                    _maxClampedValue = value;
                }
            }
        }

        public override bool Clampable { get { return false; } }
        public override bool ReadOnly { get => _readOnly; set => _readOnly = value; }
        public override bool IsClamped { get => _isClamped; set => _isClamped = value; }
        public override System.Type Type { get { return typeof(T); } }

        [Group("General", "GameManager Icon")]
        [SerializeField, HideInInspector]
        protected bool _resetWhenStart = true;
        [SerializeField, HideInInspector]
        protected T _defaultValue;
        [SerializeField, HideInInspector]
        protected T _value = default;

        [SerializeField, HideInInspector]
        protected bool _readOnly = false;
        [SerializeField, HideInInspector]
        private bool _raiseWarning = true;

        [SerializeField, HideInInspector]
        protected bool _isClamped = false;
        [SerializeField, HideInInspector]
        protected T _minClampedValue = default;
        [SerializeField, HideInInspector]
        protected T _maxClampedValue = default;

        public override void OnEnable()
        {
            base.OnEnable();
            if (_resetWhenStart && !ReadOnly/* && Application.isPlaying*/)
            {
                SetValue(_defaultValue);
            }
            else
            {
                Raise();
            }
        }

        protected virtual T GetValue()
        {
            if (_readOnly)
            {
                return _value;
            }
            if (Clampable && IsClamped)
            {
                SetValue(ClampValue(_value));
            }
            return _value;
        }

        protected virtual T SetValue(T value)
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
            Raise();
            return value;
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

        public override string ToString()
        {
            return _value == null ? "null" : Value.ToString();
        }
        public static implicit operator T(BaseVariable<T> variable)
        {
            return variable.Value;
        }
    }
}