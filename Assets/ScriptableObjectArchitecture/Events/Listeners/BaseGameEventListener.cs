using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.Events.GameEvents;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
#endif

namespace ScriptableObjectArchitecture.Events.Listeners
{
    public abstract class BaseGameEventListener<TType, TEvent, TResponse> : DebuggableGameEventListener, IGameEventListener<TType>
where TEvent : GameEventBase<TType>
where TResponse : UnityEvent<TType>
    {
        protected override ScriptableObject GameEvent => _event;
        protected override UnityEventBase Response => _response;

        [Group("General", "GameManager Icon"), SerializeField]
        protected TEvent _event = default;
        [Group("Response", "d_CollabMoved Icon"), SerializeField]
        protected TResponse _response = default;
        [SerializeField]
        private TEvent _previouslyRegisteredEvent = default;

        [Group("Debug", "Search Icon")]
        [SerializeField]
        protected TType _debugValue = default;

        public void OnEventRaised(TType value)
        {
            RaiseResponse(value);
#if UNITY_EDITOR
            CreateDebugEntry(_response);
#endif
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
        protected override ScriptableObject GameEvent => _event;
        protected override UnityEventBase Response => _response;

        [Group("General", "GameManager Icon"), SerializeField]
        protected TEvent _event = default;

        [Group("Response", "d_CollabMoved Icon"), SerializeField]
        protected TResponse _response = default;

        [SerializeField, HideInInspector]
        private TEvent _previouslyRegisteredEvent = default;

        public void OnEventRaised()
        {
            RaiseResponse();
#if UNITY_EDITOR
            CreateDebugEntry(_response);
#endif
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