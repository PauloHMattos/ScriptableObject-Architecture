using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseObserver : DebuggableGameEventListener, IVariableObserver
    {
        public abstract void OnVariableChanged();

        public enum ListnerOption
        {
            OnChanged,
            OnUpdate,
            OnLateUpdate,
            OnFixedUpdate,
            OnTimeInterval
        }

        [SerializeField]
        protected ListnerOption _listnerOption = ListnerOption.OnChanged;
        private float _lastTime;
        [SerializeField]
        private float _delay;

        public float LastTime { get => _lastTime; set => _lastTime = value; }
        public float Delay { get => _delay; set => _delay = value; }

        protected virtual void Update()
        {
            if (_listnerOption.HasFlag(ListnerOption.OnUpdate))
            {
                OnVariableChanged();
            }
            else if (_listnerOption.HasFlag(ListnerOption.OnTimeInterval))
            {
                // TODO
                if (Time.time - _lastTime >= _delay)
                {
                    _lastTime = Time.time;
                    OnVariableChanged();
                }
            }
        }

        protected virtual void LateUpdate()
        {
            if (_listnerOption.HasFlag(ListnerOption.OnLateUpdate))
            {
                OnVariableChanged();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_listnerOption.HasFlag(ListnerOption.OnFixedUpdate))
            {
                OnVariableChanged();
            }
        }


        public abstract void Register();
        public abstract void Unregister();
    }

    public abstract class BaseObserver<TType, TVariable> : BaseObserver
        where TVariable : Subject
    {
        protected override ScriptableObject GameEvent => Variable;

        protected ScriptableObject Variable { get { return _variable; } }

        [SerializeField]
        protected TVariable _variable = default(TVariable);
        [SerializeField]
        private TVariable _previouslyRegisteredVariable = default(TVariable);
        [SerializeField]
        protected TType _debugValue = default(TType);

        protected virtual void OnEnable()
        {
            if (_variable != null)
            {
                if (_listnerOption == ListnerOption.OnChanged)
                {
                    Register();
                }
                OnVariableChanged();
            }
        }


        protected virtual void OnDisable()
        {
            if (_variable != null)
            {
                Unregister();
            }
        }


        public override void Register()
        {
            if (_previouslyRegisteredVariable != null)
            {
                _previouslyRegisteredVariable.RemoveObserver(this);
            }

            _variable.AddObserver(this);
            _previouslyRegisteredVariable = _variable;
        }

        public override void Unregister()
        {
            _variable.RemoveObserver(this);
        }

        protected abstract void RaiseResponse(TType value);
    }

    public abstract class BaseObserver<TType, TVariable, TResponse> : BaseObserver<TType, TVariable>
where TVariable : BaseVariable<TType>
where TResponse : UnityEvent<TType>
    {

        protected override UnityEventBase Response { get { return _response; } }
        
        [SerializeField]
        private TResponse _response = default(TResponse);

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