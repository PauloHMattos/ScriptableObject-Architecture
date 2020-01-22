using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventRaiser : SOArchitectureBaseMonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        private bool _showGeneral;
        [SerializeField, HideInInspector]
        private bool _showEvents;
#endif
        [FormerlySerializedAs("response"), FormerlySerializedAs("_response")]
        [SerializeField, HideInInspector]
        protected UnityEvent _events;

        protected virtual void Update()
        {
            // Just so the component can be disabled ins the inspector
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}