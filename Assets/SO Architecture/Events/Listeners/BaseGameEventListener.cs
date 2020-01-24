using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
#endif

namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventListener<TType, TEvent, TResponse> : DebuggableGameEventListener, IGameEventListener<TType>
where TEvent : GameEventBase<TType>
where TResponse : UnityEvent<TType>
    {
        protected override ScriptableObject GameEvent { get { return _event; } }
        protected override UnityEventBase Response { get { return _response; } }

        [Group("General"), SerializeField]
        protected TEvent _event = default(TEvent);
        [Group("Response"), SerializeField]
        protected TResponse _response = default(TResponse);
        [SerializeField]
        private TEvent _previouslyRegisteredEvent = default(TEvent);

        [Group("Debug")]
        [SerializeField]
        protected TType _debugValue = default(TType);

        public void OnEventRaised(TType value)
        {
            RaiseResponse(value);

            CreateDebugEntry(_response);

            AddStackTrace(value);
        }
        private void RaiseResponse(TType value)
        {
            _response.Invoke(value);
        }
        private void OnEnable()
        {
            if (_event != null)
                Register();
        }
        private void OnDisable()
        {
            if (_event != null)
                _event.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }

            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }

    public abstract class BaseGameEventListener<TEvent, TResponse> : DebuggableGameEventListener, IGameEventListener
        where TEvent : GameEventBase
        where TResponse : UnityEvent
    {
        protected override ScriptableObject GameEvent { get { return _event; } }
        protected override UnityEventBase Response { get { return _response; } }

        [Group("General"), SerializeField]
        protected TEvent _event = default(TEvent);

        [Group("Event"), SerializeField]
        protected TResponse _response = default(TResponse);

        [SerializeField, HideInInspector]
        private TEvent _previouslyRegisteredEvent = default(TEvent);

        public void OnEventRaised()
        {
            RaiseResponse();

            CreateDebugEntry(_response);

            AddStackTrace();
        }
        protected void RaiseResponse()
        {
            _response.Invoke();
        }
        private void OnEnable()
        {
            if (_event != null)
                Register();
        }
        private void OnDisable()
        {
            if (_event != null)
                _event.RemoveListener(this);
        }
        private void Register()
        {
            if (_previouslyRegisteredEvent != null)
            {
                _previouslyRegisteredEvent.RemoveListener(this);
            }
            _event.AddListener(this);
            _previouslyRegisteredEvent = _event;
        }
    }
}