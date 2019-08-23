using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public abstract class BaseGameEventRaiser : MonoBehaviour
    {
        public UnityEvent response;

        protected virtual void Update()
        {
            // Just so the component can be disabled ins the inspector
        }
    }
}