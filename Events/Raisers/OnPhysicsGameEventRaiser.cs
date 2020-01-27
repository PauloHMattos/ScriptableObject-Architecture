using System;
using ScriptableObjectArchitecture.Attributes;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    public abstract class OnPhysicsGameEventRaiser : BaseGameEventRaiser
    {
        [Group("General", "GameManager Icon")]
        public LifeCycle _lifeCycle;
        [Tag]
        public string Tag = "";
        public LayerMask layerMask;

        [Flags]
        public enum LifeCycle
        {
            Enter = 1,
            Stay = 2,
            Exit = 4
        }

        protected bool CheckTagAndLayer(string collisionTag, int collisionLayer)
        {
            if (!string.IsNullOrEmpty(Tag) && !collisionTag.Equals(Tag))
            {
                return false;
            }

            return ((layerMask & (1 << collisionLayer)) != 0);
        }

        protected void OnEnter(string collisionTag, int collisionLayer)
        {
            if (!_lifeCycle.HasFlag(LifeCycle.Enter))
            {
                return;
            }

            if (!CheckTagAndLayer(collisionTag, collisionLayer))
            {
                return;
            }
            
            _events.Invoke();
        }
        protected void OnStay(string collisionTag, int collisionLayer)
        {
            if (!_lifeCycle.HasFlag(LifeCycle.Stay))
            {
                return;
            }

            if (!CheckTagAndLayer(collisionTag, collisionLayer))
            {
                return;
            }

            _events.Invoke();
        }
        protected void OnExit(string collisionTag, int collisionLayer)
        {
            if (!_lifeCycle.HasFlag(LifeCycle.Exit))
            {
                return;
            }

            if (!CheckTagAndLayer(collisionTag, collisionLayer))
            {
                return;
            }

            _events.Invoke();
        }
    }
}