using System;
using ScriptableObjectArchitecture.Attributes;
using ScriptableObjectArchitecture.References;
using ScriptableObjectArchitecture.Utility;
using UnityEngine;

namespace ScriptableObjectArchitecture.Events.Raisers
{
    [AddComponentMenu(SoArchitectureUtility.EVENT_RAISER_SUBMENU + "Timed Raiser")]
    public class TimedGameEventRaiser : BaseGameEventRaiser
    {
        [Group("General", "GameManager Icon")]
        public LifeCycleOptions LifeCycle;
        public FloatReference Interval;
        public float TimeUntilNext;

        private float _lastTime;

        protected override void Update()
        {
            if (LifeCycle.HasFlag(LifeCycleOptions.OnUpdate))
            {
                Raise();
            }
        }

        protected virtual void LateUpdate()
        {
            if (LifeCycle.HasFlag(LifeCycleOptions.OnLateUpdate))
            {
                Raise();
            }
        }

        protected virtual void FixedUpdate()
        {
            if (LifeCycle.HasFlag(LifeCycleOptions.OnFixedUpdate))
            {
                Raise();
            }
        }

        protected void Raise()
        {
            TimeUntilNext = Time.time - _lastTime;
            if (TimeUntilNext < Interval.Value)
            {
                return;
            }

            _lastTime = Time.time;
            _events.Invoke();
        }

        [Flags]
        public enum LifeCycleOptions
        {
            OnUpdate = 1,
            OnLateUpdate = 2,
            OnFixedUpdate = 4
        }
    }
}