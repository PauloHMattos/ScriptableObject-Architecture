﻿using System;
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_RAISER_SUBMENU + "Timed Raiser")]
    public class TimedGameEventRaiser : BaseGameEventRaiser
    {
        [Group("General", "GameManager Icon")]
        public LifeCycle lifeCycle;
        public FloatReference interval;
        public float timeUntilNext;

        private float _lastTime;

        protected override void Update()
        {
            if (lifeCycle.HasFlag(LifeCycle.OnUpdate))
            {
                Raise();
            }
        }

        protected virtual void LateUpdate()
        {
            if (lifeCycle.HasFlag(LifeCycle.OnLateUpdate))
            {
                Raise();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (lifeCycle.HasFlag(LifeCycle.OnFixedUpdate))
            {
                Raise();
            }
        }

        protected void Raise()
        {
            timeUntilNext = Time.time - _lastTime;
            if (timeUntilNext < interval.Value)
            {
                return;
            }

            _lastTime = Time.time;
            _events.Invoke();
        }

        [Flags]
        public enum LifeCycle
        {
            OnUpdate = 1,
            OnLateUpdate = 2,
            OnFixedUpdate = 4
        }
    }
}