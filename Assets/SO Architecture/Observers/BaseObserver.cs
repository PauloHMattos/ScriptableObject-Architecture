using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseObserver<TType, TVariable> : DebuggableGameEventListener, IVariableObserver
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


        private void OnEnable()
        {
            if (_variable != null)
            {
                Register();
                OnVariableChanged();
            }
        }


        private void OnDisable()
        {
            if (_variable != null)
            {
                Unregister();
            }
        }

        public void Log(TType value)
        {
            Debug.Log($"[{gameObject.name} - {Variable.name}]: {value}");
        }

        protected virtual void Register()
        {
            if (_previouslyRegisteredVariable != null)
            {
                _previouslyRegisteredVariable.RemoveObserver(this);
            }

            _variable.AddObserver(this);
            _previouslyRegisteredVariable = _variable;
        }

        protected virtual void Unregister()
        {
            _variable.RemoveObserver(this);
        }

        protected abstract void RaiseResponse(TType value);
        public abstract void OnVariableChanged();
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