using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseObserver : DebuggableGameEventListener, IVariableObserver
    {
        public abstract void OnVariableChanged();

        public enum ListenerOption
        {
            OnChanged,
            OnUpdate,
            OnLateUpdate,
            OnFixedUpdate,
            OnTimeInterval,
            None,
        }

        [Group("General")]
        [SerializeField] protected ListenerOption _listenerOption = ListenerOption.OnChanged;
        [SerializeField] private float _delay;
        private float _lastTime;

        public float Delay { get => _delay; set => _delay = value; }
        
        protected virtual void Update()
        {
            if (_listenerOption == ListenerOption.OnUpdate)
            {
                OnVariableChanged();
            }
            else if (_listenerOption == ListenerOption.OnTimeInterval)
            {
                if (Time.time - _lastTime >= _delay)
                {
                    _lastTime = Time.time;
                    OnVariableChanged();
                }
            }
        }

        protected virtual void LateUpdate()
        {
            if (_listenerOption == ListenerOption.OnLateUpdate)
            {
                OnVariableChanged();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_listenerOption == ListenerOption.OnFixedUpdate)
            {
                OnVariableChanged();
            }
        }
    }

    public abstract class BaseObserver<TType, TVariable> : BaseObserver
        where TVariable : Subject
    {
        protected override ScriptableObject GameEvent { get { return _variable; } }

        [Group("General")]
        [SerializeField] protected bool _raiseOnStart = true;
        [SerializeField] protected TVariable _variable = default;
        [SerializeField] protected TType _debugValue = default;

        protected virtual void OnEnable()
        {
            if (_variable != null)
            {
                if (_listenerOption == ListenerOption.OnChanged)
                {
                    Register();
                }
                if (_raiseOnStart)
                {
                    OnVariableChanged();
                }
            }
            else
            {
                Debug.LogWarning($"{ToString()}: Variable not defined. Disabling component", this);
                this.enabled = false;
            }
        }

        protected virtual void OnDisable()
        {
            if (_variable != null)
            {
                Unregister();
            }
        }

        public void Register()
        {
            _variable.AddObserver(this);
        }

        public void Unregister()
        {
            _variable.RemoveObserver(this);
        }

        protected abstract void RaiseResponse(TType value);
    }

    public abstract class BaseObserver<TType, TVariable, TResponse> : BaseObserver<TType, TVariable>
where TVariable : BaseVariable<TType>, ISubject
where TResponse : UnityEvent<TType>
    {
        protected override UnityEventBase Response { get { return _response; } }
        
        [Group("Response"), SerializeField]
        protected TResponse _response = default;

        public override void OnVariableChanged()
        {
            RaiseResponse(_variable.Value);
        }

        protected override void RaiseResponse(TType value)
        {
            _response.Invoke(value);
        }
    }
}