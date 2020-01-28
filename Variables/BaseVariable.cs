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
            get => GetValue();
            set => SetValue(value);
        }
        public virtual T MinClampValue
        {
            get => Clampable ? _minClampedValue : default;
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
            get => Clampable ? _maxClampedValue : default;
            set
            {
                if (Clampable)
                {
                    _maxClampedValue = value;
                }
            }
        }

        public override bool Clampable => false;
        public override bool ReadOnly { get => _readOnly; set => _readOnly = value; }
        public override bool IsClamped { get => _isClamped; set => _isClamped = value; }
        public override System.Type Type => typeof(T);
        protected virtual bool FullReadOnly => false;

        [Group("General", "GameManager Icon")]
        [SerializeField] protected T _value;
        [SerializeField, ShowIf(nameof(FullReadOnly), false)] protected bool _resetWhenStart = true;
        [SerializeField, ShowIf(nameof(_resetWhenStart), true), Indent] protected T _defaultValue;

        [SerializeField, ShowIf(nameof(FullReadOnly), false)] protected bool _readOnly;
        [SerializeField, ShowIf(nameof(_readOnly), true), Indent] protected bool _raiseWarning = true;

        [SerializeField, ShowIf(nameof(FullReadOnly), false)] protected bool _isClamped;
        [SerializeField, ShowIf(nameof(_isClamped), true), Indent, Label("Min Value")] protected T _minClampedValue;
        [SerializeField, ShowIf(nameof(_isClamped), true), Indent, Label("Max Value")] protected T _maxClampedValue;

        public override void Awake()
        {
            base.Awake();
            _readOnly = true;
            _resetWhenStart = false;
            _readOnly = true;
            _resetWhenStart = false;
            _isClamped = false;
            _raiseWarning = false;
        }

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
                _value = ClampValue(_value);
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