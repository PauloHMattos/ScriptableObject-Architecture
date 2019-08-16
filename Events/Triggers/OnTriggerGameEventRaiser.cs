using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public class OnTriggerGameEventRaiser : BaseGameEventRaiser
    {
        public LifeCycle _lifeCycle;

        [Flags]
        public enum LifeCycle
        {
            Enter = 1,
            Stay = 2,
            Exit = 4
        }

        private void OnEnter()
        {
            if (_lifeCycle.HasFlag(LifeCycle.Enter))
            {
                response.Invoke();
            }
        }
        private void OnStay()
        {
            if (_lifeCycle.HasFlag(LifeCycle.Stay))
            {
                response.Invoke();
            }
        }
        private void OnExit()
        {
            if (_lifeCycle.HasFlag(LifeCycle.Exit))
            {
                response.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnEnter();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEnter();
        }

        private void OnTriggerExit(Collider other)
        {
            OnExit();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnExit();
        }

        private void OnTriggerStay(Collider other)
        {
            OnStay();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            OnStay();
        }
    }
}
