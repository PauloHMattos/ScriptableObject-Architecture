using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventRaiser : MonoBehaviour
    {
        [FormerlySerializedAs("response")]
        [SerializeField, HideInInspector]
        protected UnityEvent _response;

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