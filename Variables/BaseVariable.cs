using UnityEngine;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseVariable : Subject
    {
        public abstract bool IsClamped { get; }
        public abstract bool Clampable { get; }
        public abstract bool ReadOnly { get; }
        public abstract System.Type Type { get; }
        public abstract object BaseValue { get; set; }

//#if UNITY_EDITOR
//#pragma warning disable 0414
//        [SerializeField]
//        private bool _showGeneral = false;

//#pragma warning restore
//#endif

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
                return _value;
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
                SetValue((T)value);
            }
        }

        [Group("General")]
        [SerializeField]
        protected bool _resetWhenStart = true;
        [SerializeField]
        protected T _defaultValue;
        [SerializeField]
        protected T _value = default(T);
        [SerializeField]
        protected bool _readOnly = false;
        [SerializeField]
        private bool _raiseWarning = true;
        
        [Group("Test")]
        [SerializeField]
        protected bool _isClamped = false;
        [SerializeField]
        protected T _minClampedValue = default(T);
        [SerializeField]
        protected T _maxClampedValue = default(T);

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
            Raise();
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