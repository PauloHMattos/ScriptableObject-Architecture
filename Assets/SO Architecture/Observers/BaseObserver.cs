﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseObserver : DebuggableGameEventListener, IVariableObserver, IGameEventListener
    {
        public abstract void OnVariableChanged();

        public enum ListnerOption
        {
            OnChanged,
            OnUpdate,
            OnLateUpdate,
            OnFixedUpdate,
            OnTimeInterval,
            OnEvent
        }

        [SerializeField]
        protected ListnerOption _listnerOption = ListnerOption.OnChanged;
        [SerializeField]
        protected GameEvent _gameEvent;
        private float _lastTime;
        [SerializeField]
        private float _delay;

        public float LastTime { get => _lastTime; set => _lastTime = value; }
        public float Delay { get => _delay; set => _delay = value; }
        
        protected virtual void OnEnable()
        {
            if (_listnerOption == ListnerOption.OnEvent && _gameEvent != null)
            {
                _gameEvent.AddListener(this);
            }
        }

        protected virtual void OnDisable()
        {
            if (_gameEvent != null)
            {
                _gameEvent.RemoveListener(this);
            }
        }

        protected virtual void Update()
        {
            if (_listnerOption == ListnerOption.OnUpdate)
            {
                OnVariableChanged();
            }
            else if (_listnerOption == ListnerOption.OnTimeInterval)
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
            if (_listnerOption == ListnerOption.OnLateUpdate)
            {
                OnVariableChanged();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (_listnerOption == ListnerOption.OnFixedUpdate)
            {
                OnVariableChanged();
            }
        }


        public abstract void Register();
        public abstract void Unregister();

        public void OnEventRaised()
        {
            OnVariableChanged();
        }
    }

    public abstract class BaseObserver<TType, TVariable> : BaseObserver
        where TVariable : Subject
    {
        protected override ScriptableObject GameEvent => Variable;

        protected ScriptableObject Variable { get { return _variable; } }

        [SerializeField]
        protected bool _raiseOnStart = true;
        [SerializeField]
        protected TVariable _variable = default(TVariable);
        [SerializeField]
        private TVariable _previouslyRegisteredVariable = default(TVariable);
        [SerializeField]
        protected TType _debugValue = default(TType);

        protected virtual void Start()
        {
            if (_raiseOnStart && _variable != null)
            {
                OnVariableChanged();
            }
        }

        protected override void OnEnable()
        {
            if (_variable != null)
            {
                base.OnEnable();
                if (_listnerOption == ListnerOption.OnChanged)
                {
                    Register();
                }
            }
            else
            {
                Debug.LogWarning($"{ToString()}: Variable not defined. Disabling component", this);
                this.enabled = false;
            }
        }


        protected override void OnDisable()
        {
            if (_variable != null)
            {
                base.OnDisable();
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