using System;
using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
    public class OnCollisionGameEventRaiser : BaseGameEventRaiser
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

        private void OnCollisionEnter(Collision other)
        {
            OnEnter();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnEnter();
        }

        private void OnCollisionExit(Collision other)
        {
            OnExit();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnExit();
        }

        private void OnCollisionStay(Collision other)
        {
            OnStay();
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            OnStay();
        }
    }
}